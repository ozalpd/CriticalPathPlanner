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

namespace CriticalPath.Web.Controllers
{
    public partial class CustomerDepartmentsController : BaseController 
    {
        protected virtual async Task<IQueryable<CustomerDepartment>> GetCustomerDepartmentQuery(QueryParameters qParams)
        {
            var query = GetCustomerDepartmentQuery();
            if (!string.IsNullOrEmpty(qParams.SearchString))
            {
                query = from a in query
                        where
                            a.DepartmentName.Contains(qParams.SearchString) 
                        select a;
            }
            if (qParams.CustomerId != null)
            {
                query = query.Where(x => x.CustomerId == qParams.CustomerId);
            }

            qParams.TotalCount = await query.CountAsync();
            return query.Skip(qParams.Skip).Take(qParams.PageSize);
        }

        protected virtual async Task<List<CustomerDepartmentDTO>> GetCustomerDepartmentDtoList(QueryParameters qParams)
        {
            var query = await GetCustomerDepartmentQuery(qParams);
            var list = qParams.TotalCount > 0 ? await query.ToListAsync() : new List<CustomerDepartment>();
            var result = new List<CustomerDepartmentDTO>();
            foreach (var item in list)
            {
                result.Add(new CustomerDepartmentDTO(item));
            }

            return result;
        }

        [Authorize]
        public async Task<ActionResult> Index(QueryParameters qParams)
        {
            await PutCanUserInViewBag();
            var query = await GetCustomerDepartmentQuery(qParams);
            var result = new PagedList<CustomerDepartment>(qParams);
            if (qParams.TotalCount > 0)
            {
                result.Items = await query.ToListAsync();
            }

            PutPagerInViewBag(result);
            return View(result.Items);
        }

