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
            if (string.IsNullOrEmpty((entity.CancellationReason)))
            {
                throw new Exception(string.Format("{0} required!", EntityStrings.CancellationReason));
            }

            CancelCancellation(entity);
            await DataContext.SaveChangesAsync(this);
        }

        protected virtual void SetDiscontinuedUser(IDiscontinuedUser entity)
        {
            entity.DiscontinuedUserId = UserID;
            entity.DiscontinuedUserIp = GetUserIP();
        }


        #region SetSelectList Helper Methods

        protected virtual async Task<SelectList> GetCountrySelectList(int countryId = 0)
        {
            var currecies = await DataContext.GetCountryDtoList();
            return new SelectList(currecies, "Id", "CountryName", countryId);
        }

        protected virtual async Task SetCountrySelectList(int countryId = 0)
        {
            ViewBag.CountryId = await GetCountrySelectList(countryId);
        }

        protected virtual async Task<SelectList> GetCurrencySelectList(int currencyId = 0)
        {
            var currecies = await DataContext.GetCurrencyDtoList();
            return new SelectList(currecies, "Id", "CurrencyCode", currencyId);
        }

        protected virtual async Task SetCurrencySelectList(int currencyId = 0)
        {
            ViewBag.CurrencyId = await GetCurrencySelectList(currencyId);
        }

        protected async Task SetProductCategorySelectListAsync(ProductDTO product)
        {
            await SetProductCategorySelectListAsync(product.CategoryId);
        }

        protected async Task SetProductCategorySelectListAsync(Product product)
        {
            var productCategory = product?.Category == null ? null : product.Category;
            if (productCategory == null && product != null && product.CategoryId > 0)
                productCategory = await FindAsyncProductCategory(product.CategoryId);

            await SetProductCategorySelectListAsync(productCategory);
        }

        protected async Task SetProductCategorySelectListAsync(int categoryId)
        {
            var productCategory = categoryId > 0 ? await FindAsyncProductCategory(categoryId) : null;
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


        protected async Task SetSupplierSelectList(int supplierId)
        {
            await SetSupplierSelectList(null, supplierId);
        }

        protected async Task SetSupplierSelectList(PurchaseOrder purchaseOrder)
        {
            if (purchaseOrder.Product == null)
            {
                purchaseOrder.Product = purchaseOrder.Product ??
                            await DataContext.Products
                                    .FirstOrDefaultAsync(p => p.Id == purchaseOrder.ProductId);
            }

            int supplierId = purchaseOrder == null ? 0 : (purchaseOrder.SupplierId ?? 0);
            await SetSupplierSelectList(purchaseOrder.Product, supplierId);
        }

        protected async Task SetSupplierSelectList(Product product, int supplierId)
        {
            var suppliers = product?.Suppliers;
            if (suppliers == null || suppliers.Count == 0)
            {
                var querySuppliers = DataContext.GetSupplierQuery();
                suppliers = await querySuppliers.ToListAsync();
            }
            //else if (supplierId == 0)
            //{
            //    supplierId = suppliers.FirstOrDefault().Id;
            //}
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

        protected async Task SetCustomerDepartmentSelectListAsync(int customerId, int departmentId)
        {
            List<CustomerDepartmentDTO> departmentList;
            if (customerId > 0)
            {
                var query = DataContext
                            .GetCustomerDepartmentDtoQuery()
                            .Where(d => d.CustomerId == customerId);
                departmentList = await query.ToListAsync();
            }
            else
            {
                departmentList = new List<CustomerDepartmentDTO>();
            }
            ViewBag.CustomerDepartmentId = new SelectList(departmentList, "Id", "DepartmentName", departmentId);
        }

        protected void SetPageSizeSelectList(int pageSize)
        {
            int[] sizeArray = { 10, 20, 50, 100 };
            if (!sizeArray.Contains(pageSize))
            {
                pageSize = sizeArray[0];
            }

            var list = from s in sizeArray
                       select new { val = s, title = s.ToString() };
            ViewBag.PageSize = new SelectList(list, "val", "title", pageSize);
        }

        protected virtual async Task SetFreightTermSelectListAsync(int freightTermId)
        {
            var terms = await DataContext.GetFreightTermDtoList();
            ViewBag.FreightTermId = new SelectList(terms, "Id", "IncotermCode", freightTermId);
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

        protected virtual async Task<bool> CanUserDiscontinue()
        {
            if (!_canUserDiscontinue.HasValue)
            {
                _canUserDiscontinue = Request.IsAuthenticated && (
                                    await IsUserAdminAsync() ||
                                    await IsUserSupervisorAsync());
            }
            return _canUserDiscontinue.Value;
        }
        bool? _canUserDiscontinue;

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