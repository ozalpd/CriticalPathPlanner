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
    public partial class ProcessesController : BaseController 
    {
        protected virtual async Task<List<ProcessDTO>> GetProcessDtoList(QueryParameters qParams)
        {
            var query = await GetProcessQuery(qParams);
            var list = qParams.TotalCount > 0 ? await query.ToListAsync() : new List<Process>();
            var result = new List<ProcessDTO>();
            foreach (var item in list)
            {
                result.Add(new ProcessDTO(item));
            }

            return result;
        }

        [Authorize]
        public async Task<ActionResult> Index(QueryParameters qParams)
        {
            var query = await GetProcessQuery(qParams);
            await PutCanUserInViewBag();
			var result = new PagedList<Process>(qParams);
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
        public async Task<ActionResult> GetProcessList(QueryParameters qParams)
        {
            var result = await GetProcessDtoList(qParams);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public async Task<ActionResult> GetProcessPagedList(QueryParameters qParams)
        {
            var items = await GetProcessDtoList(qParams);
            var result = new PagedList<ProcessDTO>(qParams, items);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public async Task<JsonResult> GetProcessesForAutoComplete(QueryParameters qParam)
        {
            var query = GetProcessQuery()
                        .Where(x => x.PurchaseOrder.PoNr.Contains(qParam.SearchString))
                        .Take(qParam.PageSize);
            var list = from x in query
                       select new
                       {
                           id = x.Id,
                           value = x.PurchaseOrder.PoNr,
                           label = x.PurchaseOrder.PoNr //can be extended as x.Category.CategoryName + "/" + x.PurchaseOrder.PoNr,
                       };

            return Json(await list.ToListAsync(), JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public async Task<ActionResult> Details(int? id)  //GET: /Processes/Details/5
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Process process = await FindAsyncProcess(id.Value);

            if (process == null)
            {
                return HttpNotFound();
            }

            await PutCanUserInViewBag();
            return View(process);
        }

        [Authorize]
        public async Task<ActionResult> GetProcess(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Process process = await FindAsyncProcess(id.Value);

            if (process == null)
            {
                return HttpNotFound();
            }

            return Json(new ProcessDTO(process), JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "admin, supervisor, clerk")]
        public async Task<ActionResult> Edit(int? id)  //GET: /Processes/Edit/5
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Process process = await FindAsyncProcess(id.Value);

            if (process == null)
            {
                return HttpNotFound();
            }

            await SetProcessTemplateSelectList(process);
            return View(process);
        }

        [Authorize(Roles = "admin, supervisor, clerk")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Process process)  //POST: /Processes/Edit/5
        {
            DataContext.SetInsertDefaults(process, this);

            if (ModelState.IsValid)
            {
                OnEditSaving(process);
 
                DataContext.Entry(process).State = EntityState.Modified;
                await DataContext.SaveChangesAsync(this);
 
                OnEditSaved(process);
                return RedirectToAction("Details", new { id = process.Id });
            }

            await SetProcessTemplateSelectList(process);
            return View(process);
        }

        public new partial class QueryParameters : BaseController.QueryParameters
        {
            public QueryParameters() { }
            public QueryParameters(QueryParameters parameters) : base(parameters)
            {
                PurchaseOrderId = parameters.PurchaseOrderId;
                ProcessTemplateId = parameters.ProcessTemplateId;
            }
            public int? PurchaseOrderId { get; set; }
            public int? ProcessTemplateId { get; set; }
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
        partial void OnEditSaving(Process process);
        partial void OnEditSaved(Process process);
    }
}
