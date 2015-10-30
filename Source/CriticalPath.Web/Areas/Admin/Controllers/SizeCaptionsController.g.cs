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
    public partial class SizeCaptionsController : BaseController 
    {
        protected virtual IQueryable<SizeCaption> GetSizeCaptionQuery(QueryParameters qParams)
        {
            var query = GetSizeCaptionQuery();
            if (!string.IsNullOrEmpty(qParams.SearchString))
            {
                query = from a in query
                        where
                            a.Caption.Contains(qParams.SearchString) 
                        select a;
            }
            if (qParams.SizeStandardId != null)
            {
                query = query.Where(x => x.SizeStandardId == qParams.SizeStandardId);
            }

            return query;
        }

        [Authorize]
        public async Task<ActionResult> Index(QueryParameters qParams)
        {
            var query = GetSizeCaptionQuery(qParams);
            qParams.TotalCount = await query.CountAsync();
            PutPagerInViewBag(qParams);
            await PutCanUserInViewBag();

            if (qParams.TotalCount > 0)
            {
                return View(await query.Skip(qParams.Skip).Take(qParams.PageSize).ToListAsync());
            }
            else
            {
                return View(new List<SizeCaption>());   //there isn't any record, so no need to run a query
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
        public async Task<ActionResult> Details(int? id)  //GET: /SizeCaptions/Details/5
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SizeCaption sizeCaption = await FindAsyncSizeCaption(id.Value);

            if (sizeCaption == null)
            {
                return HttpNotFound();
            }

            return View(sizeCaption);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        [Route("SizeCaptions/Create/{sizeStandardId:int?}")]
        public async Task<ActionResult> Create(int? sizeStandardId)  //GET: /SizeCaptions/Create
        {
            var sizeCaption = new SizeCaption();
            if (sizeStandardId != null)
            {
                var sizeStandard = await FindAsyncSizeStandard(sizeStandardId.Value);
                if (sizeStandard == null)
                    return HttpNotFound();
                sizeCaption.SizeStandardId = sizeStandard.Id;
            }
            await SetSizeCaptionDefaults(sizeCaption);
            SetSelectLists(sizeCaption);
            return View(sizeCaption);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        [Route("SizeCaptions/Create/{sizeStandardId:int?}")]
        public async Task<ActionResult> Create(int? sizeStandardId, SizeCaption sizeCaption)  //POST: /SizeCaptions/Create
        {
            DataContext.SetInsertDefaults(sizeCaption, this);

            if (ModelState.IsValid)
            {
                OnCreateSaving(sizeCaption);
 
                DataContext.SizeCaptions.Add(sizeCaption);
                await DataContext.SaveChangesAsync(this);
 
                OnCreateSaved(sizeCaption);
                return RedirectToAction("Index");
            }

            SetSelectLists(sizeCaption);
            return View(sizeCaption);
        }

        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Edit(int? id)  //GET: /SizeCaptions/Edit/5
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SizeCaption sizeCaption = await FindAsyncSizeCaption(id.Value);

            if (sizeCaption == null)
            {
                return HttpNotFound();
            }

            SetSelectLists(sizeCaption);
            return View(sizeCaption);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(SizeCaption sizeCaption)  //POST: /SizeCaptions/Edit/5
        {
            DataContext.SetInsertDefaults(sizeCaption, this);

            if (ModelState.IsValid)
            {
                OnEditSaving(sizeCaption);
 
                DataContext.Entry(sizeCaption).State = EntityState.Modified;
                await DataContext.SaveChangesAsync(this);
 
                OnEditSaved(sizeCaption);
                return RedirectToAction("Index");
            }

            SetSelectLists(sizeCaption);
            return View(sizeCaption);
        }


        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Delete(int? id)  //GET: /SizeCaptions/Delete/5
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SizeCaption sizeCaption = await FindAsyncSizeCaption(id.Value);

            if (sizeCaption == null)
            {
                return HttpNotFound();
            }

            DataContext.SizeCaptions.Remove(sizeCaption);
            try
            {
                await DataContext.SaveChangesAsync(this);
            }
            catch (Exception ex)
            {
                var sb = new StringBuilder();
                sb.Append(MessageStrings.CanNotDelete);
                sb.Append(sizeCaption.Caption);
                sb.Append("<br/>");
                AppendExceptionMsg(ex, sb);

                return GetErrorResult(sb, HttpStatusCode.InternalServerError);
            }

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        public new partial class QueryParameters : BaseController.QueryParameters
        {
            public int? SizeStandardId { get; set; }
        }


        //Partial methods
        partial void OnCreateSaving(SizeCaption sizeCaption);
        partial void OnCreateSaved(SizeCaption sizeCaption);
        partial void OnEditSaving(SizeCaption sizeCaption);
        partial void OnEditSaved(SizeCaption sizeCaption);
        partial void SetSelectLists(SizeCaption sizeCaption);
    }
}
