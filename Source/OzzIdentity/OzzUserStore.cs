using Microsoft.AspNet.Identity.EntityFramework;
using OzzIdentity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzzIdentity
{
    public class OzzUserStore : UserStore<OzzUser>
    {
        public OzzUserStore() { }
        public OzzUserStore(OzzIdentityDbContext dbContext)
            : base(dbContext)
        { }

    }
}
