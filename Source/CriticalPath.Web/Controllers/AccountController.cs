using System.Web.Mvc;
using OzzIdentity.Controllers;
using OzzIdentity;

namespace CriticalPath.Web.Controllers
{
    [Authorize]
    public class AccountController : BaseAccountController
    {
        public AccountController() { }

        public AccountController(OzzUserManager userManager, OzzSignInManager signInManager)
            : base(userManager, signInManager)
        {
        }
    }
}