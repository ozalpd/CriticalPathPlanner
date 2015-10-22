using System.Linq;
using System.Data;
using System.Web.Mvc;
using System.Threading.Tasks;
using CriticalPath.Data;
using CriticalPath.Data.Resources;
using System.Net;
using System;
using System.Text;

namespace CriticalPath.Web.Controllers
{
    public partial class ProductCategoriesController
    {
        partial void SetViewBags(ProductCategory productCategory)
		{
            int parentCategoryId = productCategory == null ? 0 : (productCategory.ParentCategoryId ?? 0);
            var queryParentCategory = DataContext
                                        .GetProductCategoryQuery()
                                        .Where(c => c.Products.Count == 0);
			ViewBag.ParentCategoryId = new SelectList(queryParentCategory, "Id", "Title", parentCategoryId);
		}

        //public new partial class QueryParameters : BaseController.QueryParameters { }

        //Purpose: To set default property values for newly created ProductCategory entity
        //partial void SetDefaults(ProductCategory productCategory) { }
    }
}
