using CriticalPath.Web.Models;
using OzzIdentity.Controllers;
using OzzIdentity.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CriticalPath.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = SecurityRoles.Admin)]
    public class UsersController : AbstractAdminController
    {
        // GET: Admin/Users
        public async Task<ActionResult> Index(CriticalPath.Web.Controllers.BaseController.QueryParameters qParams)
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
                return View(users);
            }
        }



        // GET: Admin/SeedRoles
        // This action is for one time use only.
        // If we need to use this action, there may not be any admin role yet.
        [AllowAnonymous]
        public override async Task<ActionResult> SeedRoles()
        {
            return await base.SeedRoles();
        }

        protected override string AdminRole
        {
            get { return SecurityRoles.Admin; }
        }

        //will be used in SeedRoles action
        protected override string[] GetApplicationRoles()
        {
            return new string[] {
                SecurityRoles.Admin,
                SecurityRoles.Supervisor,
                SecurityRoles.Clerk,
                SecurityRoles.Observer
            };
        }
    }
}