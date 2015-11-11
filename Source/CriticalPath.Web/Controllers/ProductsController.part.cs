using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using CriticalPath.Web.Models;
using System.Net;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using CriticalPath.Data;

namespace CriticalPath.Web.Controllers
{
    public partial class ProductsController
    {
        public async Task<ActionResult> Products(int categoryId)
        {
            var query = from p in DataContext.GetProductDtoQuery()
                        where p.CategoryId == categoryId
                        select p;

            var result = await query.ToListAsync();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        protected override Task SetProductDefaults(Product product)
        {
            return base.SetProductDefaults(product);
        }
    }
}
