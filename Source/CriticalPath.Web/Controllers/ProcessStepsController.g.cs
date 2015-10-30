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
using CriticalPath.Data.Resources;

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

        [Authorize]
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


        //Partial methods
        partial void OnCreateSaving(ProcessStep processStep);
        partial void OnCreateSaved(ProcessStep processStep);
        partial void OnEditSaving(ProcessStep processStep);
        partial void OnEditSaved(ProcessStep processStep);
        partial void SetSelectLists(ProcessStep processStep);
    }
}
