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
    public partial class LicensorsController : BaseController 
    {
        protected virtual async Task<IQueryable<Licensor>> GetLicensorQuery(QueryParameters qParams)
        {
            var query = GetLicensorQuery();
            if (!string.IsNullOrEmpty(qParams.SearchString))
            {
                query = from a in query
                        where
                            a.LicensorCode.Contains(qParams.SearchString) | 
                            a.CompanyName.Contains(qParams.SearchString) | 
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
                query = query.Where(x => x.DiscontinueDate <= qParams.DiscontinueDateMax.Value);
            }

            qParams.TotalCount = await query.CountAsync();
            return query.Skip(qParams.Skip).Take(qParams.PageSize);
        }

        protected virtual async Task<List<LicensorDTO>> GetLicensorDtoList(QueryParameters qParams)
        {
            var query = await GetLicensorQuery(qParams);
            var list = qParams.TotalCount > 0 ? await query.ToListAsync() : new List<Licensor>();
            var result = new List<LicensorDTO>();
            foreach (var item in list)
            {
                result.Add(new LicensorDTO(item));
            }

            return result;
        }

        [Authorize]
        public async Task<ActionResult> Index(QueryParameters qParams)
        {
            var query = await GetLicensorQuery(qParams);
            await PutCanUserInViewBag();
			var result = new PagedList<Licensor>(qParams);
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
        public async Task<ActionResult> GetLicensorList(QueryParameters qParams)
        {
            var result = await GetLicensorDtoList(qParams);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public async Task<ActionResult> GetLicensorPagedList(QueryParameters qParams)
        {
            var items = await GetLicensorDtoList(qParams);
            var result = new PagedList<LicensorDTO>(qParams, items);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public async Task<JsonResult> GetLicensorsForAutoComplete(QueryParameters qParam)
        {
            var query = GetLicensorQuery()
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
        public async Task<ActionResult> Details(int? id)  //GET: /Licensors/Details/5
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Licensor licensor = await FindAsyncLicensor(id.Value);

            if (licensor == null)
            {
                return HttpNotFound();
            }

            await PutCanUserInViewBag();
            return View(licensor);
        }

        [Authorize]
        public async Task<ActionResult> GetLicensor(int? id)
        {
            if (id == null)
            {
                return BadRequestTextResult();
            }
            Licensor licensor = await FindAsyncLicensor(id.Value);

            if (licensor == null)
            {
                return NotFoundTextResult();
            }

            return Json(new LicensorDTO(licensor), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize(Roles = "admin, supervisor, clerk")]
        public async Task<ActionResult> Create()  //GET: /Licensors/Create
        {
            var licensor = new Licensor();
            await SetLicensorDefaults(licensor);
            SetSelectLists(licensor);
            return View(licensor);
        }

        [HttpPost]
        [Authorize(Roles = "admin, supervisor, clerk")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Licensor licensor)  //POST: /Licensors/Create
        {
            DataContext.SetInsertDefaults(licensor, this);

            if (ModelState.IsValid)
            {
                OnCreateSaving(licensor);
 
                DataContext.Companies.Add(licensor);
                await DataContext.SaveChangesAsync(this);
 
                OnCreateSaved(licensor);
                return RedirectToAction("Index");
            }

            SetSelectLists(licensor);
            return View(licensor);
        }

        [Authorize(Roles = "admin, supervisor, clerk")]
        public async Task<ActionResult> Edit(int? id)  //GET: /Licensors/Edit/5
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Licensor licensor = await FindAsyncLicensor(id.Value);

            if (licensor == null)
            {
                return HttpNotFound();
            }

            SetSelectLists(licensor);
            return View(licensor);
        }

        [Authorize(Roles = "admin, supervisor, clerk")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Licensor licensor)  //POST: /Licensors/Edit/5
        {
            DataContext.SetInsertDefaults(licensor, this);

            if (ModelState.IsValid)
            {
                OnEditSaving(licensor);
 
                DataContext.Entry(licensor).State = EntityState.Modified;
                await DataContext.SaveChangesAsync(this);
 
                OnEditSaved(licensor);
                return RedirectToAction("Index");
            }

            SetSelectLists(licensor);
            return View(licensor);
        }


        [Authorize(Roles = "admin, supervisor")]
        public async Task<ActionResult> Delete(int? id)  //GET: /Licensors/Delete/5
        {
            if (id == null)
            {
                return BadRequestTextResult();
            }
            Licensor licensor = await FindAsyncLicensor(id.Value);

            if (licensor == null)
            {
                return NotFoundTextResult();
            }

            int purchaseOrdersCount = licensor.PurchaseOrders.Count;
            int contactsCount = licensor.Contacts.Count;
            if ((purchaseOrdersCount + contactsCount) > 0)
            {
                var sb = new StringBuilder();

                sb.Append(MessageStrings.CanNotDelete);
                sb.Append(" <b>");
                sb.Append(licensor.CompanyName);
                sb.Append("</b>.<br/>");

                if (purchaseOrdersCount > 0)
                {
                    sb.Append(string.Format(MessageStrings.RelatedRecordsExist, purchaseOrdersCount, EntityStrings.PurchaseOrders));
                    sb.Append("<br/>");
                }

                if (contactsCount > 0)
                {
                    sb.Append(string.Format(MessageStrings.RelatedRecordsExist, contactsCount, EntityStrings.Contacts));
                    sb.Append("<br/>");
                }

                return StatusCodeTextResult(sb, HttpStatusCode.BadRequest);
            }

            DataContext.Companies.Remove(licensor);
            try
            {
                await DataContext.SaveChangesAsync(this);
            }
            catch (Exception ex)
            {
                var sb = new StringBuilder();
                sb.Append(MessageStrings.CanNotDelete);
                sb.Append(licensor.CompanyName);
                sb.Append("<br/>");
                AppendExceptionMsg(ex, sb);

                return StatusCodeTextResult(sb, HttpStatusCode.InternalServerError);
            }

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

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
        partial void OnCreateSaving(Licensor licensor);
        partial void OnCreateSaved(Licensor licensor);
        partial void OnEditSaving(Licensor licensor);
        partial void OnEditSaved(Licensor licensor);
        partial void SetSelectLists(Licensor licensor);
    }
}
