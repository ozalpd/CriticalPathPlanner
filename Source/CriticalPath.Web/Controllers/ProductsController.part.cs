using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Threading.Tasks;
using CriticalPath.Data;
using CriticalPath.Web.Models;
using CP.i8n;

namespace CriticalPath.Web.Controllers
{
    public partial class ProductsController 
    {
        [Authorize(Roles = "admin, supervisor, clerk")]
        public async Task<JsonResult> GetProductsWithPrice(QueryParameters qParam)
        {
            var query = GetProductQuery()
                        .Where(p => p.ProductCode.Contains(qParam.SearchString))
                        .Take(qParam.PageSize);
            var list = from p in query
                       select new
                       {
                           id = p.Id,
                           value = p.ProductCode,
                           label = p.ProductCode + " [" + p.Category.ParentCategory.CategoryName + " / " + p.Category.CategoryName + "]",
                           UnitPrice = p.UnitPrice,
                           SellingCurrencyId = p.SellingCurrencyId,
                           RoyaltyFee = p.RoyaltyFee,
                           RoyaltyCurrencyId = p.RoyaltyCurrencyId,
                           RetailPrice = p.RetailPrice,
                           RetailCurrencyId = p.RetailCurrencyId,
                           BuyingPrice = p.BuyingPrice,
                           BuyingCurrencyId = p.BuyingCurrencyId
                       };

            return Json(await list.ToListAsync(), JsonRequestBehavior.AllowGet);
        }

        //Purpose: To set default property values for newly created Product entity
        //protected override async Task SetProductDefaults(Product product) { }
    }
}
