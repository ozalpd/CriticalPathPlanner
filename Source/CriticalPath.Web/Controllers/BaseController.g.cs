using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Threading.Tasks;
using CriticalPath.Data;
using CriticalPath.Web.Models;
using CP.i8n;
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by OzzCodeGen.
//
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CriticalPath.Web.Controllers
{
    public abstract partial class BaseController : OzzIdentity.Controllers.AbstractController 
    {
        public partial class QueryParameters
        {
            public QueryParameters() { Constructing(); }
            protected virtual void Constructing()
            {
                Page = 1;
                PageSize = 10;
            }

            public QueryParameters(QueryParameters parameters)
            {
                Constructing(parameters);
            }
            protected virtual void Constructing(QueryParameters parameters)
            {
                Page = parameters.Page;
                PageSize = parameters.PageSize;
                SearchString = parameters.SearchString;
                TotalCount = parameters.TotalCount;
            }

            public string SearchString { get; set; }
            public int Page { get; set; }
            public int PageSize { get; set; }
            public int PageCount
            {
                get
                {
                    return TotalCount > 0 ? (int)Math.Ceiling(TotalCount / (double)PageSize) : 0;
                }
            }

            public int Skip
            {
                get { return (Page - 1) * PageSize; }
            }

            public int TotalCount
            {
                get { return _totalCount; }
                set
                {
                    _totalCount = value;
                    if (Page < 1) Page = 1;
                    if (Page > PageCount) Page = PageCount;
                    int skip = (Page - 1) * PageSize;
                }
            }
            int _totalCount;
        }

        protected virtual void PutPagerInViewBag(QueryParameters qParams)
        {
            ViewBag.page = qParams.Page;
            ViewBag.totalCount = qParams.TotalCount;
            ViewBag.pageSize = qParams.PageSize;
            ViewBag.pageCount = qParams.PageCount;
        }

        protected virtual async Task PutCanUserInViewBag()
        {
            ViewBag.canUserEdit = await CanUserEdit();
            ViewBag.canUserCreate = await CanUserCreate();
            ViewBag.canUserDelete = await CanUserDelete();
            ViewBag.canSeeRestricted = await CanUserSeeRestricted();
        }
        //If we forget to implement override methods, we will keep it secure.
        protected virtual Task<bool> CanUserCreate() { return Task.FromResult(false); }
        protected virtual Task<bool> CanUserEdit() { return Task.FromResult(false); }
        protected virtual Task<bool> CanUserDelete() { return Task.FromResult(false); }
        protected virtual Task<bool> CanUserSeeRestricted() { return Task.FromResult(false); }

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


        protected virtual async Task<bool> IsUserSupplierAsync()
        {
            if (!_isUserSupplier.HasValue)
            {
                _isUserSupplier = Request.IsAuthenticated && 
                                    await UserManager.IsInRoleAsync(UserID, SecurityRoles.Supplier);
            }
            return _isUserSupplier.Value;
        }
        bool? _isUserSupplier;

        #region Query Methods for Entity Types

        /// <summary>
        /// Finds an AspNetUser by PrimaryKey value
        /// </summary>
        /// <param name="id">Represents PrimaryKey of AspNetUser.Id</param>
        /// <returns></returns>
        protected virtual async Task<AspNetUser> FindAsyncAspNetUser(string id)
        {
            return await GetAspNetUserQuery()
                            .FirstOrDefaultAsync(x => x.Id == id);
        }

        protected virtual IQueryable<AspNetUser> GetAspNetUserQuery()
        {
            return DataContext.GetAspNetUserQuery();
        }

        protected virtual Task SetAspNetUserDefaults(AspNetUser aspNetUser)
        {
            return Task.FromResult(default(object));
        }

        /// <summary>
        /// Finds an Employee by PrimaryKey value
        /// </summary>
        /// <param name="id">Represents PrimaryKey of Employee.Id</param>
        /// <returns></returns>
        protected virtual async Task<Employee> FindAsyncEmployee(int id)
        {
            return await GetEmployeeQuery()
                            .FirstOrDefaultAsync(x => x.Id == id);
        }

        protected virtual IQueryable<Employee> GetEmployeeQuery()
        {
            return DataContext.GetEmployeeQuery();
        }

        protected virtual Task SetEmployeeDefaults(EmployeeDTO employee)
        {
            return Task.FromResult(default(object));
        }

        /// <summary>
        /// Finds an FreightTerm by PrimaryKey value
        /// </summary>
        /// <param name="id">Represents PrimaryKey of FreightTerm.Id</param>
        /// <returns></returns>
        protected virtual async Task<FreightTerm> FindAsyncFreightTerm(int id)
        {
            return await GetFreightTermQuery()
                            .FirstOrDefaultAsync(x => x.Id == id);
        }

        protected virtual IQueryable<FreightTerm> GetFreightTermQuery()
        {
            return DataContext.GetFreightTermQuery();
        }

        protected virtual Task SetFreightTermDefaults(FreightTerm freightTerm)
        {
            return Task.FromResult(default(object));
        }

        /// <summary>
        /// Finds an Currency by PrimaryKey value
        /// </summary>
        /// <param name="id">Represents PrimaryKey of Currency.Id</param>
        /// <returns></returns>
        protected virtual async Task<Currency> FindAsyncCurrency(int id)
        {
            return await GetCurrencyQuery()
                            .FirstOrDefaultAsync(x => x.Id == id);
        }

        protected virtual IQueryable<Currency> GetCurrencyQuery()
        {
            return DataContext.GetCurrencyQuery();
        }

        protected virtual Task SetCurrencyDefaults(Currency currency)
        {
            return Task.FromResult(default(object));
        }

        /// <summary>
        /// Finds an Country by PrimaryKey value
        /// </summary>
        /// <param name="id">Represents PrimaryKey of Country.Id</param>
        /// <returns></returns>
        protected virtual async Task<Country> FindAsyncCountry(int id)
        {
            return await GetCountryQuery()
                            .FirstOrDefaultAsync(x => x.Id == id);
        }

        protected virtual IQueryable<Country> GetCountryQuery()
        {
            return DataContext.GetCountryQuery();
        }

        protected virtual Task SetCountryDefaults(Country country)
        {
            return Task.FromResult(default(object));
        }

        /// <summary>
        /// Finds an SizingStandard by PrimaryKey value
        /// </summary>
        /// <param name="id">Represents PrimaryKey of SizingStandard.Id</param>
        /// <returns></returns>
        protected virtual async Task<SizingStandard> FindAsyncSizingStandard(int id)
        {
            return await GetSizingStandardQuery()
                            .FirstOrDefaultAsync(x => x.Id == id);
        }

        protected virtual IQueryable<SizingStandard> GetSizingStandardQuery()
        {
            return DataContext.GetSizingStandardQuery();
        }

        protected virtual Task SetSizingStandardDefaults(SizingStandardVM sizingStandard)
        {
            return Task.FromResult(default(object));
        }

        /// <summary>
        /// Finds an Sizing by PrimaryKey value
        /// </summary>
        /// <param name="id">Represents PrimaryKey of Sizing.Id</param>
        /// <returns></returns>
        protected virtual async Task<Sizing> FindAsyncSizing(int id)
        {
            return await GetSizingQuery()
                            .FirstOrDefaultAsync(x => x.Id == id);
        }

        protected virtual IQueryable<Sizing> GetSizingQuery()
        {
            return DataContext.GetSizingQuery();
        }

        protected virtual Task SetSizingDefaults(Sizing sizing)
        {
            return Task.FromResult(default(object));
        }

        /// <summary>
        /// Finds an Contact by PrimaryKey value
        /// </summary>
        /// <param name="id">Represents PrimaryKey of Contact.Id</param>
        /// <returns></returns>
        protected virtual async Task<Contact> FindAsyncContact(int id)
        {
            return await GetContactQuery()
                            .FirstOrDefaultAsync(x => x.Id == id);
        }

        protected virtual IQueryable<Contact> GetContactQuery()
        {
            return DataContext.GetContactQuery();
        }

        protected virtual Task SetContactDefaults(Contact contact)
        {
            return Task.FromResult(default(object));
        }

        /// <summary>
        /// Finds an Customer by PrimaryKey value
        /// </summary>
        /// <param name="id">Represents PrimaryKey of Customer.Id</param>
        /// <returns></returns>
        protected virtual async Task<Customer> FindAsyncCustomer(int id)
        {
            return await GetCustomerQuery()
                            .FirstOrDefaultAsync(x => x.Id == id);
        }

        protected virtual IQueryable<Customer> GetCustomerQuery()
        {
            return DataContext.GetCustomerQuery();
        }

        protected virtual Task SetCustomerDefaults(Customer customer)
        {
            return Task.FromResult(default(object));
        }

        /// <summary>
        /// Finds an CustomerDepartment by PrimaryKey value
        /// </summary>
        /// <param name="id">Represents PrimaryKey of CustomerDepartment.Id</param>
        /// <returns></returns>
        protected virtual async Task<CustomerDepartment> FindAsyncCustomerDepartment(int id)
        {
            return await GetCustomerDepartmentQuery()
                            .FirstOrDefaultAsync(x => x.Id == id);
        }

        protected virtual IQueryable<CustomerDepartment> GetCustomerDepartmentQuery()
        {
            return DataContext.GetCustomerDepartmentQuery();
        }

        protected virtual Task SetCustomerDepartmentDefaults(CustomerDepartment customerDepartment)
        {
            return Task.FromResult(default(object));
        }

        /// <summary>
        /// Finds an Supplier by PrimaryKey value
        /// </summary>
        /// <param name="id">Represents PrimaryKey of Supplier.Id</param>
        /// <returns></returns>
        protected virtual async Task<Supplier> FindAsyncSupplier(int id)
        {
            return await GetSupplierQuery()
                            .FirstOrDefaultAsync(x => x.Id == id);
        }

        protected virtual IQueryable<Supplier> GetSupplierQuery()
        {
            return DataContext.GetSupplierQuery();
        }

        protected virtual Task SetSupplierDefaults(Supplier supplier)
        {
            return Task.FromResult(default(object));
        }

        /// <summary>
        /// Finds an Licensor by PrimaryKey value
        /// </summary>
        /// <param name="id">Represents PrimaryKey of Licensor.Id</param>
        /// <returns></returns>
        protected virtual async Task<Licensor> FindAsyncLicensor(int id)
        {
            return await GetLicensorQuery()
                            .FirstOrDefaultAsync(x => x.Id == id);
        }

        protected virtual IQueryable<Licensor> GetLicensorQuery()
        {
            return DataContext.GetLicensorQuery();
        }

        protected virtual Task SetLicensorDefaults(Licensor licensor)
        {
            return Task.FromResult(default(object));
        }

        /// <summary>
        /// Finds an Manufacturer by PrimaryKey value
        /// </summary>
        /// <param name="id">Represents PrimaryKey of Manufacturer.Id</param>
        /// <returns></returns>
        protected virtual async Task<Manufacturer> FindAsyncManufacturer(int id)
        {
            return await GetManufacturerQuery()
                            .FirstOrDefaultAsync(x => x.Id == id);
        }

        protected virtual IQueryable<Manufacturer> GetManufacturerQuery()
        {
            return DataContext.GetManufacturerQuery();
        }

        protected virtual Task SetManufacturerDefaults(Manufacturer manufacturer)
        {
            return Task.FromResult(default(object));
        }

        /// <summary>
        /// Finds an ProductCategory by PrimaryKey value
        /// </summary>
        /// <param name="id">Represents PrimaryKey of ProductCategory.Id</param>
        /// <returns></returns>
        protected virtual async Task<ProductCategory> FindAsyncProductCategory(int id)
        {
            return await GetProductCategoryQuery()
                            .FirstOrDefaultAsync(x => x.Id == id);
        }

        protected virtual IQueryable<ProductCategory> GetProductCategoryQuery()
        {
            return DataContext.GetProductCategoryQuery();
        }

        protected virtual Task SetProductCategoryDefaults(ProductCategory productCategory)
        {
            return Task.FromResult(default(object));
        }

        /// <summary>
        /// Finds an Product by PrimaryKey value
        /// </summary>
        /// <param name="id">Represents PrimaryKey of Product.Id</param>
        /// <returns></returns>
        protected virtual async Task<Product> FindAsyncProduct(int id)
        {
            return await GetProductQuery()
                            .FirstOrDefaultAsync(x => x.Id == id);
        }

        protected virtual IQueryable<Product> GetProductQuery()
        {
            return DataContext.GetProductQuery();
        }

        protected virtual Task SetProductDefaults(Product product)
        {
            return Task.FromResult(default(object));
        }

        /// <summary>
        /// Finds an PurchaseOrder by PrimaryKey value
        /// </summary>
        /// <param name="id">Represents PrimaryKey of PurchaseOrder.Id</param>
        /// <returns></returns>
        protected virtual async Task<PurchaseOrder> FindAsyncPurchaseOrder(int id)
        {
            return await GetPurchaseOrderQuery()
                            .FirstOrDefaultAsync(x => x.Id == id);
        }

        protected virtual IQueryable<PurchaseOrder> GetPurchaseOrderQuery()
        {
            return DataContext.GetPurchaseOrderQuery();
        }

        protected virtual Task SetPurchaseOrderDefaults(PurchaseOrderDTO purchaseOrder)
        {
            return Task.FromResult(default(object));
        }

        /// <summary>
        /// Finds an SizeRatio by PrimaryKey value
        /// </summary>
        /// <param name="id">Represents PrimaryKey of SizeRatio.Id</param>
        /// <returns></returns>
        protected virtual async Task<SizeRatio> FindAsyncSizeRatio(int id)
        {
            return await GetSizeRatioQuery()
                            .FirstOrDefaultAsync(x => x.Id == id);
        }

        protected virtual IQueryable<SizeRatio> GetSizeRatioQuery()
        {
            return DataContext.GetSizeRatioQuery();
        }

        protected virtual Task SetSizeRatioDefaults(SizeRatio sizeRatio)
        {
            return Task.FromResult(default(object));
        }

        /// <summary>
        /// Finds an Process by PrimaryKey value
        /// </summary>
        /// <param name="id">Represents PrimaryKey of Process.Id</param>
        /// <returns></returns>
        protected virtual async Task<Process> FindAsyncProcess(int id)
        {
            return await GetProcessQuery()
                            .FirstOrDefaultAsync(x => x.Id == id);
        }

        protected virtual IQueryable<Process> GetProcessQuery()
        {
            return DataContext.GetProcessQuery();
        }

        protected virtual Task SetProcessDefaults(Process process)
        {
            return Task.FromResult(default(object));
        }

        /// <summary>
        /// Finds an ProcessStep by PrimaryKey value
        /// </summary>
        /// <param name="id">Represents PrimaryKey of ProcessStep.Id</param>
        /// <returns></returns>
        protected virtual async Task<ProcessStep> FindAsyncProcessStep(int id)
        {
            return await GetProcessStepQuery()
                            .FirstOrDefaultAsync(x => x.Id == id);
        }

        protected virtual IQueryable<ProcessStep> GetProcessStepQuery()
        {
            return DataContext.GetProcessStepQuery();
        }

        protected virtual Task SetProcessStepDefaults(ProcessStep processStep)
        {
            return Task.FromResult(default(object));
        }

        /// <summary>
        /// Finds an ProcessStepRevision by PrimaryKey value
        /// </summary>
        /// <param name="id">Represents PrimaryKey of ProcessStepRevision.Id</param>
        /// <returns></returns>
        protected virtual async Task<ProcessStepRevision> FindAsyncProcessStepRevision(int id)
        {
            return await GetProcessStepRevisionQuery()
                            .FirstOrDefaultAsync(x => x.Id == id);
        }

        protected virtual IQueryable<ProcessStepRevision> GetProcessStepRevisionQuery()
        {
            return DataContext.GetProcessStepRevisionQuery();
        }

        protected virtual Task SetProcessStepRevisionDefaults(ProcessStepRevision processStepRevision)
        {
            return Task.FromResult(default(object));
        }

        /// <summary>
        /// Finds an ProcessTemplate by PrimaryKey value
        /// </summary>
        /// <param name="id">Represents PrimaryKey of ProcessTemplate.Id</param>
        /// <returns></returns>
        protected virtual async Task<ProcessTemplate> FindAsyncProcessTemplate(int id)
        {
            return await GetProcessTemplateQuery()
                            .FirstOrDefaultAsync(x => x.Id == id);
        }

        protected virtual IQueryable<ProcessTemplate> GetProcessTemplateQuery()
        {
            return DataContext.GetProcessTemplateQuery();
        }

        protected virtual Task SetProcessTemplateDefaults(ProcessTemplate processTemplate)
        {
            return Task.FromResult(default(object));
        }

        /// <summary>
        /// Finds an ProcessStepTemplate by PrimaryKey value
        /// </summary>
        /// <param name="id">Represents PrimaryKey of ProcessStepTemplate.Id</param>
        /// <returns></returns>
        protected virtual async Task<ProcessStepTemplate> FindAsyncProcessStepTemplate(int id)
        {
            return await GetProcessStepTemplateQuery()
                            .FirstOrDefaultAsync(x => x.Id == id);
        }

        protected virtual IQueryable<ProcessStepTemplate> GetProcessStepTemplateQuery()
        {
            return DataContext.GetProcessStepTemplateQuery();
        }

        protected virtual Task SetProcessStepTemplateDefaults(ProcessStepTemplate processStepTemplate)
        {
            return Task.FromResult(default(object));
        }
        #endregion
        
        protected void AppendExceptionMsg(Exception ex, StringBuilder sb)
        {
            if (ex == null)
                return;
            if (HttpContext.IsDebuggingEnabled)
            {
                sb.Append(ex.Message);
                sb.Append("<br/>");
                AppendExceptionMsg(ex.InnerException, sb);
            }
        }

        protected virtual ActionResult BadRequestTextResult()
        {
            return StatusCodeTextResult("HTTP Error 400.0 - Bad Request", HttpStatusCode.BadRequest);
        }

        protected virtual ActionResult NotFoundTextResult()
        {
            return StatusCodeTextResult("HTTP Error 404.0 - Not Found", HttpStatusCode.NotFound);
        }

        protected virtual ActionResult StatusCodeTextResult(string content, HttpStatusCode statusCode)
        {
            Response.StatusCode = (int)statusCode;
            return new ContentResult
            {
                ContentType = "text/plain",
                Content = content,
                ContentEncoding = Encoding.UTF8
            };
        }

        protected virtual ActionResult StatusCodeTextResult(StringBuilder sb, HttpStatusCode statusCode)
        {
            if (HttpContext.IsDebuggingEnabled)
            {
                sb.Append("<br/><b>RouteData.Values</b><br/>");
                foreach (var item in RouteData.Values)
                {
                    sb.Append(item.Key);
                    sb.Append(" : ");
                    sb.Append(item.Value);
                    sb.Append("<br/>");
                }
            }
            Response.StatusCode = (int)statusCode;
            return new ContentResult
            {
                ContentType = "text/plain",
                Content = sb.ToString(),
                ContentEncoding = Encoding.UTF8
            };
        }
        
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