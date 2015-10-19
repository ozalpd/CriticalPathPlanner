using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Threading.Tasks;
using CriticalPath.Data;
using CriticalPath.Web.Models;
using System.Net;
using System.Web.Mvc;

namespace CriticalPath.Web.Controllers
{
    public partial class ProcessStepsController : BaseController 
    {
        partial void SetViewBags(ProcessStep processStep);
        partial void SetDefaults(ProcessStep processStep);
        
        [Authorize]
        public async Task<ActionResult> Index(QueryParameters qParams)
        {
            var query = DataContext.GetProcessStepQuery();
            if (!string.IsNullOrEmpty(qParams.SearchString))
            {
                query = from a in query
                        where
                            a.Title.Contains(qParams.SearchString) | 
                            a.Description.Contains(qParams.SearchString) 
                        select a;
            }
            qParams.TotalCount = await query.CountAsync();
            SetPagerParameters(qParams);

            ViewBag.canUserEdit = await CanUserEdit();
            ViewBag.canUserCreate = await CanUserCreate();
            ViewBag.canUserDelete = await CanUserDelete();

            if (qParams.TotalCount > 0)
            {
                return View(await query.Skip(qParams.Skip).Take(qParams.PageSize).ToListAsync());
            }
            else
            {
                return View(new List<ProcessStep>());   //there isn't any record, so no need to run a query
            }
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

        [HttpGet]
        [Authorize(Roles = "admin, supervisor, clerk")]
        public ActionResult Create()  //GET: /ProcessSteps/Create
        {
            var processStep = new ProcessStep();
            SetDefaults(processStep);
            SetViewBags(null);
            return View(processStep);
        }

        [HttpPost]
        [Authorize(Roles = "admin, supervisor, clerk")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProcessStep processStep)  //POST: /ProcessSteps/Create
        {
            DataContext.SetInsertDefaults(processStep, this);

            if (ModelState.IsValid)
            {
 
                DataContext.ProcessSteps.Add(processStep);
                await DataContext.SaveChangesAsync(this);
                return RedirectToAction("Details", new { Id = processStep.Id });
            }

            SetViewBags(processStep);
            return View(processStep);
        }
        
        protected virtual async Task<bool> CanUserCreate()
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

            SetViewBags(processStep);
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
 
                DataContext.Entry(processStep).State = EntityState.Modified;
                await DataContext.SaveChangesAsync(this);
                return RedirectToAction("Index");
            }

            SetViewBags(processStep);
            return View(processStep);
        }

        protected virtual async Task<bool> CanUserEdit()
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

        [Authorize(Roles = "admin, supervisor")]
        public async Task<ActionResult> Delete(int? id)  //GET: /ProcessSteps/Delete/5
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
            
            DataContext.ProcessSteps.Remove(processStep);
            await DataContext.SaveChangesAsync(this);

            return RedirectToAction("Index");
        }
        
        protected virtual async Task<bool> CanUserDelete()
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
    }
}
