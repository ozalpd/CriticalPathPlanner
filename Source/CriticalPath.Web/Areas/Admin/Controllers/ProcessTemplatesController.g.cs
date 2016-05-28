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
using CriticalPath.Web.Controllers;

namespace CriticalPath.Web.Areas.Admin.Controllers
{
    public partial class ProcessTemplatesController : BaseController 
    {
        protected virtual async Task<IQueryable<ProcessTemplate>> GetProcessTemplateQuery(QueryParameters qParams)
        {
            var query = GetProcessTemplateQuery();
            if (!string.IsNullOrEmpty(qParams.SearchString))
            {
                query = from a in query
                        where
                            a.TemplateName.Contains(qParams.SearchString) | 
                            a.DefaultTitle.Contains(qParams.SearchString) 
                        select a;
            }
            if (qParams.IsApproved != null)
            {
                query = query.Where(x => x.IsApproved == qParams.IsApproved.Value);
            }
            if (qParams.ApproveDateMin != null)
            {
                query = query.Where(x => x.ApproveDate >= qParams.ApproveDateMin.Value);
            }
            if (qParams.ApproveDateMax != null)
            {
                var maxDate = qParams.ApproveDateMax.Value.AddDays(1);
                query = query.Where(x => x.ApproveDate < maxDate);
            }

            qParams.TotalCount = await query.CountAsync();
            return query.Skip(qParams.Skip).Take(qParams.PageSize);
        }

        protected virtual async Task<List<ProcessTemplateDTO>> GetProcessTemplateDtoList(QueryParameters qParams)
        {
            var query = await GetProcessTemplateQuery(qParams);
            var list = qParams.TotalCount > 0 ? await query.ToListAsync() : new List<ProcessTemplate>();
            var result = new List<ProcessTemplateDTO>();
            foreach (var item in list)
            {
                result.Add(new ProcessTemplateDTO(item));
            }

            return result;
        }

        [Authorize]
        public async Task<ActionResult> Index(QueryParameters qParams)
        {
            await PutCanUserInViewBag();
            var query = await GetProcessTemplateQuery(qParams);
            var result = new PagedList<ProcessTemplate>(qParams);
            if (qParams.TotalCount > 0)
            {
                result.Items = await query.ToListAsync();
            }

            PutPagerInViewBag(result);
            return View(result.Items);
        }

