using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzzIdentity.Models
{
    public class OzzIdentityDbContext : IdentityDbContext<OzzUser>
    {
        public OzzIdentityDbContext()
           : base("IdentityConnection") { }

        public OzzIdentityDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString) { }

        public static OzzIdentityDbContext Create()
        {
            return new OzzIdentityDbContext();
        }
    }
}
