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
        partial void SetViewBags(Product product)
        {
            var queryCategoryId = DataContext
                                    .GetProductCategoryQuery()
                                    .Where(c => c.SubCategories.Count == 0);
            int categoryId = product == null ? 0 : product.CategoryId;
            ViewBag.CategoryId = new SelectList(queryCategoryId, "Id", "Title", categoryId);
        }

        //public new class QueryParameters : BaseController.QueryParameters
        //{

        //}

        //Purpose of SetDefaults: To set default property values for newly created Product entity
        //partial void SetDefaults(Product product) { }

    }
}
