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
    public partial class ManufacturersController : BaseCompaniesController 
    {
        protected virtual async Task<IQueryable<Manufacturer>> GetManufacturerQuery(QueryParameters qParams)
        {
            var query = GetManufacturerQuery();
            if (!string.IsNullOrEmpty(qParams.SearchString))
            {
                query = from a in query
                        where
                            a.CompanyName.Contains(qParams.SearchString) | 
                            a.City.Contains(qParams.SearchString) | 
                            a.State.Contains(qParams.SearchString) | 
                            a.ManufacturerCode.Contains(qParams.SearchString) 
                        select a;
            }
            if (qParams.CountryId != null)
            {
                query = query.Where(x => x.CountryId == qParams.CountryId);
            }
            if (qParams.SupplierId != null)
            {
                query = query.Where(x => x.SupplierId == qParams.SupplierId);
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

        protected virtual async Task<List<ManufacturerDTO>> GetManufacturerDtoList(QueryParameters qParams)
        {
            var query = await GetManufacturerQuery(qParams);
            var list = qParams.TotalCount > 0 ? await query.ToListAsync() : new List<Manufacturer>();
            var result = new List<ManufacturerDTO>();
            foreach (var item in list)
            {
                result.Add(new ManufacturerDTO(item));
            }

            return result;
        }

        [Authorize]
        public async Task<ActionResult> Index(QueryParameters qParams)
        {
            await PutCanUserInViewBag();
            var query = await GetManufacturerQuery(qParams);
            var result = new PagedList<Manufacturer>(qParams);
            if (qParams.TotalCount > 0)
            {
                result.Items = await query.ToListAsync();
            }

            PutPagerInViewBag(result);
            return View(result.Items);
        }

        [Authorize]
        public async Task<ActionResult> GetManufacturerList(QueryParameters qParams)
        {
            var result = await GetManufacturerDtoList(qParams);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public async Task<ActionResult> GetManufacturerPagedList(QueryParameters qParams)
        {
            var items = await GetManufacturerDtoList(qParams);
            var result = new PagedList<ManufacturerDTO>(qParams, items);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public async Task<JsonResult> GetManufacturersForAutoComplete(QueryParameters qParam)
        {
            var query = GetManufacturerQuery()
                        .Where(x => x.CompanyName.Contains(qParam.SearchString))
                        .Take(qParam.PageSize);
            var list = from x in query
                       select new
                       {
                           id = x.Id,
                           value = x.CompanyName,
                           label = x.CompanyName
                       };

            return Json(await list.ToListAsync(), JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public async Task<ActionResult> Details(int? id, bool? modal)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manufacturer manufacturer = await FindAsyncManufacturer(id.Value);

            if (manufacturer == null)
            {
                return HttpNotFound();
            }

            await PutCanUserInViewBag();
            if (modal ?? false)
            {
                return PartialView("_Details", manufacturer);
            }
            return View(manufacturer);
        }

        [Authorize]
        public async Task<ActionResult> GetManufacturer(int? id)
        {
            if (id == null)
            {
                return BadRequestTextResult();
            }
            Manufacturer manufacturer = await FindAsyncManufacturer(id.Value);

            if (manufacturer == null)
            {
                return NotFoundTextResult();
            }

            return Json(new ManufacturerDTO(manufacturer), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize(Roles = "admin, supervisor, clerk")]
        [Route("Manufacturers/Create/{supplierId:int?}")]
        public async Task<ActionResult> Create(int? supplierId, bool? modal)
        {
            var manufacturer = new Manufacturer();
            if (supplierId != null)
            {
                var supplier = await FindAsyncSupplier(supplierId.Value);
                if (supplier == null)
                    return HttpNotFound();
                manufacturer.Supplier = supplier;
                manufacturer.SupplierId = supplierId.Value;
            }
            await SetManufacturerDefaults(manufacturer);
            await SetCountrySelectList(manufacturer.CountryId);
            await SetSupplierSelectList(manufacturer.SupplierId);
            
            if (modal ?? false)
            {
                ViewBag.Modal = true;
                return PartialView("_Create", manufacturer);
            }
            return View(manufacturer);
        }

        [HttpPost]
        [Authorize(Roles = "admin, supervisor, clerk")]
        [ValidateAntiForgeryToken]
        [Route("Manufacturers/Create/{supplierId:int?}")]
        public async Task<ActionResult> Create(int? supplierId, Manufacturer manufacturer, bool? modal)
        {
            if (ModelState.IsValid)
            {
                OnCreateSaving(manufacturer);
 
                DataContext.Companies.Add(manufacturer);
                await DataContext.SaveChangesAsync(this);
 
                OnCreateSaved(manufacturer);
                if (modal ?? false)
                {
                    return Json(new { saved = true });
                }
                return RedirectToAction("Index");
            }

            await SetCountrySelectList(manufacturer.CountryId);
            await SetSupplierSelectList(manufacturer.SupplierId);
            
            if (modal ?? false)
            {
                ViewBag.Modal = true;
                return PartialView("_Create", manufacturer);
            }
            return View(manufacturer);
        }

        [Authorize(Roles = "admin, supervisor, clerk")]
        public async Task<ActionResult> Edit(int? id, bool? modal)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manufacturer manufacturer = await FindAsyncManufacturer(id.Value);

            if (manufacturer == null)
            {
                return HttpNotFound();
            }

            await SetCountrySelectList(manufacturer.CountryId);
            await SetSupplierSelectList(manufacturer.SupplierId);
            
            if (modal ?? false)
            {
                ViewBag.Modal = true;
                return PartialView("_Edit", manufacturer);
            }
            return View(manufacturer);
        }

        [Authorize(Roles = "admin, supervisor, clerk")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Manufacturer manufacturer, bool? modal)
        {
            if (ModelState.IsValid)
            {
                OnEditSaving(manufacturer);
 
                DataContext.Entry(manufacturer).State = EntityState.Modified;
                await DataContext.SaveChangesAsync(this);
 
                OnEditSaved(manufacturer);
                if (modal ?? false)
                {
                    return Json(new { saved = true });
                }
                return RedirectToAction("Index");
            }

            await SetCountrySelectList(manufacturer.CountryId);
            await SetSupplierSelectList(manufacturer.SupplierId);
            
            if (modal ?? false)
            {
                ViewBag.Modal = true;
                return PartialView("_Edit", manufacturer);
            }
            return View(manufacturer);
        }


        [Authorize(Roles = "admin, supervisor")]
        public async Task<ActionResult> Delete(int? id)  //GET: /Manufacturers
        {
            if (id == null)
            {
                return BadRequestTextResult();
            }
            Manufacturer manufacturer = await FindAsyncManufacturer(id.Value);

            if (manufacturer == null)
            {
                return NotFoundTextResult();
            }

            int contactsCount = manufacturer.Contacts.Count;
            if ((contactsCount) > 0)
            {
                var sb = new StringBuilder();

                sb.Append(MessageStrings.CanNotDelete);
                sb.Append(" <b>");
                sb.Append(manufacturer.CompanyName);
                sb.Append("</b>.<br/>");

                if (contactsCount > 0)
                {
                    sb.Append(string.Format(MessageStrings.RelatedRecordsExist, contactsCount, EntityStrings.Contacts));
                    sb.Append("<br/>");
                }

                return StatusCodeTextResult(sb, HttpStatusCode.BadRequest);
            }

            DataContext.Companies.Remove(manufacturer);
            try
            {
                await DataContext.SaveChangesAsync(this);
            }
            catch (Exception ex)
            {
                var sb = new StringBuilder();
                sb.Append(MessageStrings.CanNotDelete);
                sb.Append(manufacturer.CompanyName);
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
                SupplierId = parameters.SupplierId;
            }
            public int? CountryId { get; set; }
            public int? SupplierId { get; set; }
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
        partial void OnCreateSaving(Manufacturer manufacturer);
        partial void OnCreateSaved(Manufacturer manufacturer);
        partial void OnEditSaving(Manufacturer manufacturer);
        partial void OnEditSaved(Manufacturer manufacturer);
    }
}
