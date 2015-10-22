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
    public partial class ProcessTemplatesController : BaseController 
    {
        [Authorize]
        public async Task<ActionResult> Index(QueryParameters qParams)
        {
            var query = DataContext.GetProcessTemplateQuery();
            if (!string.IsNullOrEmpty(qParams.SearchString))
            {
                query = from a in query
                        where
                            a.TemplateName.Contains(qParams.SearchString) | 
                            a.DefaultTitle.Contains(qParams.SearchString) 
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
                return View(new List<ProcessTemplate>());   //there isn't any record, so no need to run a query
            }
        }
        
        protected virtual async Task<bool> CanUserCreate()
        {
            if (!_canUserCreate.HasValue)
            {
                _canUserCreate = Request.IsAuthenticated && (
                                    await IsUserAdminAsync());
            }
            return _canUserCreate.Value;
        }
        bool? _canUserCreate;

        protected virtual async Task<bool> CanUserEdit()
        {
            if (!_canUserEdit.HasValue)
            {
                _canUserEdit = Request.IsAuthenticated && (
                                    await IsUserAdminAsync());
            }
            return _canUserEdit.Value;
        }
        bool? _canUserEdit;
        
        protected virtual async Task<bool> CanUserDelete()
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
        public async Task<ActionResult> Details(int? id)  //GET: /ProcessTemplates/Details/5
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProcessTemplate processTemplate = await FindAsyncProcessTemplate(id.Value);

            if (processTemplate == null)
            {
                return HttpNotFound();
            }

            return View(processTemplate);
        }


        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Create()  //GET: /ProcessTemplates/Create
        {
            var processTemplate = new ProcessTemplate();
            SetDefaults(processTemplate);
            SetViewBags(null);
            return View(processTemplate);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProcessTemplate processTemplate)  //POST: /ProcessTemplates/Create
        {
            DataContext.SetInsertDefaults(processTemplate, this);

            if (ModelState.IsValid)
            {
                OnCreateSaving(processTemplate);
 
                DataContext.ProcessTemplates.Add(processTemplate);
                await DataContext.SaveChangesAsync(this);
 
                OnCreateSaved(processTemplate);
                return RedirectToAction("Index");
            }

            SetViewBags(processTemplate);
            return View(processTemplate);
        }


        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Edit(int? id)  //GET: /ProcessTemplates/Edit/5
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProcessTemplate processTemplate = await FindAsyncProcessTemplate(id.Value);

            if (processTemplate == null)
            {
                return HttpNotFound();
            }

            SetViewBags(processTemplate);
            return View(processTemplate);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ProcessTemplate processTemplate)  //POST: /ProcessTemplates/Edit/5
        {
            DataContext.SetInsertDefaults(processTemplate, this);

            if (ModelState.IsValid)
            {
                OnEditSaving(processTemplate);
 
                DataContext.Entry(processTemplate).State = EntityState.Modified;
                await DataContext.SaveChangesAsync(this);
 
                OnEditSaved(processTemplate);
                return RedirectToAction("Index");
            }

            SetViewBags(processTemplate);
            return View(processTemplate);
        }


        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Delete(int? id)  //GET: /ProcessTemplates/Delete/5
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProcessTemplate processTemplate = await FindAsyncProcessTemplate(id.Value);

            if (processTemplate == null)
            {
                return HttpNotFound();
            }

            int stepTemplatesCount = processTemplate.StepTemplates.Count;
            if ((stepTemplatesCount) > 0)
            {
                var sb = new StringBuilder();

                sb.Append(MessageStrings.CanNotDelete);
                sb.Append(" <b>");
                sb.Append(processTemplate.TemplateName);
                sb.Append("</b>.<br/>");
                sb.Append(MessageStrings.BecauseOfRelatedRecords);
                sb.Append(".<br/>");

                if (stepTemplatesCount > 0)
                {
                    sb.Append(EntityStrings.StepTemplates);
                    sb.Append(": ");
                    sb.Append(stepTemplatesCount);
                    sb.Append("<br/>");
                }

                return GetErrorResult(sb, HttpStatusCode.BadRequest);
            }

            DataContext.ProcessTemplates.Remove(processTemplate);
            try
            {
                await DataContext.SaveChangesAsync(this);
            }
            catch (Exception ex)
            {
                var sb = new StringBuilder();
                sb.Append(MessageStrings.CanNotDelete);
                sb.Append(processTemplate.TemplateName);
                sb.Append("<br/>");
                AppendExceptionMsg(ex, sb);

                return GetErrorResult(sb, HttpStatusCode.InternalServerError);
            }

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }


        //Partial methods
        partial void OnCreateSaving(ProcessTemplate processTemplate);
        partial void OnCreateSaved(ProcessTemplate processTemplate);
        partial void OnEditSaving(ProcessTemplate processTemplate);
        partial void OnEditSaved(ProcessTemplate processTemplate);
        partial void SetDefaults(ProcessTemplate processTemplate);
        partial void SetViewBags(ProcessTemplate processTemplate);
    }
}
