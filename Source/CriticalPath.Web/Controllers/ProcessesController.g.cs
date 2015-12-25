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
            public bool? IsCompleted { get; set; }
            public bool? Cancelled { get; set; }
            public DateTime? StartDateMin { get; set; }
            public DateTime? StartDateMax { get; set; }
            public DateTime? TargetDateMin { get; set; }
            public DateTime? TargetDateMax { get; set; }
            public DateTime? ForecastDateMin { get; set; }
            public DateTime? ForecastDateMax { get; set; }
            public DateTime? RealizedDateMin { get; set; }
            public DateTime? RealizedDateMax { get; set; }
            public DateTime? CancelDateMin { get; set; }
            public DateTime? CancelDateMax { get; set; }
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
