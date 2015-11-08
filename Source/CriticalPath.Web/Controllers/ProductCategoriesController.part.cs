using System.Linq;
using System.Data;
using System.Web.Mvc;
using System.Threading.Tasks;
using CriticalPath.Data;
using CP.i8n;
using System.Net;
using System;
using System.Text;
using System.Data.Entity;

namespace CriticalPath.Web.Controllers
{
    public partial class ProductCategoriesController
    {
        partial void SetSelectLists(ProductCategory productCategory)
        {
            int parentCategoryId = productCategory == null ? 0 : (productCategory.ParentCategoryId ?? 0);
            var queryParentCategory = DataContext
                                        .GetProductCategoryQuery()
                                        .Where(c => c.Products.Count == 0);
			ViewBag.ParentCategoryId = new SelectList(queryParentCategory, "Id", "Title", parentCategoryId);
		}

        public async Task<ActionResult> MainCategories()
        {
            var query = from c in DataContext.GetProductCategoryDtoQuery()
                        where c.ParentCategoryId == null
                        select c;

            var result = await query.ToListAsync();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> SubCategories(int parentCategoryId)
        {
            var query = from c in DataContext.GetProductCategoryDtoQuery()
                        where c.ParentCategoryId == parentCategoryId
                        select c;

            var result = await query.ToListAsync();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
