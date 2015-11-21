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
        protected virtual async Task<IQueryable<ProductCategory>> GetProductCategoryQuery(QueryParameters qParams)
        {
            var query = GetProductCategoryQuery();
            if (!string.IsNullOrEmpty(qParams.SearchString))
            {
                query = from a in query
                        where
                            a.CategoryName.Contains(qParams.SearchString) |
                            a.ParentCategory.CategoryName.Contains(qParams.SearchString) |
                            a.ParentCategory.CategoryCode.Contains(qParams.SearchString) |
                            a.CategoryCode.Contains(qParams.SearchString)
                        select a;
            }
            if (qParams.ParentCategoryId != null)
            {
                query = query.Where(x => x.ParentCategoryId == qParams.ParentCategoryId);
            }

            qParams.TotalCount = await query.CountAsync();
            return query.Skip(qParams.Skip).Take(qParams.PageSize);
        }

        protected async Task SetParentCategorySelectList(int parentCategoryId = 0)
        {
            var queryParentCategory = DataContext
                                       .GetProductCategoryQuery()
                                       //.Where(c => c.Products.Count == 0);  //For three or more level categories
                                       .Where(c => c.ParentCategoryId == null); //For two level categories
            var list = await DataContext
                        .GetProductCategoryDtoQuery(queryParentCategory)
                        .ToListAsync();
            ViewBag.ParentCategoryId = new SelectList(list, "Id", "CategoryName", parentCategoryId);
        }

        protected async override Task PutCanUserInViewBag()
        {
            await base.PutCanUserInViewBag();
            await SetParentCategorySelectList();
        }
    }
}
