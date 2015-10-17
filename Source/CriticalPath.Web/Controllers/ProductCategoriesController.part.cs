using System;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Net;
using System.Web.Mvc;
using System.Threading.Tasks;
using CriticalPath.Web.Models;
using CriticalPath.Data;

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

        //Purpose: To set default property values for newly created ProductCategory entity
        //partial void SetDefaults(ProductCategory productCategory) { }

        protected virtual async Task<bool> CanUserEdit()
        {
            if (!_canUserEdit.HasValue)
            {
                _canUserEdit = Request.IsAuthenticated && await IsUserAdminAsync() && await IsUserClerkAsync();
            }
            return _canUserEdit.Value;
        }
        bool? _canUserEdit;

        protected virtual async Task<bool> CanUserCreate()
        {
            if (!_canUserCreate.HasValue)
            {
                _canUserCreate = Request.IsAuthenticated && await IsUserAdminAsync() && await IsUserClerkAsync();
            }
            return _canUserCreate.Value;
        }
        bool? _canUserCreate;

        protected virtual async Task<bool> CanUserDelete()
        {
            if (!_canUserDelete.HasValue)
            {
                _canUserDelete = Request.IsAuthenticated && await IsUserAdminAsync();
            }
            return _canUserDelete.Value;
        }
        bool? _canUserDelete;
    }
}
