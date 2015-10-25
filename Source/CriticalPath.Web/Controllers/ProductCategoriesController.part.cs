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
        protected override void SetProductCategoryDefaults(ProductCategory productCategory)
        {
            int parentCategoryId = productCategory == null ? 0 : (productCategory.ParentCategoryId ?? 0);
            var queryParentCategory = DataContext
                                        .GetProductCategoryQuery()
                                        .Where(c => c.Products.Count == 0);
			ViewBag.ParentCategoryId = new SelectList(queryParentCategory, "Id", "Title", parentCategoryId);
		}
    }
}
