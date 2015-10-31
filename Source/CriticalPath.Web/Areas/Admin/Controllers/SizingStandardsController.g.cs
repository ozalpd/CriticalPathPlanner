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
    public partial class SizingStandardsController : BaseController 
    {
        protected virtual IQueryable<SizingStandard> GetSizingStandardQuery(QueryParameters qParams)
        {
            var query = GetSizingStandardQuery();
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
            var query = GetSizingStandardQuery(qParams);
            qParams.TotalCount = await query.CountAsync();
            PutPagerInViewBag(qParams);
            await PutCanUserInViewBag();

            if (qParams.TotalCount > 0)
            {
                return View(await query.Skip(qParams.Skip).Take(qParams.PageSize).ToListAsync());
            }
            else
            {
                return View(new List<SizingStandard>());   //there isn't any record, so no need to run a query
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
        public async Task<ActionResult> Details(int? id)  //GET: /SizingStandards/Details/5
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SizingStandard sizingStandard = await FindAsyncSizingStandard(id.Value);

            if (sizingStandard == null)
            {
                return HttpNotFound();
            }

            return View(sizingStandard);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Create()  //GET: /SizingStandards/Create
        {
            var sizingStandardVM = new SizingStandardVM();
            await SetSizingStandardDefaults(sizingStandardVM);
            SetSelectLists(sizingStandardVM.ToSizingStandard());
            return View(sizingStandardVM);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SizingStandardVM sizingStandardVM)  //POST: /SizingStandards/Create
        {
            DataContext.SetInsertDefaults(sizingStandardVM, this);

            if (ModelState.IsValid)
            {
                OnCreateSaving(sizingStandardVM);
                var entity = sizingStandardVM.ToSizingStandard();
                DataContext.SizingStandards.Add(entity);
                await DataContext.SaveChangesAsync(this);
                OnCreateSaved(entity);
                return RedirectToAction("Index");
            }

            SetSelectLists(sizingStandardVM.ToSizingStandard());
            return View(sizingStandardVM);
        }



        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Delete(int? id)  //GET: /SizingStandards/Delete/5
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SizingStandard sizingStandard = await FindAsyncSizingStandard(id.Value);

            if (sizingStandard == null)
            {
                return HttpNotFound();
            }

            int sizingsCount = sizingStandard.Sizings.Count;
            int purchaseOrdersCount = sizingStandard.PurchaseOrders.Count;
            if ((sizingsCount + purchaseOrdersCount) > 0)
            {
                var sb = new StringBuilder();

                sb.Append(MessageStrings.CanNotDelete);
                sb.Append(" <b>");
                sb.Append(sizingStandard.Title);
                sb.Append("</b>.<br/>");

                if (sizingsCount > 0)
                {
                    sb.Append(string.Format(MessageStrings.RelatedRecordsExist, sizingsCount, EntityStrings.Sizings));
                    sb.Append("<br/>");
                }

                if (purchaseOrdersCount > 0)
                {
                    sb.Append(string.Format(MessageStrings.RelatedRecordsExist, purchaseOrdersCount, EntityStrings.PurchaseOrders));
                    sb.Append("<br/>");
                }

                return GetErrorResult(sb, HttpStatusCode.BadRequest);
            }

            DataContext.SizingStandards.Remove(sizingStandard);
            try
            {
                await DataContext.SaveChangesAsync(this);
            }
            catch (Exception ex)
            {
                var sb = new StringBuilder();
                sb.Append(MessageStrings.CanNotDelete);
                sb.Append(sizingStandard.Title);
                sb.Append("<br/>");
                AppendExceptionMsg(ex, sb);

                return GetErrorResult(sb, HttpStatusCode.InternalServerError);
            }

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }


        //Partial methods
        partial void OnCreateSaving(SizingStandardVM sizingStandard);
        partial void OnCreateSaved(SizingStandard sizingStandard);
        partial void OnEditSaving(SizingStandardVM sizingStandard);
        partial void OnEditSaved(SizingStandard sizingStandard);
        partial void SetSelectLists(SizingStandard sizingStandard);
    }
}
