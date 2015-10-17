using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using OzzIdentity.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OzzIdentity
{
    // Configure the application sign-in manager which is used in this application.
    //public class OzzSignInManager : SignInManager<ApplicationUser, Guid>
    public class OzzSignInManager : SignInManager<OzzUser, string>
    {
        public OzzSignInManager(OzzUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(OzzUser user)
        {
            return user.GenerateUserIdentityAsync((OzzUserManager)UserManager);
        }

        public static OzzSignInManager Create(IdentityFactoryOptions<OzzSignInManager> options, IOwinContext context)
        {
            return new OzzSignInManager(context.GetUserManager<OzzUserManager>(), context.Authentication);
        }
    }
}
