using CriticalPath.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CriticalPath.Web.Controllers
{
    public abstract partial class BaseController
    {
        protected virtual async Task<Company> FindAsyncCompany(int id)
        {
            return await DataContext.Companies.FirstOrDefaultAsync(x => x.Id == id);
        }

        protected virtual void Approve(IApproval approval)
        {
            approval.IsApproved = true;
            approval.ApproveDate = DateTime.Today;
            approval.ApprovedUserId = UserID;
            approval.ApprovedUserIp = GetUserIP();
        }

        protected virtual async Task ApproveSaveAsync(IApproval approval)
        {
            Approve(approval);
            await DataContext.SaveChangesAsync(this);
        }

        #region SetSelectList Helper Methods

        protected async Task SetProductCategorySelectListAsync(ProductDTO product)
        {
            await SetProductCategorySelectListAsync(product);
        }

        protected async Task SetProductCategorySelectListAsync(Product product)
        {
            var productCategory = product == null ? null : product.Category == null ? null : product.Category;
            if (productCategory == null && product != null && product.CategoryId > 0)
                productCategory = await FindAsyncProductCategory(product.CategoryId);

            await SetProductCategorySelectListAsync(productCategory);
        }

        protected async Task SetProductCategorySelectListAsync(ProductCategoryDTO productCategory)
        {
            await SetProductCategorySelectListAsync(productCategory);
        }

        protected async Task SetProductCategorySelectListAsync(ProductCategory productCategory)
        {
            int parentCategoryId = productCategory == null ? 0 : productCategory.ParentCategoryId ?? 0;
            List<ProductCategoryDTO> categoryList;
            if (parentCategoryId > 0)
            {
                var querySubCatg = from c in DataContext.GetProductCategoryDtoQuery()
                                   where c.ParentCategoryId == parentCategoryId
                                   select c;
                categoryList = await querySubCatg.ToListAsync();
            }
            else
            {
                categoryList = new List<ProductCategoryDTO>();
            }
            int categoryId = productCategory == null ? 0 : productCategory.Id;
            ViewBag.CategoryId = new SelectList(categoryList, "Id", "Title", categoryId);

            var query = from c in DataContext.GetProductCategoryDtoQuery()
                        where c.ParentCategoryId == null
                        select c;
            List<ProductCategoryDTO> parentCategoryList = await query.ToListAsync();
            ViewBag.ParentCategoryId = new SelectList(parentCategoryList, "Id", "Title", parentCategoryId);
        }

        protected async Task SetProductSelectListAsync(ProductDTO product)
        {
            await SetProductSelectListAsync(product.ToProduct());
        }

        protected async Task SetProductSelectListAsync(Product product)
        {
            int productCategoryId = product == null ? 0 :
                                    product.Category == null ? product.CategoryId : product.Category.Id;
            List<ProductDTO> productList;
            if (productCategoryId > 0)
            {
                var query = from p in DataContext.GetProductDtoQuery()
                            where p.CategoryId == productCategoryId
                            select p;
                productList = await query.ToListAsync();
            }
            else
            {
                productList = new List<ProductDTO>();
            }
            ViewBag.ProductId = new SelectList(productList, "Id", "Title", productCategoryId);
            await SetProductCategorySelectListAsync(product);
        }

        #endregion

        #region CanUserAdd Authorization Methods

        protected virtual async Task<bool> CanUserAddPurchaseOrder()
        {
            if (!_canUserAddPurchaseOrder.HasValue)
            {
                _canUserAddPurchaseOrder = Request.IsAuthenticated && (
                                    await IsUserAdminAsync() ||
                                    await IsUserSupervisorAsync() ||
                                    await IsUserClerkAsync());
            }
            return _canUserAddPurchaseOrder.Value;
        }
        bool? _canUserAddPurchaseOrder;

        protected virtual async Task<bool> CanUserAddContact()
        {
            if (!_canUserAddContact.HasValue)
            {
                _canUserAddContact = Request.IsAuthenticated && (
                                    await IsUserAdminAsync() ||
                                    await IsUserSupervisorAsync() ||
                                    await IsUserClerkAsync());
            }
            return _canUserAddContact.Value;
        }
        bool? _canUserAddContact;

        protected virtual async Task<bool> CanUserApprove()
        {
            if (!_canUserApprove.HasValue)
            {
                _canUserApprove = Request.IsAuthenticated && (
                                    await IsUserAdminAsync() ||
                                    await IsUserSupervisorAsync());
            }
            return _canUserApprove.Value;
        }
        bool? _canUserApprove;

        #endregion
    }
}