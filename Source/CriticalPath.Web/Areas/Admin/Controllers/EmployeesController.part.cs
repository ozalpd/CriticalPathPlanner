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
                            a.AspNetUser.LastName.Contains(qParams.SearchString) ||
                            a.Position.Position.Contains(qParams.SearchString)
                        select a;
            }
            if (qParams.IsActive != null)
            {
                query = query.Where(x => x.IsActive == qParams.IsActive.Value);
            }
            if ((qParams.PositionId ?? 0) > 0)
            {
                query = query.Where(x => x.PositionId == qParams.PositionId.Value);
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

            await SetEmployeePositionSelectListAsync(qParams.PositionId ?? 0);
            await PutCanUserInViewBag();
            var result = new PagedList<EmployeeDTO>(qParams, items);
            ViewBag.result = result.ToJson();

            return View();
        }


        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Create()
        {
            var vm = new EmployeeCreateVM();
            SetEmployeeDefaults(vm);
            await SetEmployeePositionSelectListAsync(vm.PositionId ?? 0);
            return View(vm);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(EmployeeCreateVM vm)
        {
            if (ModelState.IsValid)
            {
                var entity = vm.ToEmployee();
                //TODO: Implement password reset and send a password recovery mail
                var user = vm.ToUser();
                IdentityResult result = await CreateUserUnique(user, vm.NewPassword);
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

                await DataContext.RefreshDesignerDtoList();
                await DataContext.RefreshMerchandiserDtoList();

                return RedirectToAction("Details", new { id = entity.Id });
            }

            await SetEmployeePositionSelectListAsync(vm.PositionId ?? 0);
            return View(vm);
        }

        protected void SetEmployeeDefaults(EmployeeCreateVM employee)
        {
            employee.NewPassword = "Dnm!2345";
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
            await SetEmployeePositionSelectListAsync(employeeDTO.PositionId ?? 0);
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
                employee.PositionId = vm.PositionId;
                user.FirstName = vm.FirstName;
                user.LastName = vm.LastName;
                user.Email = vm.Email;
                user.PhoneNumber = vm.PhoneNumber;

                await UserManager.UpdateAsync(user);
                await DataContext.SaveChangesAsync(this);

                await DataContext.RefreshDesignerDtoList();
                await DataContext.RefreshMerchandiserDtoList();

                return RedirectToAction("Details", new { id = vm.Id });
            }

            await SetEmployeePositionSelectListAsync(vm.PositionId ?? 0);
            return View(vm);
        }


        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequestTextResult();
            }
            Employee employee = await FindAsyncEmployee(id.Value);

            if (employee == null)
            {
                return NotFoundTextResult();
            }

            DataContext.Employees.Remove(employee);
            try
            {
                await DataContext.SaveChangesAsync(this);

                await DataContext.RefreshDesignerDtoList();
                await DataContext.RefreshMerchandiserDtoList();
            }
            catch (Exception ex)
            {
                var sb = new StringBuilder();
                sb.Append(MessageStrings.CanNotDelete);
                sb.Append(employee.AspNetUser.UserName);
                sb.Append("<br/>");
                AppendExceptionMsg(ex, sb);

                return StatusCodeTextResult(sb, HttpStatusCode.InternalServerError);
            }

            return new HttpStatusCodeResult(HttpStatusCode.OK);
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


        [Authorize(Roles = "admin")]
        public async Task<ActionResult> ResetPassword(int? id)
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

            var vm = new EmployeePasswordResetVM(employee);

            return View(vm);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(EmployeePasswordResetVM vm)
        {
            if (ModelState.IsValid)
            {
                var token = await UserManager.GeneratePasswordResetTokenAsync(vm.AspNetUserId);
                var result = await UserManager.ResetPasswordAsync(vm.AspNetUserId, token, vm.NewPassword);
                if (result.Succeeded)
                {
                    return RedirectToAction("Details", new { id = vm.Id });
                }
            }
            return View(vm);
        }
    }
}
