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
    public partial class CustomersController : BaseCompaniesController 
    {
        protected virtual async Task<IQueryable<Customer>> GetCustomerQuery(QueryParameters qParams)
        {
            var query = GetCustomerQuery();
            if (!string.IsNullOrEmpty(qParams.SearchString))
            {
                query = from a in query
                        where
                            a.CompanyName.Contains(qParams.SearchString) | 
                            a.CustomerCode.Contains(qParams.SearchString) | 
                            a.City.Contains(qParams.SearchString) | 
                            a.State.Contains(qParams.SearchString) 
                        select a;
            }
            if (qParams.CountryId != null)
            {
                query = query.Where(x => x.CountryId == qParams.CountryId);
            }
            if (qParams.Discontinued != null)
            {
                query = query.Where(x => x.Discontinued == qParams.Discontinued.Value);
            }
            if (qParams.DiscontinueDateMin != null)
            {
                query = query.Where(x => x.DiscontinueDate >= qParams.DiscontinueDateMin.Value);
            }
            if (qParams.DiscontinueDateMax != null)
            {
                var maxDate = qParams.DiscontinueDateMax.Value.AddDays(1);
                query = query.Where(x => x.DiscontinueDate < maxDate);
            }

            qParams.TotalCount = await query.CountAsync();
            return query.Skip(qParams.Skip).Take(qParams.PageSize);
        }

        [Authorize(Roles = "admin, supervisor, clerk")]
        public async Task<ActionResult> GetCustomerList(QueryParameters qParams)
        {
            var result = await GetCustomerDtoList(qParams);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "admin, supervisor, clerk")]
        public async Task<ActionResult> GetCustomerPagedList(QueryParameters qParams)
        {
            var items = await GetCustomerDtoList(qParams);
            var result = new PagedList<CustomerDTO>(qParams, items);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "admin, supervisor, clerk")]
        public async Task<ActionResult> Details(int? id, bool? modal)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = await FindAsyncCustomer(id.Value);

            if (customer == null)
            {
                return HttpNotFound();
            }

            await PutCanUserInViewBag();
            if (modal ?? false)
            {
                return PartialView("_Details", customer);
            }
            return View(customer);
        }

        [Authorize(Roles = "admin, supervisor, clerk")]
        public async Task<ActionResult> GetCustomer(int? id)
        {
            if (id == null)
            {
                return BadRequestTextResult();
            }
            Customer customer = await FindAsyncCustomer(id.Value);

            if (customer == null)
            {
                return NotFoundTextResult();
            }

            return Json(new CustomerDTO(customer), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize(Roles = "admin, supervisor, clerk")]
        public async Task<ActionResult> Create(bool? modal)
        {
            var customer = new Customer();
            await SetCustomerDefaults(customer);
            await SetCountrySelectList(customer.CountryId);
            if (modal ?? false)
            {
                ViewBag.Modal = true;
                return PartialView("_Create", customer);
            }
            return View(customer);
        }

        [HttpPost]
        [Authorize(Roles = "admin, supervisor, clerk")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Customer customer, bool? modal)
        {
            if (ModelState.IsValid)
            {
                OnCreateSaving(customer);
 
                DataContext.Companies.Add(customer);
                await DataContext.SaveChangesAsync(this);
 
                OnCreateSaved(customer);
                if (modal ?? false)
                {
                    return Json(new { saved = true });
                }
                return RedirectToAction("Index");
            }

            await SetCountrySelectList(customer.CountryId);
            if (modal ?? false)
            {
                ViewBag.Modal = true;
                return PartialView("_Create", customer);
            }
            return View(customer);
        }

        [Authorize(Roles = "admin, supervisor, clerk")]
        public async Task<ActionResult> Edit(int? id, bool? modal)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = await FindAsyncCustomer(id.Value);

            if (customer == null)
            {
                return HttpNotFound();
            }

            await SetCountrySelectList(customer.CountryId);
            if (modal ?? false)
            {
                ViewBag.Modal = true;
                return PartialView("_Edit", customer);
            }
            return View(customer);
        }

        [Authorize(Roles = "admin, supervisor, clerk")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Customer customer, bool? modal)
        {
            if (ModelState.IsValid)
            {
                OnEditSaving(customer);
 
                DataContext.Entry(customer).State = EntityState.Modified;
                await DataContext.SaveChangesAsync(this);
 
                OnEditSaved(customer);
                if (modal ?? false)
                {
                    return Json(new { saved = true });
                }
                return RedirectToAction("Index");
            }

            await SetCountrySelectList(customer.CountryId);
            if (modal ?? false)
            {
                ViewBag.Modal = true;
                return PartialView("_Edit", customer);
            }
            return View(customer);
        }


        [Authorize(Roles = "admin, supervisor")]
        public async Task<ActionResult> Delete(int? id)  //GET: /Customers
        {
            if (id == null)
            {
                return BadRequestTextResult();
            }
            Customer customer = await FindAsyncCustomer(id.Value);

            if (customer == null)
            {
                return NotFoundTextResult();
            }

            int contactsCount = customer.Contacts.Count;
            int ordersCount = customer.Orders.Count;
            int departmentsCount = customer.Departments.Count;
            if ((contactsCount + ordersCount + departmentsCount) > 0)
            {
                var sb = new StringBuilder();

                sb.Append(MessageStrings.CanNotDelete);
                sb.Append(" <b>");
                sb.Append(customer.CompanyName);
                sb.Append("</b>.<br/>");

                if (contactsCount > 0)
                {
                    sb.Append(string.Format(MessageStrings.RelatedRecordsExist, contactsCount, EntityStrings.Contacts));
                    sb.Append("<br/>");
                }

                if (ordersCount > 0)
                {
                    sb.Append(string.Format(MessageStrings.RelatedRecordsExist, ordersCount, EntityStrings.Orders));
                    sb.Append("<br/>");
                }

                if (departmentsCount > 0)
                {
                    sb.Append(string.Format(MessageStrings.RelatedRecordsExist, departmentsCount, EntityStrings.Departments));
                    sb.Append("<br/>");
                }

                return StatusCodeTextResult(sb, HttpStatusCode.BadRequest);
            }

            DataContext.Companies.Remove(customer);
            try
            {
                await DataContext.SaveChangesAsync(this);
            }
            catch (Exception ex)
            {
                var sb = new StringBuilder();
                sb.Append(MessageStrings.CanNotDelete);
                sb.Append(customer.CompanyName);
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

        
        protected override bool CanUserSeeRestricted() { return true; }
        protected override Task<bool> CanUserSeeRestrictedAsync() { return Task.FromResult(true); }


        public new partial class QueryParameters : BaseController.QueryParameters
        {
            public QueryParameters() { }
            public QueryParameters(QueryParameters parameters) : base(parameters)
            {
                CountryId = parameters.CountryId;
            }
            public int? CountryId { get; set; }
            public bool? Discontinued { get; set; }
            public DateTime? DiscontinueDateMin { get; set; }
            public DateTime? DiscontinueDateMax { get; set; }
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
        partial void OnCreateSaving(Customer customer);
        partial void OnCreateSaved(Customer customer);
        partial void OnEditSaving(Customer customer);
        partial void OnEditSaved(Customer customer);
    }
}
