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

        [Authorize]
        public async Task<ActionResult> GetProcessList(QueryParameters qParams)
        {
            var result = await GetProcessDtoList(qParams);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public async Task<ActionResult> GetProcessPagedList(QueryParameters qParams)
        {
            var result = new PagedList<ProcessDTO>(qParams);
            result.Items = await GetProcessDtoList(qParams);
            return Json(result, JsonRequestBehavior.AllowGet);
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

        [HttpGet]
        [Authorize(Roles = "admin, supervisor, clerk")]
        [Route("Processes/Create/{purchaseOrderId:int?}")]
        public async Task<ActionResult> Create(int? purchaseOrderId)  //GET: /Processes/Create
        {
            var process = new Process();
            if (purchaseOrderId != null)
            {
                var purchaseOrder = await FindAsyncPurchaseOrder(purchaseOrderId.Value);
                if (purchaseOrder == null)
                    return HttpNotFound();
                process.PurchaseOrder = purchaseOrder;
            }
            await SetProcessDefaults(process);
            await SetSupplierSelectList(process);
            await SetProcessTemplateSelectList(process);
            return View(process);
        }

        [HttpPost]
        [Authorize(Roles = "admin, supervisor, clerk")]
        [ValidateAntiForgeryToken]
        [Route("Processes/Create/{purchaseOrderId:int?}")]
        public async Task<ActionResult> Create(int? purchaseOrderId, Process process)  //POST: /Processes/Create
        {
            DataContext.SetInsertDefaults(process, this);

            if (ModelState.IsValid)
            {
                OnCreateSaving(process);
 
                DataContext.Processes.Add(process);
                await DataContext.SaveChangesAsync(this);
 
                OnCreateSaved(process);
                return RedirectToAction("Index", "ProcessSteps", new { processId = process.Id, pageSize = process.ProcessSteps.Count });
            }

            await SetSupplierSelectList(process);
            await SetProcessTemplateSelectList(process);
            return View(process);
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

            await SetSupplierSelectList(process);
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

            await SetSupplierSelectList(process);
            await SetProcessTemplateSelectList(process);
            return View(process);
        }

        public new partial class QueryParameters : BaseController.QueryParameters
        {
            public QueryParameters() { }
            public QueryParameters(QueryParameters parameters) : base(parameters)
            {
                PurchaseOrderId = parameters.PurchaseOrderId;
                SupplierId = parameters.SupplierId;
                ProcessTemplateId = parameters.ProcessTemplateId;
            }
            public int? PurchaseOrderId { get; set; }
            public int? SupplierId { get; set; }
            public int? ProcessTemplateId { get; set; }
        }

        public partial class PagedList<T> : QueryParameters
        {
            public PagedList() { }
            public PagedList(QueryParameters parameters) : base(parameters) { }

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
        partial void OnCreateSaving(Process process);
        partial void OnCreateSaved(Process process);
        partial void OnEditSaving(Process process);
        partial void OnEditSaved(Process process);
    }
}
