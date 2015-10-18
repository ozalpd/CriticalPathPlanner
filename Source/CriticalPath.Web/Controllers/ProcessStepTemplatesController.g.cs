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
    public partial class ProcessStepTemplatesController : BaseController 
    {
        partial void SetViewBags(ProcessStepTemplate processStepTemplate);
        partial void SetDefaults(ProcessStepTemplate processStepTemplate);

        
        [Authorize]
        public async Task<ActionResult> Index(string searchString, int pageNr = 1, int pageSize = 10)
        {
            var query = DataContext.GetProcessStepTemplateQuery();
            if (!string.IsNullOrEmpty(searchString))
            {
                query = from a in query
                        where
                            a.Title.Contains(searchString) 
                        select a;
            }
            int totalCount = await query.CountAsync();
            int pageCount = totalCount > 0 ? (int)Math.Ceiling(totalCount / (double)pageSize) : 0;
            if (pageNr < 1) pageNr = 1;
            if (pageNr > pageCount) pageNr = pageCount;
            int skip = (pageNr - 1) * pageSize;

            ViewBag.pageNr = pageNr;
            ViewBag.totalCount = totalCount;
            ViewBag.pageSize = pageSize;
            ViewBag.pageCount = pageCount;

            ViewBag.canUserEdit = await CanUserEdit();
            ViewBag.canUserCreate = await CanUserCreate();
            ViewBag.canUserDelete = await CanUserDelete();

            if (totalCount > 0)
            {
                return View(await query.Skip(skip).Take(pageSize).ToListAsync());
            }
            else
            {
                return View(new List<ProcessStepTemplate>());
            }
        }

        [Authorize]
        public async Task<ActionResult> Details(int? id)  //GET: /ProcessStepTemplates/Details/5
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProcessStepTemplate processStepTemplate = await FindAsyncProcessStepTemplate(id.Value);

            if (processStepTemplate == null)
            {
                return HttpNotFound();
            }

            return View(processStepTemplate);
        }

        [Authorize(Roles = "admin, supervisor, clerk")]
        public ActionResult Create()  //GET: /ProcessStepTemplates/Create
        {
            var processStepTemplate = new ProcessStepTemplate();
            SetDefaults(processStepTemplate);
            SetViewBags(null);
            return View(processStepTemplate);
        }

        [Authorize(Roles = "admin, supervisor, clerk")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProcessStepTemplate processStepTemplate)  //POST: /ProcessStepTemplates/Create
        {
            DataContext.SetInsertDefaults(processStepTemplate, this);

            if (ModelState.IsValid)
            {
 
                DataContext.ProcessStepTemplates.Add(processStepTemplate);
                await DataContext.SaveChangesAsync(this);
                return RedirectToAction("Index");
            }

            SetViewBags(processStepTemplate);
            return View(processStepTemplate);
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
        public async Task<ActionResult> Edit(int? id)  //GET: /ProcessStepTemplates/Edit/5
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProcessStepTemplate processStepTemplate = await FindAsyncProcessStepTemplate(id.Value);

            if (processStepTemplate == null)
            {
                return HttpNotFound();
            }

            SetViewBags(processStepTemplate);
            return View(processStepTemplate);
        }

        [Authorize(Roles = "admin, supervisor, clerk")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ProcessStepTemplate processStepTemplate)  //POST: /ProcessStepTemplates/Edit/5
        {
            DataContext.SetInsertDefaults(processStepTemplate, this);

            if (ModelState.IsValid)
            {
 
                DataContext.Entry(processStepTemplate).State = EntityState.Modified;
                await DataContext.SaveChangesAsync(this);
                return RedirectToAction("Index");
            }

            SetViewBags(processStepTemplate);
            return View(processStepTemplate);
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
        public async Task<ActionResult> Delete(int? id)  //GET: /ProcessStepTemplates/Delete/5
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProcessStepTemplate processStepTemplate = await FindAsyncProcessStepTemplate(id.Value);

            if (processStepTemplate == null)
            {
                return HttpNotFound();
            }
            
            DataContext.ProcessStepTemplates.Remove(processStepTemplate);
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
