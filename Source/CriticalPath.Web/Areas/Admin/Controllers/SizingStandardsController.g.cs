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
    public partial class SizingStandardsController : BaseController 
    {
        protected virtual async Task<IQueryable<SizingStandard>> GetSizingStandardQuery(QueryParameters qParams)
        {
            var query = GetSizingStandardQuery();
            if (!string.IsNullOrEmpty(qParams.SearchString))
            {
                query = from a in query
                        where
                            a.Title.Contains(qParams.SearchString) 
                        select a;
            }

            qParams.TotalCount = await query.CountAsync();
            return query.Skip(qParams.Skip).Take(qParams.PageSize);
        }

        protected virtual async Task<List<SizingStandardDTO>> GetSizingStandardDtoList(QueryParameters qParams)
        {
            var query = await GetSizingStandardQuery(qParams);
            var list = qParams.TotalCount > 0 ? await query.ToListAsync() : new List<SizingStandard>();
            var result = new List<SizingStandardDTO>();
            foreach (var item in list)
            {
                result.Add(new SizingStandardDTO(item));
            }

            return result;
        }

        [Authorize]
        public async Task<ActionResult> Index(QueryParameters qParams)
        {
            var query = await GetSizingStandardQuery(qParams);
            await PutCanUserInViewBag();
			var result = new PagedList<SizingStandard>(qParams);
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

        [Authorize]
        public async Task<ActionResult> GetSizingStandardList(QueryParameters qParams)
        {
            var result = await GetSizingStandardDtoList(qParams);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public async Task<ActionResult> GetSizingStandardPagedList(QueryParameters qParams)
        {
            var items = await GetSizingStandardDtoList(qParams);
            var result = new PagedList<SizingStandardDTO>(qParams, items);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

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

            await PutCanUserInViewBag();
            return View(sizingStandard);
        }

        [Authorize]
        public async Task<ActionResult> GetSizingStandard(int? id)
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

            return Json(new SizingStandardDTO(sizingStandard), JsonRequestBehavior.AllowGet);
        }



        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Delete(int? id)  //GET: /SizingStandards/Delete/5
        {
            if (id == null)
            {
                return AjaxBadRequest();
            }
            SizingStandard sizingStandard = await FindAsyncSizingStandard(id.Value);

            if (sizingStandard == null)
            {
                return AjaxNotFound();
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

                return GetAjaxStatusCode(sb, HttpStatusCode.BadRequest);
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

                return GetAjaxStatusCode(sb, HttpStatusCode.InternalServerError);
            }

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        public new partial class QueryParameters : BaseController.QueryParameters
        {
            public QueryParameters() { }
            public QueryParameters(QueryParameters parameters) : base(parameters)
            {
            }
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
        partial void SetSelectLists(SizingStandard sizingStandard);
    }
}
