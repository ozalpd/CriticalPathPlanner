using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Web.Mvc;
using System.Threading.Tasks;
using OzzIdentity.Models;
using OzzIdentity.Controllers;
using CriticalPath.Web.Models;

namespace CriticalPath.Web.Controllers
{
    public partial class AdmPanelController : AbstractAdminController
    {
        [Authorize]
        public override ActionResult Index()
        {
            var sb = new StringBuilder();
            sb.Append("<h4>Test Panel</h4>");

            var idContext = new OzzIdentityDbContext();
            var users = idContext.Users;

            foreach (var user in users)
            {
                sb.Append(user.Id);
                sb.Append(" ");
                sb.Append(user.UserName);
                sb.Append(" ");
                sb.Append(user.FirstName);
                sb.Append(" ");
                sb.Append(user.LastName);
                sb.Append("<br>");
            }
            return Content(sb.ToString());
        }

        public ActionResult SelectTheme(string theme)
        {
            if (!string.IsNullOrEmpty(theme))
                AppSettings.SelectedTheme = theme;

            var vm = new SelectThemeVM();
            vm.Theme = AppSettings.SelectedTheme;

            return View(vm);
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
