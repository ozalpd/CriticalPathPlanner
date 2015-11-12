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
        protected virtual IQueryable<ProcessStep> GetProcessStepQuery(QueryParameters qParams)
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

            return query;
        }

        [Authorize(Roles = "admin, supervisor, clerk")]
        public async Task<ActionResult> Index(QueryParameters qParams)
        {
            var query = GetProcessStepQuery(qParams);
            qParams.TotalCount = await query.CountAsync();
            PutPagerInViewBag(qParams);
            await PutCanUserInViewBag();

            if (qParams.TotalCount > 0)
            {
                return View(await query.Skip(qParams.Skip).Take(qParams.PageSize).ToListAsync());
            }
            else
            {
                return View(new List<ProcessStep>());   //there isn't any record, so no need to run a query
            }
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
            public int? ProcessId { get; set; }
            public int? TemplateId { get; set; }
        }

        partial void OnEditSaving(ProcessStep processStep);
        partial void OnEditSaved(ProcessStep processStep);
        partial void SetSelectLists(ProcessStep processStep);
    }
}
