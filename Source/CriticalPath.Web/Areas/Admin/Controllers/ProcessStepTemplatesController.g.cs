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
    public partial class ProcessStepTemplatesController : BaseController 
    {
        protected virtual async Task<IQueryable<ProcessStepTemplate>> GetProcessStepTemplateQuery(QueryParameters qParams)
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

            qParams.TotalCount = await query.CountAsync();
            return query.Skip(qParams.Skip).Take(qParams.PageSize);
        }

        protected virtual async Task<List<ProcessStepTemplateDTO>> GetProcessStepTemplateDtoList(QueryParameters qParams)
        {
            var query = await GetProcessStepTemplateQuery(qParams);
            var list = qParams.TotalCount > 0 ? await query.ToListAsync() : new List<ProcessStepTemplate>();
            var result = new List<ProcessStepTemplateDTO>();
            foreach (var item in list)
            {
                result.Add(new ProcessStepTemplateDTO(item));
            }

            return result;
        }

        [Authorize]
        public async Task<ActionResult> Index(QueryParameters qParams)
        {
            var query = await GetProcessStepTemplateQuery(qParams);
            await PutCanUserInViewBag();
			var result = new PagedList<ProcessStepTemplate>(qParams);
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

        
        protected override Task<bool> CanUserSeeRestricted() { return Task.FromResult(true); }

        [Authorize]
        public async Task<ActionResult> GetProcessStepTemplateList(QueryParameters qParams)
        {
            var result = await GetProcessStepTemplateDtoList(qParams);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public async Task<ActionResult> GetProcessStepTemplatePagedList(QueryParameters qParams)
        {
            var items = await GetProcessStepTemplateDtoList(qParams);
            var result = new PagedList<ProcessStepTemplateDTO>(qParams, items);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public async Task<JsonResult> GetProcessStepTemplatesForAutoComplete(QueryParameters qParam)
        {
            var query = GetProcessStepTemplateQuery()
                        .Where(x => x.Title.Contains(qParam.SearchString))
                        .Take(qParam.PageSize);
            var list = from x in query
                       select new
                       {
                           id = x.Id,
                           value = x.Title,
                           label = x.Title
                       };

            return Json(await list.ToListAsync(), JsonRequestBehavior.AllowGet);
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

            await PutCanUserInViewBag();
            return View(processStepTemplate);
        }

        [Authorize]
        public async Task<ActionResult> GetProcessStepTemplate(int? id)
        {
            if (id == null)
            {
                return BadRequestTextResult();
            }
            ProcessStepTemplate processStepTemplate = await FindAsyncProcessStepTemplate(id.Value);

            if (processStepTemplate == null)
            {
                return NotFoundTextResult();
            }

            return Json(new ProcessStepTemplateDTO(processStepTemplate), JsonRequestBehavior.AllowGet);
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
                processStepTemplate.ProcessTemplate = processTemplate;
            }
            await SetProcessStepTemplateDefaults(processStepTemplate);
            SetSelectLists(processStepTemplate);
            return View(processStepTemplate);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
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
                return BadRequestTextResult();
            }
            ProcessStepTemplate processStepTemplate = await FindAsyncProcessStepTemplate(id.Value);

            if (processStepTemplate == null)
            {
                return NotFoundTextResult();
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

                return StatusCodeTextResult(sb, HttpStatusCode.InternalServerError);
            }

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        public new partial class QueryParameters : BaseController.QueryParameters
        {
            public QueryParameters() { }
            public QueryParameters(QueryParameters parameters) : base(parameters)
            {
                ProcessTemplateId = parameters.ProcessTemplateId;
            }
            public int? ProcessTemplateId { get; set; }
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
        partial void OnCreateSaving(ProcessStepTemplate processStepTemplate);
        partial void OnCreateSaved(ProcessStepTemplate processStepTemplate);
        partial void OnEditSaving(ProcessStepTemplate processStepTemplate);
        partial void OnEditSaved(ProcessStepTemplate processStepTemplate);
        partial void SetSelectLists(ProcessStepTemplate processStepTemplate);
    }
}
