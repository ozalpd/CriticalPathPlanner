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
    public partial class ProcessStepsController : BaseController 
    {
        protected virtual async Task<IQueryable<ProcessStep>> GetProcessStepQuery(QueryParameters qParams)
        {
            var query = GetProcessStepQuery();
            if (!string.IsNullOrEmpty(qParams.SearchString))
            {
                query = from a in query
                        where
                            a.Title.Contains(qParams.SearchString) | 
                            a.Description.Contains(qParams.SearchString) 
                        select a;
            }
            if (qParams.ProcessId != null)
            {
                query = query.Where(x => x.ProcessId == qParams.ProcessId);
            }
            if (qParams.TemplateId != null)
            {
                query = query.Where(x => x.TemplateId == qParams.TemplateId);
            }

            qParams.TotalCount = await query.CountAsync();
            return query.Skip(qParams.Skip).Take(qParams.PageSize);
        }

        protected virtual async Task<List<ProcessStepDTO>> GetProcessStepDtoList(QueryParameters qParams)
        {
            var query = await GetProcessStepQuery(qParams);
            var list = qParams.TotalCount > 0 ? await query.ToListAsync() : new List<ProcessStep>();
            var result = new List<ProcessStepDTO>();
            foreach (var item in list)
            {
                result.Add(new ProcessStepDTO(item));
            }

            return result;
        }

        [Authorize(Roles = "admin, supervisor, clerk")]
        public async Task<ActionResult> Index(QueryParameters qParams)
        {
            var query = await GetProcessStepQuery(qParams);
            await PutCanUserInViewBag();
			var result = new PagedList<ProcessStep>(qParams);
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
                                    await IsUserAdminAsync());
            }
            return _canUserDelete.Value;
        }
        bool? _canUserDelete;

        [Authorize(Roles = "admin, supervisor, clerk")]
        public async Task<ActionResult> GetProcessStepList(QueryParameters qParams)
        {
            var result = await GetProcessStepDtoList(qParams);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "admin, supervisor, clerk")]
        public async Task<ActionResult> GetProcessStepPagedList(QueryParameters qParams)
        {
            var result = new PagedList<ProcessStepDTO>(qParams);
            result.Items = await GetProcessStepDtoList(qParams);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "admin, supervisor, clerk")]
        public async Task<ActionResult> Details(int? id)  //GET: /ProcessSteps/Details/5
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProcessStep processStep = await FindAsyncProcessStep(id.Value);

            if (processStep == null)
            {
                return HttpNotFound();
            }

            await PutCanUserInViewBag();
            return View(processStep);
        }

        [Authorize(Roles = "admin, supervisor, clerk")]
        public async Task<ActionResult> GetProcessStep(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProcessStep processStep = await FindAsyncProcessStep(id.Value);

            if (processStep == null)
            {
                return HttpNotFound();
            }

            return Json(new ProcessStepDTO(processStep), JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "admin, supervisor, clerk")]
        public async Task<ActionResult> Edit(int? id)  //GET: /ProcessSteps/Edit/5
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProcessStep processStep = await FindAsyncProcessStep(id.Value);

            if (processStep == null)
            {
                return HttpNotFound();
            }

            SetSelectLists(processStep);
            return View(processStep);
        }

        [Authorize(Roles = "admin, supervisor, clerk")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ProcessStep processStep)  //POST: /ProcessSteps/Edit/5
        {
            DataContext.SetInsertDefaults(processStep, this);

            if (ModelState.IsValid)
            {
                OnEditSaving(processStep);
 
                DataContext.Entry(processStep).State = EntityState.Modified;
                await DataContext.SaveChangesAsync(this);
 
                OnEditSaved(processStep);
                return RedirectToAction("Index");
            }

            SetSelectLists(processStep);
            return View(processStep);
        }

        public new partial class QueryParameters : BaseController.QueryParameters
        {
            public QueryParameters() { }
            public QueryParameters(QueryParameters parameters) : base(parameters)
            {
                ProcessId = parameters.ProcessId;
                TemplateId = parameters.TemplateId;
            }
            public int? ProcessId { get; set; }
            public int? TemplateId { get; set; }
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
        partial void OnEditSaving(ProcessStep processStep);
        partial void OnEditSaved(ProcessStep processStep);
        partial void SetSelectLists(ProcessStep processStep);
    }
}
