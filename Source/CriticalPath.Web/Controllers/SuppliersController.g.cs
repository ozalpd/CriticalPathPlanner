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
    public partial class SuppliersController : BaseCompaniesController 
    {
        [Authorize]
        public async Task<ActionResult> Details(int? id, bool? modal)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = await FindAsyncSupplier(id.Value);

            if (supplier == null)
            {
                return HttpNotFound();
            }

            await PutCanUserInViewBag();
            if (modal ?? false)
            {
                return PartialView("_Details", supplier);
            }
            return View(supplier);
        }

        [Authorize]
        public async Task<ActionResult> GetSupplier(int? id)
        {
            if (id == null)
            {
                return BadRequestTextResult();
            }
            Supplier supplier = await FindAsyncSupplier(id.Value);

            if (supplier == null)
            {
                return NotFoundTextResult();
            }

            return Json(new SupplierDTO(supplier), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize(Roles = "admin, supervisor, clerk")]
        public async Task<ActionResult> Create(bool? modal)
        {
            var supplier = new Supplier();
            await SetSupplierDefaults(supplier);
            await SetCountrySelectList(supplier.CountryId);
            if (modal ?? false)
            {
                ViewBag.Modal = true;
                return PartialView("_Create", supplier);
            }
            return View(supplier);
        }

        [HttpPost]
        [Authorize(Roles = "admin, supervisor, clerk")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Supplier supplier, bool? modal)
        {
            if (ModelState.IsValid)
            {
                OnCreateSaving(supplier);
 
                DataContext.Companies.Add(supplier);
                await DataContext.SaveChangesAsync(this);
 
                OnCreateSaved(supplier);
                if (modal ?? false)
                {
                    return Json(new { saved = true });
                }
                return RedirectToAction("Index");
            }

            await SetCountrySelectList(supplier.CountryId);
            if (modal ?? false)
            {
                ViewBag.Modal = true;
                return PartialView("_Create", supplier);
            }
            return View(supplier);
        }

        [Authorize(Roles = "admin, supervisor, clerk")]
        public async Task<ActionResult> Edit(int? id, bool? modal)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = await FindAsyncSupplier(id.Value);

            if (supplier == null)
            {
                return HttpNotFound();
            }

            await SetCountrySelectList(supplier.CountryId);
            if (modal ?? false)
            {
                ViewBag.Modal = true;
                return PartialView("_Edit", supplier);
            }
            return View(supplier);
        }

        [Authorize(Roles = "admin, supervisor, clerk")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Supplier supplier, bool? modal)
        {
            if (ModelState.IsValid)
            {
                OnEditSaving(supplier);
 
                DataContext.Entry(supplier).State = EntityState.Modified;
                await DataContext.SaveChangesAsync(this);
 
                OnEditSaved(supplier);
                if (modal ?? false)
                {
                    return Json(new { saved = true });
                }
                return RedirectToAction("Index");
            }

            await SetCountrySelectList(supplier.CountryId);
            if (modal ?? false)
            {
                ViewBag.Modal = true;
                return PartialView("_Edit", supplier);
            }
            return View(supplier);
        }


        [Authorize(Roles = "admin, supervisor")]
        public async Task<ActionResult> Delete(int? id)  //GET: /Suppliers
        {
            if (id == null)
            {
                return BadRequestTextResult();
            }
            Supplier supplier = await FindAsyncSupplier(id.Value);

            if (supplier == null)
            {
                return NotFoundTextResult();
            }

            int contactsCount = supplier.Contacts.Count;
            int productsCount = supplier.Products.Count;
            int manufacturersCount = supplier.Manufacturers.Count;
            if ((contactsCount + productsCount + manufacturersCount) > 0)
            {
                var sb = new StringBuilder();

                sb.Append(MessageStrings.CanNotDelete);
                sb.Append(" <b>");
                sb.Append(supplier.CompanyName);
                sb.Append("</b>.<br/>");

                if (contactsCount > 0)
                {
                    sb.Append(string.Format(MessageStrings.RelatedRecordsExist, contactsCount, EntityStrings.Contacts));
                    sb.Append("<br/>");
                }

                if (productsCount > 0)
                {
                    sb.Append(string.Format(MessageStrings.RelatedRecordsExist, productsCount, EntityStrings.Products));
                    sb.Append("<br/>");
                }

                if (manufacturersCount > 0)
                {
                    sb.Append(string.Format(MessageStrings.RelatedRecordsExist, manufacturersCount, EntityStrings.Manufacturers));
                    sb.Append("<br/>");
                }

                return StatusCodeTextResult(sb, HttpStatusCode.BadRequest);
            }

            DataContext.Companies.Remove(supplier);
            try
            {
                await DataContext.SaveChangesAsync(this);
            }
            catch (Exception ex)
            {
                var sb = new StringBuilder();
                sb.Append(MessageStrings.CanNotDelete);
                sb.Append(supplier.CompanyName);
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
        partial void OnCreateSaving(Supplier supplier);
        partial void OnCreateSaved(Supplier supplier);
        partial void OnEditSaving(Supplier supplier);
        partial void OnEditSaved(Supplier supplier);
    }
}