        [Authorize]
        public async Task<ActionResult> GetProcessTemplateList(QueryParameters qParams)
        {
            var result = await GetProcessTemplateDtoList(qParams);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public async Task<ActionResult> GetProcessTemplatePagedList(QueryParameters qParams)
        {
            var items = await GetProcessTemplateDtoList(qParams);
            var result = new PagedList<ProcessTemplateDTO>(qParams, items);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public async Task<JsonResult> GetProcessTemplatesForAutoComplete(QueryParameters qParam)
        {
            var query = GetProcessTemplateQuery()
                        .Where(x => x.TemplateName.Contains(qParam.SearchString))
                        .Take(qParam.PageSize);
            var list = from x in query
                       select new
                       {
                           id = x.Id,
                           value = x.TemplateName,
                           label = x.TemplateName
                       };

            return Json(await list.ToListAsync(), JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public async Task<ActionResult> Details(int? id, bool? modal)
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

            await PutCanUserInViewBag();
            if (modal ?? false)
            {
                return PartialView("_Details", processTemplate);
            }
            return View(processTemplate);
        }

        [Authorize]
        public async Task<ActionResult> GetProcessTemplate(int? id)
        {
            if (id == null)
            {
                return BadRequestTextResult();
            }
            ProcessTemplate processTemplate = await FindAsyncProcessTemplate(id.Value);

            if (processTemplate == null)
            {
                return NotFoundTextResult();
            }

            return Json(new ProcessTemplateDTO(processTemplate), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Create(bool? modal)
        {
            var processTemplate = new ProcessTemplate();
            await SetProcessTemplateDefaults(processTemplate);
            SetSelectLists(processTemplate);
            if (modal ?? false)
            {
                ViewBag.Modal = true;
                return PartialView("_Create", processTemplate);
            }
            return View(processTemplate);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProcessTemplate processTemplate, bool? modal)
        {
            if (ModelState.IsValid)
            {
                OnCreateSaving(processTemplate);
 
                DataContext.ProcessTemplates.Add(processTemplate);
                await DataContext.SaveChangesAsync(this);
 
                OnCreateSaved(processTemplate);
                if (modal ?? false)
                {
                    return Json(new { saved = true });
                }
                return RedirectToAction("Create", "ProcessStepTemplates", new { processTemplateId = processTemplate.Id });
            }

            SetSelectLists(processTemplate);
            if (modal ?? false)
            {
                ViewBag.Modal = true;
                return PartialView("_Create", processTemplate);
            }
            return View(processTemplate);
        }

        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Edit(int? id, bool? modal)
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

            SetSelectLists(processTemplate);
            if (modal ?? false)
            {
                ViewBag.Modal = true;
                return PartialView("_Edit", processTemplate);
            }
            return View(processTemplate);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ProcessTemplate processTemplate, bool? modal)
        {
            if (ModelState.IsValid)
            {
                OnEditSaving(processTemplate);
 
                DataContext.Entry(processTemplate).State = EntityState.Modified;
                await DataContext.SaveChangesAsync(this);
 
                OnEditSaved(processTemplate);
                if (modal ?? false)
                {
                    return Json(new { saved = true });
                }
                return RedirectToAction("Details", new { id = processTemplate.Id });
            }

            SetSelectLists(processTemplate);
            if (modal ?? false)
            {
                ViewBag.Modal = true;
                return PartialView("_Edit", processTemplate);
            }
            return View(processTemplate);
        }


        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Delete(int? id)  //GET: /ProcessTemplates
        {
            if (id == null)
            {
                return BadRequestTextResult();
            }
            ProcessTemplate processTemplate = await FindAsyncProcessTemplate(id.Value);

            if (processTemplate == null)
            {
                return NotFoundTextResult();
            }

            int stepTemplatesCount = processTemplate.StepTemplates.Count;
            if ((stepTemplatesCount) > 0)
            {
                var sb = new StringBuilder();

                sb.Append(MessageStrings.CanNotDelete);
                sb.Append(" <b>");
                sb.Append(processTemplate.TemplateName);
                sb.Append("</b>.<br/>");

                if (stepTemplatesCount > 0)
                {
                    sb.Append(string.Format(MessageStrings.RelatedRecordsExist, stepTemplatesCount, EntityStrings.StepTemplates));
                    sb.Append("<br/>");
                }

                return StatusCodeTextResult(sb, HttpStatusCode.BadRequest);
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

                return StatusCodeTextResult(sb, HttpStatusCode.InternalServerError);
            }

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        protected override bool CanUserCreate()
        {
            if (!_canUserCreate.HasValue)
            {
                _canUserCreate = Request.IsAuthenticated && (
                                    IsUserAdmin());
            }
            return _canUserCreate.Value;
        }
        protected override async Task<bool> CanUserCreateAsync()
        {
            if (!_canUserCreate.HasValue)
            {
                _canUserCreate = Request.IsAuthenticated && (
                                    await IsUserAdminAsync());
            }
            return _canUserCreate.Value;
        }
        bool? _canUserCreate;

        protected override bool CanUserEdit()
        {
            if (!_canUserEdit.HasValue)
            {
                _canUserEdit = Request.IsAuthenticated && (
                                    IsUserAdmin());
            }
            return _canUserEdit.Value;
        }
        protected override async Task<bool> CanUserEditAsync()
        {
            if (!_canUserEdit.HasValue)
            {
                _canUserEdit = Request.IsAuthenticated && (
                                    await IsUserAdminAsync());
            }
            return _canUserEdit.Value;
        }
        bool? _canUserEdit;
        
        protected override bool CanUserDelete()
        {
            if (!_canUserDelete.HasValue)
            {
                _canUserDelete = Request.IsAuthenticated && (
                                    IsUserAdmin());
            }
            return _canUserDelete.Value;
        }
        protected override async Task<bool> CanUserDeleteAsync()
        {
            if (!_canUserDelete.HasValue)
            {
                _canUserDelete = Request.IsAuthenticated && (
                                    await IsUserAdminAsync());
            }
            return _canUserDelete.Value;
        }
        bool? _canUserDelete;

        
        protected override bool CanUserSeeRestricted() { return true; }
        protected override Task<bool> CanUserSeeRestrictedAsync() { return Task.FromResult(true); }


        public new partial class QueryParameters : BaseController.QueryParameters
        {
            public QueryParameters() { }
            public QueryParameters(QueryParameters parameters) : base(parameters)
            {
            }
            public bool? IsApproved { get; set; }
            public DateTime? ApproveDateMin { get; set; }
            public DateTime? ApproveDateMax { get; set; }
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
        partial void OnCreateSaving(ProcessTemplate processTemplate);
        partial void OnCreateSaved(ProcessTemplate processTemplate);
        partial void OnEditSaving(ProcessTemplate processTemplate);
        partial void OnEditSaved(ProcessTemplate processTemplate);
        partial void SetSelectLists(ProcessTemplate processTemplate);
    }
}
