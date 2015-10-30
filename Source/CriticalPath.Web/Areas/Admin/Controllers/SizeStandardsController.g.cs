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
    public partial class SizeStandardsController : BaseController 
    {
        protected virtual IQueryable<SizeStandard> GetSizeStandardQuery(QueryParameters qParams)
        {
            var query = GetSizeStandardQuery();
            if (!string.IsNullOrEmpty(qParams.SearchString))
            {
                query = from a in query
                        where
                            a.Title.Contains(qParams.SearchString) 
                        select a;
            }

            return query;
        }

        [Authorize]
        public async Task<ActionResult> Index(QueryParameters qParams)
        {
            var query = GetSizeStandardQuery(qParams);
            qParams.TotalCount = await query.CountAsync();
            PutPagerInViewBag(qParams);
            await PutCanUserInViewBag();

            if (qParams.TotalCount > 0)
            {
                return View(await query.Skip(qParams.Skip).Take(qParams.PageSize).ToListAsync());
            }
            else
            {
                return View(new List<SizeStandard>());   //there isn't any record, so no need to run a query
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
        public async Task<ActionResult> Details(int? id)  //GET: /SizeStandards/Details/5
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SizeStandard sizeStandard = await FindAsyncSizeStandard(id.Value);

            if (sizeStandard == null)
            {
                return HttpNotFound();
            }

            return View(sizeStandard);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Create()  //GET: /SizeStandards/Create
        {
            var sizeStandard = new SizeStandard();
            await SetSizeStandardDefaults(sizeStandard);
            SetSelectLists(sizeStandard);
            return View(sizeStandard);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SizeStandard sizeStandard)  //POST: /SizeStandards/Create
        {
            DataContext.SetInsertDefaults(sizeStandard, this);

            if (ModelState.IsValid)
            {
                OnCreateSaving(sizeStandard);
 
                DataContext.SizeStandards.Add(sizeStandard);
                await DataContext.SaveChangesAsync(this);
 
                OnCreateSaved(sizeStandard);
                return RedirectToAction("Create", "SizeCaptions", new { sizeStandardId = sizeStandard.Id });
            }

            SetSelectLists(sizeStandard);
            return View(sizeStandard);
        }

        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Edit(int? id)  //GET: /SizeStandards/Edit/5
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SizeStandard sizeStandard = await FindAsyncSizeStandard(id.Value);

            if (sizeStandard == null)
            {
                return HttpNotFound();
            }

            SetSelectLists(sizeStandard);
            return View(sizeStandard);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(SizeStandard sizeStandard)  //POST: /SizeStandards/Edit/5
        {
            DataContext.SetInsertDefaults(sizeStandard, this);

            if (ModelState.IsValid)
            {
                OnEditSaving(sizeStandard);
 
                DataContext.Entry(sizeStandard).State = EntityState.Modified;
                await DataContext.SaveChangesAsync(this);
 
                OnEditSaved(sizeStandard);
                return RedirectToAction("Index");
            }

            SetSelectLists(sizeStandard);
            return View(sizeStandard);
        }


        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Delete(int? id)  //GET: /SizeStandards/Delete/5
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SizeStandard sizeStandard = await FindAsyncSizeStandard(id.Value);

            if (sizeStandard == null)
            {
                return HttpNotFound();
            }

            int sizeCaptionsCount = sizeStandard.SizeCaptions.Count;
            if ((sizeCaptionsCount) > 0)
            {
                var sb = new StringBuilder();

                sb.Append(MessageStrings.CanNotDelete);
                sb.Append(" <b>");
                sb.Append(sizeStandard.Title);
                sb.Append("</b>.<br/>");

                if (sizeCaptionsCount > 0)
                {
                    sb.Append(string.Format(MessageStrings.RelatedRecordsExist, sizeCaptionsCount, EntityStrings.SizeCaptions));
                    sb.Append("<br/>");
                }

                return GetErrorResult(sb, HttpStatusCode.BadRequest);
            }

            DataContext.SizeStandards.Remove(sizeStandard);
            try
            {
                await DataContext.SaveChangesAsync(this);
            }
            catch (Exception ex)
            {
                var sb = new StringBuilder();
                sb.Append(MessageStrings.CanNotDelete);
                sb.Append(sizeStandard.Title);
                sb.Append("<br/>");
                AppendExceptionMsg(ex, sb);

                return GetErrorResult(sb, HttpStatusCode.InternalServerError);
            }

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }


        //Partial methods
        partial void OnCreateSaving(SizeStandard sizeStandard);
        partial void OnCreateSaved(SizeStandard sizeStandard);
        partial void OnEditSaving(SizeStandard sizeStandard);
        partial void OnEditSaved(SizeStandard sizeStandard);
        partial void SetSelectLists(SizeStandard sizeStandard);
    }
}
