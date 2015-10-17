using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace OzzIdentity.Controllers
{
    public abstract class AbstractAdminController : AbstractController
    {
        protected abstract string[] GetApplicationRoles();
        protected abstract string AdminRole { get; }

        public virtual ActionResult Index()
        {
            return Content("Admin Panel");
        }

        [Authorize]
        public virtual async Task<ActionResult> SeedRoles()
        {
            StringBuilder sb = new StringBuilder();
            bool hasAdminRole = await RoleManager.RoleExistsAsync(AdminRole);
            sb.Append(AdminRole);
            if (hasAdminRole)
            {
                sb.Append(" role already exists!");
            }
            else
            {
                await RoleManager.CreateAsync(new IdentityRole(AdminRole));
                var user = await GetCurrentUserAsync();
                await UserManager.AddToRoleAsync(UserID, AdminRole);

                sb.Append(" role created.");
            }
            sb.Append("<br/>");
            var roles = GetApplicationRoles();
            foreach (var item in roles.Where(r => r.Equals(AdminRole) == false))
            {
                bool hasRole = await RoleManager.RoleExistsAsync(item);
                sb.Append(item);
                if (hasRole)
                {
                    sb.Append(" role already exists!");
                }
                else
                {
                    await RoleManager.CreateAsync(new IdentityRole(item));
                    sb.Append(" role created.");
                }
                sb.Append("<br/>");
            }
            return Content(sb.ToString());
        }
    }
}
