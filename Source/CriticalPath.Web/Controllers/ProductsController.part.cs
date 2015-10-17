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
        
        //Purpose: To set default property values for newly created Product entity
        //partial void SetDefaults(Product product) { }


        protected virtual async Task<bool> CanUserEdit()
        {
            if (!_canUserEdit.HasValue)
            {
                _canUserEdit = Request.IsAuthenticated && (await IsUserAdminAsync() || await IsUserSupervisorAsync() || await IsUserClerkAsync());
            }
            return _canUserEdit.Value;
        }
        bool? _canUserEdit;

        protected virtual async Task<bool> CanUserCreate()
        {
            if (!_canUserCreate.HasValue)
            {
                _canUserCreate = Request.IsAuthenticated && (await IsUserAdminAsync() || await IsUserSupervisorAsync() || await IsUserClerkAsync());
            }
            return _canUserCreate.Value;
        }
        bool? _canUserCreate;

        protected virtual async Task<bool> CanUserDelete()
        {
            if (!_canUserDelete.HasValue)
            {
                _canUserDelete = Request.IsAuthenticated && (await IsUserAdminAsync() || await IsUserSupervisorAsync());
            }
            return _canUserDelete.Value;
        }
        bool? _canUserDelete;
    }
}
