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
    public partial class CustomersController : BaseController 
    {
        protected virtual async Task<IQueryable<Customer>> GetCustomerQuery(QueryParameters qParams)
        {
            var query = GetCustomerQuery();
            if (!string.IsNullOrEmpty(qParams.SearchString))
            {
                query = from a in query
                        where
                            a.CompanyName.Contains(qParams.SearchString) | 
                            a.CustomerCode.Contains(qParams.SearchString) 
                        select a;
            }

            qParams.TotalCount = await query.CountAsync();
            return query.Skip(qParams.Skip).Take(qParams.PageSize);
        }

        protected virtual async Task<List<CustomerDTO>> GetCustomerDtoList(QueryParameters qParams)
        {
            var query = await GetCustomerQuery(qParams);
            var list = qParams.TotalCount > 0 ? await query.ToListAsync() : new List<Customer>();
            var result = new List<CustomerDTO>();
            foreach (var item in list)
            {
                result.Add(new CustomerDTO(item));
            }

            return result;
        }

        [Authorize]
        public async Task<ActionResult> Index(QueryParameters qParams)
        {
            var query = await GetCustomerQuery(qParams);
            await PutCanUserInViewBag();
			var result = new PagedList<Customer>(qParams);
            if (qParams.TotalCount > 0)
            {
                result.Items = await query.ToListAsync();
            }

            PutPagerInViewBag(result);
            return View(result.Items);
        }
        
        protected override async Task<bool> CanUserCreate()
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

        protected override async Task<bool> CanUserEdit()
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
        
        protected override async Task<bool> CanUserDelete()
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

        protected override async Task<bool> CanUserSeeRestricted()
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

        [Authorize]
        public async Task<ActionResult> GetCustomerList(QueryParameters qParams)
        {
            var result = await GetCustomerDtoList(qParams);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public async Task<ActionResult> GetCustomerPagedList(QueryParameters qParams)
        {
            var items = await GetCustomerDtoList(qParams);
            var result = new PagedList<CustomerDTO>(qParams, items);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public async Task<ActionResult> Details(int? id)  //GET: /Customers/Details/5
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
            return View(customer);
        }

        [Authorize]
        public async Task<ActionResult> GetCustomer(int? id)
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

            return Json(new CustomerDTO(customer), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize(Roles = "admin, supervisor, clerk")]
        public async Task<ActionResult> Create()  //GET: /Customers/Create
        {
            var customer = new Customer();
            await SetCustomerDefaults(customer);
            SetSelectLists(customer);
            return View(customer);
        }

        [HttpPost]
        [Authorize(Roles = "admin, supervisor, clerk")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Customer customer)  //POST: /Customers/Create
        {
            DataContext.SetInsertDefaults(customer, this);

            if (ModelState.IsValid)
            {
                OnCreateSaving(customer);
 
                DataContext.Companies.Add(customer);
                await DataContext.SaveChangesAsync(this);
 
                OnCreateSaved(customer);
                return RedirectToAction("Index");
            }

            SetSelectLists(customer);
            return View(customer);
        }

        [Authorize(Roles = "admin, supervisor, clerk")]
        public async Task<ActionResult> Edit(int? id)  //GET: /Customers/Edit/5
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

            SetSelectLists(customer);
            return View(customer);
        }

        [Authorize(Roles = "admin, supervisor, clerk")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Customer customer)  //POST: /Customers/Edit/5
        {
            DataContext.SetInsertDefaults(customer, this);

            if (ModelState.IsValid)
            {
                OnEditSaving(customer);
 
                DataContext.Entry(customer).State = EntityState.Modified;
                await DataContext.SaveChangesAsync(this);
 
                OnEditSaved(customer);
                return RedirectToAction("Index");
            }

            SetSelectLists(customer);
            return View(customer);
        }


        [Authorize(Roles = "admin, supervisor")]
        public async Task<ActionResult> Delete(int? id)  //GET: /Customers/Delete/5
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

            int ordersCount = customer.Orders.Count;
            int contactsCount = customer.Contacts.Count;
            if ((ordersCount + contactsCount) > 0)
            {
                var sb = new StringBuilder();

                sb.Append(MessageStrings.CanNotDelete);
                sb.Append(" <b>");
                sb.Append(customer.CompanyName);
                sb.Append("</b>.<br/>");

                if (ordersCount > 0)
                {
                    sb.Append(string.Format(MessageStrings.RelatedRecordsExist, ordersCount, EntityStrings.Orders));
                    sb.Append("<br/>");
                }

                if (contactsCount > 0)
                {
                    sb.Append(string.Format(MessageStrings.RelatedRecordsExist, contactsCount, EntityStrings.Contacts));
                    sb.Append("<br/>");
                }

                return GetErrorResult(sb, HttpStatusCode.BadRequest);
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

                return GetErrorResult(sb, HttpStatusCode.InternalServerError);
            }

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        public new partial class QueryParameters : BaseController.QueryParameters
        {
            public QueryParameters() { }
            public QueryParameters(QueryParameters parameters) : base(parameters)
            {
            }
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
        partial void SetSelectLists(Customer customer);
    }
}
