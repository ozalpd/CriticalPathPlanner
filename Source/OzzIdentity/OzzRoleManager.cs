using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using OzzIdentity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzzIdentity
{
    public class OzzRoleManager : RoleManager<IdentityRole>
    {
        public OzzRoleManager(RoleStore<IdentityRole> store)
            : base(store) { }

        public static OzzRoleManager Create(IdentityFactoryOptions<OzzRoleManager> options, IOwinContext context)
        {
            var store = new RoleStore<IdentityRole>(context.Get<OzzIdentityDbContext>());
            return new OzzRoleManager(store);
        }
    }
}
