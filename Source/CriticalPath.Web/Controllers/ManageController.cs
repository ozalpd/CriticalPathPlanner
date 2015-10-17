using System.Web.Mvc;
using OzzIdentity.Controllers;
using OzzIdentity;

namespace CriticalPath.Web.Controllers
{
    [Authorize]
    public class ManageController : BaseManageController
    {
        public ManageController() { }

        public ManageController(OzzUserManager userManager, OzzSignInManager signInManager)
            : base(userManager, signInManager)
        { }
    }
}