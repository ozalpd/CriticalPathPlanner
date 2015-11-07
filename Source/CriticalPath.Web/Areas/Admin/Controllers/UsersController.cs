﻿using CriticalPath.Web.Areas.Admin.Models;
using CriticalPath.Web.Controllers;
using CriticalPath.Web.Models;
using OzzIdentity.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CriticalPath.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = SecurityRoles.Admin)]
    public class UsersController : BaseController
    {
        // GET: Admin/Users
        public async Task<ActionResult> Index(QueryParameters qParams)
        {
            using (var idContext = new OzzIdentityDbContext())
            {
                IQueryable<OzzUser> query = idContext.Users.OrderBy(u => u.UserName);
                if (!string.IsNullOrEmpty(qParams.SearchString))
                {
                    query = from a in query
                            where
                                a.UserName.Contains(qParams.SearchString) |
                                a.FirstName.Contains(qParams.SearchString) |
                                a.LastName.Contains(qParams.SearchString)
                            select a;
                }
                qParams.TotalCount = await query.CountAsync();

                ViewBag.page = qParams.Page;
                ViewBag.totalCount = qParams.TotalCount;
                ViewBag.pageSize = qParams.PageSize;
                ViewBag.pageCount = qParams.PageCount;

                var users = qParams.TotalCount > 0 ? await query.ToListAsync() : new List<OzzUser>();
                var usersVM = new List<UserEditVM>();
                foreach (var user in users)
                {
                    var userVM = new UserEditVM(user);
                    await SetViewModelRoles(userVM);
                    usersVM.Add(userVM);
                }
                return View(usersVM);
            }
        }


        public async Task<ActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserEditVM userVM = null;
            using (var idContext = new OzzIdentityDbContext())
            {
                var user = await idContext.Users.FirstOrDefaultAsync(u => u.Id.Equals(id));
                if (user == null)
                {
                    return HttpNotFound();
                }
                userVM = new UserEditVM(user);
            }

            await SetViewModelRoles(userVM);

            return View(userVM);
        }

        private async Task SetViewModelRoles(UserEditVM userVM)
        {
            var roles = SecurityRoles.ApplicationRoles;
            foreach (var role in roles)
            {
                bool isInRole = await UserManager.IsInRoleAsync(userVM.Id, role);
                userVM.SetIsUserInRole(role, isInRole);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(UserEditVM userVM)
        {
            if (ModelState.IsValid)
            {
                var roles = SecurityRoles.ApplicationRoles;
                foreach (var item in roles)
                {
                    bool isInRole = await UserManager.IsInRoleAsync(userVM.Id, item);
                    bool isSetToRole = userVM.IsUserInRole(item);
                    //if(isInRole && isSetToRole) //Do Nothing
                    //if(!isInRole && !isSetToRole) //Do Nothing
                    if (!isInRole && isSetToRole)
                        await UserManager.AddToRoleAsync(userVM.Id, item);
                    if (isInRole && !isSetToRole)
                        await UserManager.RemoveFromRoleAsync(userVM.Id, item);
                }

                var user = await UserManager.Users.FirstOrDefaultAsync(u => u.Id == userVM.Id);
                userVM.UpdateUser(user);
                await UserManager.UpdateAsync(user);
                return RedirectToAction("Index");
            }

            return View(userVM);
        }
    }
}