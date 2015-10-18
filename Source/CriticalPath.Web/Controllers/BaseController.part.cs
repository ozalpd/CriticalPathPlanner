using CriticalPath.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CriticalPath.Web.Controllers
{
    public partial class BaseController
    {
        protected virtual async Task<Company> FindAsyncCompany(int id)
        {
            return await DataContext.Companies.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}