        [Authorize]
        public async Task<ActionResult> GetCustomerDepartmentList(QueryParameters qParams)
        {
            var result = await GetCustomerDepartmentDtoList(qParams);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public async Task<ActionResult> GetCustomerDepartmentPagedList(QueryParameters qParams)
        {
            var items = await GetCustomerDepartmentDtoList(qParams);
            var result = new PagedList<CustomerDepartmentDTO>(qParams, items);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public async Task<JsonResult> GetCustomerDepartmentsForAutoComplete(QueryParameters qParam)
        {
            var query = GetCustomerDepartmentQuery()
                        .Where(x => x.DepartmentName.Contains(qParam.SearchString))
                        .Take(qParam.PageSize);
            var list = from x in query
                       select new
                       {
                           id = x.Id,
                           value = x.DepartmentName,
                           label = x.DepartmentName
                       };

            return Json(await list.ToListAsync(), JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public async Task<ActionResult> GetCustomerDepartment(int? id)
        {
            if (id == null)
            {
                return BadRequestTextResult();
            }
            CustomerDepartment customerDepartment = await FindAsyncCustomerDepartment(id.Value);

            if (customerDepartment == null)
            {
                return NotFoundTextResult();
            }

            return Json(new CustomerDepartmentDTO(customerDepartment), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize(Roles = "admin, supervisor, clerk")]
        [Route("CustomerDepartments/Create/{customerId:int?}")]
        public async Task<ActionResult> Create(int? customerId, bool? modal)
        {
            var customerDepartment = new CustomerDepartment();
            if (customerId != null)
            {
                var customer = await FindAsyncCustomer(customerId.Value);
                if (customer == null)
                    return HttpNotFound();
                customerDepartment.Customer = customer;
                customerDepartment.CustomerId = customerId.Value;
            }
            await SetCustomerDepartmentDefaults(customerDepartment);
            SetSelectLists(customerDepartment);
            if (modal ?? false)
            {
                ViewBag.Modal = true;
                return PartialView("_Create", customerDepartment);
            }
            return View(customerDepartment);
        }

        [HttpPost]
        [Authorize(Roles = "admin, supervisor, clerk")]
        [ValidateAntiForgeryToken]
        [Route("CustomerDepartments/Create/{customerId:int?}")]
        public async Task<ActionResult> Create(int? customerId, CustomerDepartment customerDepartment, bool? modal)
        {
            if (ModelState.IsValid)
            {
                OnCreateSaving(customerDepartment);
 
                DataContext.CustomerDepartments.Add(customerDepartment);
                await DataContext.SaveChangesAsync(this);
 
                OnCreateSaved(customerDepartment);
                if (modal ?? false)
                {
                    return Json(new { saved = true });
                }
                return RedirectToAction("Index", new { customerId = customerDepartment.CustomerId });
            }

            SetSelectLists(customerDepartment);
            if (modal ?? false)
            {
                ViewBag.Modal = true;
                return PartialView("_Create", customerDepartment);
            }
            return View(customerDepartment);
        }

        [Authorize(Roles = "admin, supervisor, clerk")]
        public async Task<ActionResult> Edit(int? id, bool? modal)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerDepartment customerDepartment = await FindAsyncCustomerDepartment(id.Value);

            if (customerDepartment == null)
            {
                return HttpNotFound();
            }

            SetSelectLists(customerDepartment);
            if (modal ?? false)
            {
                ViewBag.Modal = true;
                return PartialView("_Edit", customerDepartment);
            }
            return View(customerDepartment);
        }

        [Authorize(Roles = "admin, supervisor, clerk")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CustomerDepartment customerDepartment, bool? modal)
        {
            if (ModelState.IsValid)
            {
                OnEditSaving(customerDepartment);
 
                DataContext.Entry(customerDepartment).State = EntityState.Modified;
                await DataContext.SaveChangesAsync(this);
 
                OnEditSaved(customerDepartment);
                if (modal ?? false)
                {
                    return Json(new { saved = true });
                }
                return RedirectToAction("Index", new { customerId = customerDepartment.CustomerId });
            }

            SetSelectLists(customerDepartment);
            if (modal ?? false)
            {
                ViewBag.Modal = true;
                return PartialView("_Edit", customerDepartment);
            }
            return View(customerDepartment);
        }


        [Authorize(Roles = "admin, supervisor")]
        public async Task<ActionResult> Delete(int? id)  //GET: /CustomerDepartments
        {
            if (id == null)
            {
                return BadRequestTextResult();
            }
            CustomerDepartment customerDepartment = await FindAsyncCustomerDepartment(id.Value);

            if (customerDepartment == null)
            {
                return NotFoundTextResult();
            }

            DataContext.CustomerDepartments.Remove(customerDepartment);
            try
            {
                await DataContext.SaveChangesAsync(this);
            }
            catch (Exception ex)
            {
                var sb = new StringBuilder();
                sb.Append(MessageStrings.CanNotDelete);
                sb.Append(customerDepartment.DepartmentName);
                sb.Append("<br/>");
                AppendExceptionMsg(ex, sb);

                return StatusCodeTextResult(sb, HttpStatusCode.InternalServerError);
            }

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        protected override bool CanUserCreate()
        {
            if (!_canUserCreate.HasValue)
            {
                _canUserCreate = Request.IsAuthenticated && (
                                    IsUserAdmin() ||
                                    IsUserSupervisor() ||
                                    IsUserClerk());
            }
            return _canUserCreate.Value;
        }
        protected override async Task<bool> CanUserCreateAsync()
        {
            if (!_canUserCreate.HasValue)
            {
                _canUserCreate = Request.IsAuthenticated && (
                                    await IsUserAdminAsync() ||
                                    await IsUserSupervisorAsync() ||
                                    await IsUserClerkAsync());
            }
            return _canUserCreate.Value;
        }
        bool? _canUserCreate;

        protected override bool CanUserEdit()
        {
            if (!_canUserEdit.HasValue)
            {
                _canUserEdit = Request.IsAuthenticated && (
                                    IsUserAdmin() ||
                                    IsUserSupervisor() ||
                                    IsUserClerk());
            }
            return _canUserEdit.Value;
        }
        protected override async Task<bool> CanUserEditAsync()
        {
            if (!_canUserEdit.HasValue)
            {
                _canUserEdit = Request.IsAuthenticated && (
                                    await IsUserAdminAsync() ||
                                    await IsUserSupervisorAsync() ||
                                    await IsUserClerkAsync());
            }
            return _canUserEdit.Value;
        }
        bool? _canUserEdit;
        
        protected override bool CanUserDelete()
        {
            if (!_canUserDelete.HasValue)
            {
                _canUserDelete = Request.IsAuthenticated && (
                                    IsUserAdmin() ||
                                    IsUserSupervisor());
            }
            return _canUserDelete.Value;
        }
        protected override async Task<bool> CanUserDeleteAsync()
        {
            if (!_canUserDelete.HasValue)
            {
                _canUserDelete = Request.IsAuthenticated && (
                                    await IsUserAdminAsync() ||
                                    await IsUserSupervisorAsync());
            }
            return _canUserDelete.Value;
        }
        bool? _canUserDelete;

        protected override bool CanUserSeeRestricted()
        {
            if (!_canSeeRestricted.HasValue)
            {
                _canSeeRestricted = Request.IsAuthenticated && (
                                    IsUserAdmin() ||
                                    IsUserSupervisor() ||
                                    IsUserClerk());
            }
            return _canSeeRestricted.Value;
        }
        protected override async Task<bool> CanUserSeeRestrictedAsync()
        {
            if (!_canSeeRestricted.HasValue)
            {
                _canSeeRestricted = Request.IsAuthenticated && (
                                    await IsUserAdminAsync() ||
                                    await IsUserSupervisorAsync() ||
                                    await IsUserClerkAsync());
            }
            return _canSeeRestricted.Value;
        }
        bool? _canSeeRestricted;



        public new partial class QueryParameters : BaseController.QueryParameters
        {
            public QueryParameters() { }
            public QueryParameters(QueryParameters parameters) : base(parameters)
            {
                CustomerId = parameters.CustomerId;
            }
            public int? CustomerId { get; set; }
        }

        public partial class PagedList<T> : QueryParameters
        {
            public PagedList() { }
            public PagedList(QueryParameters parameters) : base(parameters) { }
            public PagedList(QueryParameters parameters, IEnumerable<T> items) : this(parameters)
            {
                Items = items;
            }

            public IEnumerable<T> Items
            {
                set { _items = value; }
                get
                {
                    if (_items == null)
                    {
                        _items = new List<T>();
                    }
                    return _items;
                }
            }
            IEnumerable<T> _items;
        }
        partial void OnCreateSaving(CustomerDepartment customerDepartment);
        partial void OnCreateSaved(CustomerDepartment customerDepartment);
        partial void OnEditSaving(CustomerDepartment customerDepartment);
        partial void OnEditSaved(CustomerDepartment customerDepartment);
        partial void SetSelectLists(CustomerDepartment customerDepartment);
    }
}
