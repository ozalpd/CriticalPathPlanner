using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Threading.Tasks;
using CriticalPath.Data;
using CriticalPath.Web.Models;
using CP.i8n;
using CriticalPath.Web.Controllers;
using OzzUtils.Web.Mvc;
using CriticalPath.Web.Areas.Admin.Models;
using Microsoft.AspNet.Identity;
using OzzIdentity.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CriticalPath.Web.Areas.Admin.Controllers
{
    public partial class EmployeesController
    {
        protected virtual async Task<IQueryable<Employee>> GetEmployeeQuery(QueryParameters qParams)
        {
            var query = GetEmployeeQuery();
            if (!string.IsNullOrEmpty(qParams.SearchString))
            {
                query = from a in query
                        where
                            a.AspNetUser.UserName.Contains(qParams.SearchString) ||
                            a.AspNetUser.FirstName.Contains(qParams.SearchString) ||
                            a.AspNetUser.LastName.Contains(qParams.SearchString)
                        select a;
            }
            if (qParams.IsActive != null)
            {
                query = query.Where(x => x.IsActive == qParams.IsActive.Value);
            }
            if (qParams.InactivateDateMin != null)
            {
                query = query.Where(x => x.InactivateDate >= qParams.InactivateDateMin.Value);
            }
            if (qParams.InactivateDateMax != null)
            {
                query = query.Where(x => x.InactivateDate <= qParams.InactivateDateMax.Value);
            }

            qParams.TotalCount = await query.CountAsync();
            return query.Skip(qParams.Skip).Take(qParams.PageSize);
        }

        protected virtual async Task<List<EmployeeDTO>> GetEmployeeDtoList(QueryParameters qParams)
        {
            var query = await GetEmployeeQuery(qParams);
            var result = qParams.TotalCount > 0 ?
                            await DataContext.GetEmployeeDtoQuery(query).ToListAsync() :
                            new List<EmployeeDTO>();

            return result;
        }

        [Authorize(Roles = "admin, supervisor")]
        public async Task<ActionResult> Index(QueryParameters qParams)
        {
            var items = await GetEmployeeDtoList(qParams);
            ViewBag.totalCount = qParams.TotalCount;
            await PutCanUserInViewBag();
            var result = new PagedList<EmployeeDTO>(qParams, items);
            ViewBag.result = result.ToJson();

            return View();
        }

        protected override async Task<bool> CanUserCreate()
        {
            if (!_canUserCreate.HasValue)
            {
                _canUserCreate = Request.IsAuthenticated && (
                                    await IsUserAdminAsync());
            }
            return _canUserCreate.Value;
        }
        bool? _canUserCreate;

        protected override async Task<bool> CanUserEdit()
        {
            if (!_canUserEdit.HasValue)
            {
                _canUserEdit = Request.IsAuthenticated && (
                                    await IsUserAdminAsync());
            }
            return _canUserEdit.Value;
        }
        bool? _canUserEdit;

        protected override async Task<bool> CanUserDelete()
        {
            if (!_canUserDelete.HasValue)
            {
                _canUserDelete = Request.IsAuthenticated && (
                                    await IsUserAdminAsync());
            }
            return _canUserDelete.Value;
        }
        bool? _canUserDelete;

        protected override async Task<bool> CanUserSeeRestricted()
        {
            if (!_canSeeRestricted.HasValue)
            {
                _canSeeRestricted = Request.IsAuthenticated && (
                                    await IsUserAdminAsync() ||
                                    await IsUserSupervisorAsync());
            }
            return _canSeeRestricted.Value;
        }
        bool? _canSeeRestricted;


        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Create()
        {
            var vm = new EmployeeCreateVM();
            await SetEmployeeDefaults(vm);
            return View(vm);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(EmployeeCreateVM employeeVM)
        {
            if (ModelState.IsValid)
            {
                var entity = employeeVM.ToEmployee();
                //TODO: Implement password reset and send a password recovery mail
                var user = employeeVM.ToUser();
                IdentityResult result = await CreateUserUnique(user, "Dnm!2345");
                if (result.Succeeded)
                {
                    await AddToRoleAsync(user, SecurityRoles.Clerk);
                    entity.AspNetUserId = user.Id;
                    entity.IsActive = true;

                    DataContext.Employees.Add(entity);
                    await DataContext.SaveChangesAsync(this);
                    await UserManager.SetLockoutEnabledAsync(user.Id, true);
                }
                else
                {
                    throw new Exception(string.Format("User {0} could not be created! See errors: {1}",
                        user.UserName,
                        result.Errors));
                }
                return RedirectToAction("Details", new { id = entity.Id });
                //return RedirectToAction("Index");
            }

            return View(employeeVM);
        }

        private async Task<IdentityResult> CreateUserUnique(OzzUser user, string password)
        {
            var prevSavedUser = await UserManager.FindByNameAsync(user.UserName);
            if (prevSavedUser == null)
            {
                return await UserManager.CreateAsync(user, password);
            }

            int count = await DataContext
                        .AspNetUsers
                        .Where(u => u.UserName.StartsWith(user.UserName))
                        .CountAsync();
            user.UserName = string.Format("{0}.{1}", user.UserName, count + 1);

            return await UserManager.CreateAsync(user, password);
        }

        private async Task AddToRoleAsync(OzzUser user, string roleName)
        {
            if (!(await RoleManager.RoleExistsAsync(roleName)))
            {
                await RoleManager.CreateAsync(new IdentityRole(roleName));
            }

            await UserManager.AddToRoleAsync(user.Id, roleName);
        }


        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = await FindAsyncEmployee(id.Value);

            if (employee == null)
            {
                return HttpNotFound();
            }

            var employeeDTO = new EmployeeDTO(employee);
            SetSelectLists(employee);
            return View(employeeDTO);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EmployeeDTO vm)
        {
            if (ModelState.IsValid)
            {
                Employee employee = await FindAsyncEmployee(vm.Id);
                if (employee == null)
                    return HttpNotFound();

                var user = await UserManager.Users.FirstOrDefaultAsync(u => u.Id == employee.AspNetUserId);
                if (employee.IsActive && !vm.IsActive)
                {
                    employee.InactivateDate = DateTime.UtcNow;
                    user.LockoutEndDateUtc = DateTime.UtcNow.AddYears(100);
                    user.LockoutEnabled = true;
                }
                else if (!employee.IsActive && vm.IsActive)
                {
                    employee.InactivateDate = null;
                    user.LockoutEndDateUtc = null;
                }
                employee.IsActive = vm.IsActive;
                user.FirstName = vm.FirstName;
                user.LastName = vm.LastName;
                user.Email = vm.Email;
                user.PhoneNumber = vm.PhoneNumber;

                await UserManager.UpdateAsync(user);
                await DataContext.SaveChangesAsync(this);
                return RedirectToAction("Details", new { id = vm.Id });
            }

            SetSelectLists(vm.ToEmployee());
            return View(vm);
        }

        [Authorize(Roles = "admin, supervisor")]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = await FindAsyncEmployee(id.Value);

            if (employee == null)
            {
                return HttpNotFound();
            }

            await PutCanUserInViewBag();
            return View(new EmployeeDTO(employee));
        }

        //Purpose: To set default property values for newly created Employee entity
        //protected override async Task SetEmployeeDefaults(Employee employee) { }
    }
}
