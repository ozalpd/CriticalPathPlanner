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
using CriticalPath.Web.Controllers;

namespace CriticalPath.Web.Areas.Admin.Controllers
{
    public partial class ProcessStepTemplatesController : BaseController 
    {
        protected virtual IQueryable<ProcessStepTemplate> GetProcessStepTemplateQuery(QueryParameters qParams)
        {
            var query = GetProcessStepTemplateQuery();
            if (!string.IsNullOrEmpty(qParams.SearchString))
            {
                query = from a in query
                        where
                            a.Title.Contains(qParams.SearchString) 
                        select a;
            }
            if (qParams.ProcessTemplateId != null)
            {
                query = query.Where(x => x.ProcessTemplateId == qParams.ProcessTemplateId);
            }

            return query;
        }

        [Authorize]
        public async Task<ActionResult> Index(QueryParameters qParams)
        {
            var query = GetProcessStepTemplateQuery(qParams);
            qParams.TotalCount = await query.CountAsync();
            PutPagerInViewBag(qParams);
            await PutCanUserInViewBag();

            if (qParams.TotalCount > 0)
            {
                return View(await query.Skip(qParams.Skip).Take(qParams.PageSize).ToListAsync());
            }
            else
            {
                return View(new List<ProcessStepTemplate>());   //there isn't any record, so no need to run a query
            }
        }
        
        protected override async Task<bool> CanUserCreate()
        {
            if (!_canUserCreate.HasValue)
            {
                _canUserCreate = Request.IsAuthenticated && (
                                    await IsUserAdminAsync());
            }
            return _canUserCreate.Value;
        }
        bool? _canUserCreate;

        protected override async Task<bool> CanUserEdit()
        {
            if (!_canUserEdit.HasValue)
            {
                _canUserEdit = Request.IsAuthenticated && (
                                    await IsUserAdminAsync());
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

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Create(int? processTemplateId)  //GET: /ProcessStepTemplates/Create
        {
            var processStepTemplate = new ProcessStepTemplate();
            if (processTemplateId != null)
            {
                var processTemplate = await FindAsyncProcessTemplate(processTemplateId.Value);
                if (processTemplate == null)
                    return HttpNotFound();
                processStepTemplate.ProcessTemplateId = processTemplate.Id;
            }
            await SetProcessStepTemplateDefaults(processStepTemplate);
            SetSelectLists(processStepTemplate);
            return View(processStepTemplate);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        [Route("ProcessStepTemplates/Create/{processTemplateId:int?}")]
        public async Task<ActionResult> Create(int? processTemplateId, ProcessStepTemplate processStepTemplate)  //POST: /ProcessStepTemplates/Create
        {
            DataContext.SetInsertDefaults(processStepTemplate, this);

            if (ModelState.IsValid)
            {
                OnCreateSaving(processStepTemplate);
 
                DataContext.ProcessStepTemplates.Add(processStepTemplate);
                await DataContext.SaveChangesAsync(this);
 
                OnCreateSaved(processStepTemplate);
                return RedirectToAction("Create", new { processTemplateId = processTemplateId });
            }

            SetSelectLists(processStepTemplate);
            return View(processStepTemplate);
        }

        [Authorize(Roles = "admin")]
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

            SetSelectLists(processStepTemplate);
            return View(processStepTemplate);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ProcessStepTemplate processStepTemplate)  //POST: /ProcessStepTemplates/Edit/5
        {
            DataContext.SetInsertDefaults(processStepTemplate, this);

            if (ModelState.IsValid)
            {
                OnEditSaving(processStepTemplate);
 
                DataContext.Entry(processStepTemplate).State = EntityState.Modified;
                await DataContext.SaveChangesAsync(this);
 
                OnEditSaved(processStepTemplate);
                return RedirectToAction("Index", new { processTemplateId = processStepTemplate.ProcessTemplateId });
            }

            SetSelectLists(processStepTemplate);
            return View(processStepTemplate);
        }


        [Authorize(Roles = "admin")]
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
            try
            {
                await DataContext.SaveChangesAsync(this);
            }
            catch (Exception ex)
            {
                var sb = new StringBuilder();
                sb.Append(MessageStrings.CanNotDelete);
                sb.Append(processStepTemplate.Title);
                sb.Append("<br/>");
                AppendExceptionMsg(ex, sb);

                return GetErrorResult(sb, HttpStatusCode.InternalServerError);
            }

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        public new partial class QueryParameters : BaseController.QueryParameters
        {
            public int? ProcessTemplateId { get; set; }
        }


        //Partial methods
        partial void OnCreateSaving(ProcessStepTemplate processStepTemplate);
        partial void OnCreateSaved(ProcessStepTemplate processStepTemplate);
        partial void OnEditSaving(ProcessStepTemplate processStepTemplate);
        partial void OnEditSaved(ProcessStepTemplate processStepTemplate);
        partial void SetSelectLists(ProcessStepTemplate processStepTemplate);
    }
}
