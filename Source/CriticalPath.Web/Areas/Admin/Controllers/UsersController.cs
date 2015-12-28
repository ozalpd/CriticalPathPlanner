using CriticalPath.Web.Areas.Admin.Models;
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
                PutPagerInViewBag(qParams);

                var users = qParams.TotalCount > 0 ? await query.ToListAsync() : new List<OzzUser>();
                var usersVM = new List<UserAdminVM>();
                foreach (var user in users)
                {
                    var userVM = new UserAdminVM(user);
                    await SetViewModelRoles(userVM);
                    usersVM.Add(userVM);
                }
                return View(usersVM);
            }
        }

        public async Task<ActionResult> Details(string id, string userName)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserAdminVM userVM = await GetUserVM(id, userName);
            if (userVM == null)
                return HttpNotFound();

            return View(userVM);
        }

        public async Task<ActionResult> Edit(string id, string userName)
        {
            if (string.IsNullOrEmpty(id) && string.IsNullOrEmpty(userName))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserAdminVM userVM = await GetUserVM(id, userName);
            if (userVM == null)
                return HttpNotFound();

            return View(userVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(UserAdminVM userVM)
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




        private async Task<UserAdminVM> GetUserVM(string id, string userName)
        {
            UserAdminVM userVM = null;
            using (var idContext = new OzzIdentityDbContext())
            {
                var user = string.IsNullOrEmpty(id) ? 
                            await idContext.Users.FirstOrDefaultAsync(u => u.UserName.Equals(userName)):
                            await idContext.Users.FirstOrDefaultAsync(u => u.Id.Equals(id));
                
                if (user == null)
                {
                    return null;
                }
                userVM = new UserAdminVM(user);
            }
            await SetViewModelRoles(userVM);

            return userVM;
        }

        private async Task SetViewModelRoles(UserAdminVM userVM)
        {
            var roles = SecurityRoles.ApplicationRoles;
            foreach (var role in roles)
            {
                bool isInRole = await UserManager.IsInRoleAsync(userVM.Id, role);
                userVM.SetIsUserInRole(role, isInRole);
            }
        }
    }
}