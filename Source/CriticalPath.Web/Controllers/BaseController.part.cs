using CP.i8n;
using CriticalPath.Data;
using OzzUtils.Web.Mvc;
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

        protected virtual void CancelCancellation(ICancellation entity)
        {
            entity.CancelledUserId = UserID;
            entity.CancelledUserIp = this.GetUserIP();
            entity.CancelDate = DateTime.Now;
            entity.Cancelled = true;
        }

        protected virtual async Task CancelSaveAsync(ICancellation entity)
        {
            if (string.IsNullOrEmpty((entity.CancelNotes)))
            {
                throw new Exception(string.Format("{0} required!", EntityStrings.CancelNotes));
            }

            CancelCancellation(entity);
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
            ViewBag.CategoryId = new SelectList(categoryList, "Id", "CategoryName", categoryId);

            var query = from c in DataContext.GetProductCategoryDtoQuery()
                        where c.ParentCategoryId == null
                        select c;
            List<ProductCategoryDTO> parentCategoryList = await query.ToListAsync();
            ViewBag.ParentCategoryId = new SelectList(parentCategoryList, "Id", "CategoryName", parentCategoryId);
        }

        protected async Task SetProductSelectListAsync(ProductDTO product)
        {
            await SetProductSelectListAsync(product?.ToProduct());
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
            ViewBag.ProductId = new SelectList(productList, "Id", "ProductCode", productCategoryId);
            await SetProductCategorySelectListAsync(product);
        }



        protected async Task SetSupplierSelectList(PurchaseOrder purchaseOrder)
        {
            if (purchaseOrder.Product == null)
            {
                purchaseOrder.Product = purchaseOrder.Product ??
                            await DataContext.Products
                                    .FirstOrDefaultAsync(p => p.Id == purchaseOrder.ProductId);
            }

            int supplierId = purchaseOrder == null ? 0 : purchaseOrder.SupplierId;
            await SetSupplierSelectList(purchaseOrder.Product, supplierId);
        }

        protected async Task SetSupplierSelectList(Product product, int supplierId)
        {
            var suppliers = product.Suppliers;
            if (suppliers == null || suppliers.Count == 0)
            {
                var querySuppliers = DataContext.GetSupplierQuery();
                suppliers = await querySuppliers.ToListAsync();
            }
            else if (supplierId == 0)
            {
                supplierId = suppliers.FirstOrDefault().Id;
            }
            ViewBag.SupplierId = new SelectList(suppliers, "Id", "CompanyName", supplierId);
        }


        protected async Task SetCustomerSelectListAsync(PurchaseOrderDTO po)
        {
            int customerId = po == null ? 0 : po.CustomerId;
            await SetCustomerSelectListAsync(customerId);
        }

        protected async Task SetCustomerSelectListAsync(PurchaseOrder po)
        {
            int customerId = po == null ? 0 : po.Customer != null ? po.Customer.Id : po.CustomerId;
            await SetCustomerSelectListAsync(customerId);
        }

        protected async Task SetCustomerSelectListAsync(int customerId)
        {
            var query = DataContext.GetCustomerDtoQuery();
            var customerList = await query.ToListAsync();
            ViewBag.CustomerId = new SelectList(customerList, "Id", "CompanyName", customerId);
        }

        protected async Task SetSizingStandardSelectListAsync(PurchaseOrderDTO po)
        {
            int standardId = po == null ? 0 : po.SizingStandardId;
            await SetSizingStandardSelectListAsync(standardId);
        }

        protected async Task SetSizingStandardSelectListAsync(PurchaseOrder po)
        {
            int standardId = po == null ? 0 : po.SizingStandard != null ? po.SizingStandard.Id : po.SizingStandardId;
            await SetSizingStandardSelectListAsync(standardId);
        }

        protected virtual async Task SetSizingStandardSelectListAsync(int sizingStandardId)
        {
            var standardList = await DataContext.GetSizingStandardDtoList();
            ViewBag.SizingStandardId = new SelectList(standardList, "Id", "Title", sizingStandardId);
            ViewBag.sizingStandards = standardList.ToJson();
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

        protected virtual async Task<bool> CanUserCancelPO()
        {
            if (!_canUserCancelPO.HasValue)
            {
                _canUserCancelPO = Request.IsAuthenticated && (
                                    await IsUserAdminAsync() ||
                                    await IsUserSupervisorAsync());
            }
            return _canUserCancelPO.Value;
        }
        bool? _canUserCancelPO;

        protected virtual async Task<bool> CanUserSeeCustomer()
        {
            if (!_canUserSeeCustomer.HasValue)
            {
                _canUserSeeCustomer = Request.IsAuthenticated && (
                                    await IsUserAdminAsync() ||
                                    await IsUserSupervisorAsync() ||
                                    await IsUserClerkAsync());
            }
            return _canUserSeeCustomer.Value;
        }
        bool? _canUserSeeCustomer;

        #endregion
    }
}