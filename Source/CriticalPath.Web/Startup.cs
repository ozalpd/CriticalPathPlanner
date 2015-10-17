using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CriticalPath.Web.Startup))]
namespace CriticalPath.Web
{
    public partial class Startup : OzzIdentity.Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
