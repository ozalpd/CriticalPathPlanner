using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Threading.Tasks;
using CriticalPath.Data;
using CriticalPath.Web.Models;
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by OzzCodeGen.
//
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CriticalPath.Web.Controllers
{
    public partial class BaseController : OzzIdentity.Controllers.AbstractController 
    {
        public partial class QueryParameters
        {
            public string SearchString { get; set; }
            public int PageNr { get; set; } = 1;
            public int PageSize { get; set; } = 10;
            public int PageCount
            {
                get
                {
                    return TotalCount > 0 ? (int)Math.Ceiling(TotalCount / (double)PageSize) : 0;
                }
            }

            public int Skip
            {
                get { return (PageNr - 1) * PageSize; }
            }

            public int TotalCount
            {
                get { return _totalCount; }
                set
                {
                    _totalCount = value;
                    if (PageNr < 1) PageNr = 1;
                    if (PageNr > PageCount) PageNr = PageCount;
                    int skip = (PageNr - 1) * PageSize;
                }
            }
            int _totalCount;
        }

        protected virtual void SetPagerParameters(QueryParameters qParams)
        {
            ViewBag.pageNr = qParams.PageNr;
            ViewBag.totalCount = qParams.TotalCount;
            ViewBag.pageSize = qParams.PageSize;
            ViewBag.pageCount = qParams.PageCount;
        }

        protected virtual async Task<bool> IsUserAdminAsync()
        {
            if (!_isUserAdmin.HasValue)
            {
                _isUserAdmin = Request.IsAuthenticated && 
                                await UserManager.IsInRoleAsync(UserID, SecurityRoles.Admin);
            }
            return _isUserAdmin.Value;
        }
        bool? _isUserAdmin;



        protected virtual async Task<bool> IsUserClerkAsync()
        {
            if (!_isUserClerk.HasValue)
            {
                _isUserClerk = Request.IsAuthenticated && 
                                    await UserManager.IsInRoleAsync(UserID, SecurityRoles.Clerk);
            }
            return _isUserClerk.Value;
        }
        bool? _isUserClerk;


        protected virtual async Task<bool> IsUserObserverAsync()
        {
            if (!_isUserObserver.HasValue)
            {
                _isUserObserver = Request.IsAuthenticated && 
                                    await UserManager.IsInRoleAsync(UserID, SecurityRoles.Observer);
            }
            return _isUserObserver.Value;
        }
        bool? _isUserObserver;


        protected virtual async Task<bool> IsUserSupervisorAsync()
        {
            if (!_isUserSupervisor.HasValue)
            {
                _isUserSupervisor = Request.IsAuthenticated && 
                                    await UserManager.IsInRoleAsync(UserID, SecurityRoles.Supervisor);
            }
            return _isUserSupervisor.Value;
        }
        bool? _isUserSupervisor;

        #region Find Methods for Entities

        protected virtual async Task<Contact> FindAsyncContact(int id)
        {
            return await DataContext
                            .GetContactQuery()
                            .FirstOrDefaultAsync(x => x.Id == id);
        }

        protected virtual async Task<Supplier> FindAsyncSupplier(int id)
        {
            return await DataContext
                            .GetSupplierQuery()
                            .FirstOrDefaultAsync(x => x.Id == id);
        }

        protected virtual async Task<Customer> FindAsyncCustomer(int id)
        {
            return await DataContext
                            .GetCustomerQuery()
                            .FirstOrDefaultAsync(x => x.Id == id);
        }

        protected virtual async Task<ProductCategory> FindAsyncProductCategory(int id)
        {
            return await DataContext
                            .GetProductCategoryQuery()
                            .FirstOrDefaultAsync(x => x.Id == id);
        }

        protected virtual async Task<Product> FindAsyncProduct(int id)
        {
            return await DataContext
                            .GetProductQuery()
                            .FirstOrDefaultAsync(x => x.Id == id);
        }

        protected virtual async Task<PuchaseOrder> FindAsyncPuchaseOrder(int id)
        {
            return await DataContext
                            .GetPuchaseOrderQuery()
                            .FirstOrDefaultAsync(x => x.Id == id);
        }

        protected virtual async Task<OrderItem> FindAsyncOrderItem(int id)
        {
            return await DataContext
                            .GetOrderItemQuery()
                            .FirstOrDefaultAsync(x => x.Id == id);
        }

        protected virtual async Task<Process> FindAsyncProcess(int id)
        {
            return await DataContext
                            .GetProcessQuery()
                            .FirstOrDefaultAsync(x => x.Id == id);
        }

        protected virtual async Task<ProcessStep> FindAsyncProcessStep(int id)
        {
            return await DataContext
                            .GetProcessStepQuery()
                            .FirstOrDefaultAsync(x => x.Id == id);
        }

        protected virtual async Task<ProcessTemplate> FindAsyncProcessTemplate(int id)
        {
            return await DataContext
                            .GetProcessTemplateQuery()
                            .FirstOrDefaultAsync(x => x.Id == id);
        }

        protected virtual async Task<ProcessStepTemplate> FindAsyncProcessStepTemplate(int id)
        {
            return await DataContext
                            .GetProcessStepTemplateQuery()
                            .FirstOrDefaultAsync(x => x.Id == id);
        }
        #endregion
        
        protected CriticalPathContext DataContext
        {
            get
            {
                if (_dataContext == null)
                {
                    _dataContext = new CriticalPathContext();
                }
                return _dataContext;
            }
        }
        private CriticalPathContext _dataContext;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_dataContext != null)
                {
                    _dataContext.Dispose();
                    _dataContext = null;
                }
            }
            base.Dispose(disposing);
        }
    }
}