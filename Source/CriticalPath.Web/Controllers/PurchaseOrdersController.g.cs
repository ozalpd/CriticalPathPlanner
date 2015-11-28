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

namespace CriticalPath.Web.Controllers
{
    public partial class PurchaseOrdersController : BaseController 
    {
        protected virtual async Task<List<PurchaseOrderDTO>> GetPurchaseOrderDtoList(QueryParameters qParams)
        {
            var query = await GetPurchaseOrderQuery(qParams);
            var list = qParams.TotalCount > 0 ? await query.ToListAsync() : new List<PurchaseOrder>();
            var result = new List<PurchaseOrderDTO>();
            foreach (var item in list)
            {
                result.Add(new PurchaseOrderDTO(item));
            }

            return result;
        }

        [Authorize]
        public async Task<ActionResult> Index(QueryParameters qParams)
        {
            var query = await GetPurchaseOrderQuery(qParams);
            await PutCanUserInViewBag();
			var result = new PagedList<PurchaseOrder>(qParams);
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
                                    await IsUserAdminAsync() ||
                                    await IsUserSupervisorAsync() ||
                                    await IsUserClerkAsync());
            }
            return _canUserCreate.Value;
        }
        bool? _canUserCreate;

        protected override async Task<bool> CanUserEdit()
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
        
        protected override async Task<bool> CanUserDelete()
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

        protected override async Task<bool> CanUserSeeRestricted()
        {
            if (!_canSeeRestricted.HasValue)
            {
                _canSeeRestricted = Request.IsAuthenticated && (
                                    await IsUserAdminAsync() ||
                                    await IsUserSupervisorAsync() ||
                                    await IsUserClerkAsync());
            }
            return _canSeeRestricted.Value;
        }
        bool? _canSeeRestricted;

        [Authorize]
        public async Task<ActionResult> GetPurchaseOrderList(QueryParameters qParams)
        {
            var result = await GetPurchaseOrderDtoList(qParams);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public async Task<ActionResult> GetPurchaseOrderPagedList(QueryParameters qParams)
        {
            var items = await GetPurchaseOrderDtoList(qParams);
            var result = new PagedList<PurchaseOrderDTO>(qParams, items);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public async Task<ActionResult> GetPurchaseOrder(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseOrder purchaseOrder = await FindAsyncPurchaseOrder(id.Value);

            if (purchaseOrder == null)
            {
                return HttpNotFound();
            }

            return Json(new PurchaseOrderDTO(purchaseOrder), JsonRequestBehavior.AllowGet);
        }


        public new partial class QueryParameters : BaseController.QueryParameters
        {
            public QueryParameters() { }
            public QueryParameters(QueryParameters parameters) : base(parameters)
            {
                ProductId = parameters.ProductId;
                SellingCurrencyId = parameters.SellingCurrencyId;
                BuyingCurrencyId = parameters.BuyingCurrencyId;
                RoyaltyCurrencyId = parameters.RoyaltyCurrencyId;
                RetailCurrencyId = parameters.RetailCurrencyId;
                CustomerId = parameters.CustomerId;
                FreightTermId = parameters.FreightTermId;
                SupplierId = parameters.SupplierId;
                SizingStandardId = parameters.SizingStandardId;
            }
            public int? ProductId { get; set; }
            public int? SellingCurrencyId { get; set; }
            public int? BuyingCurrencyId { get; set; }
            public int? RoyaltyCurrencyId { get; set; }
            public int? RetailCurrencyId { get; set; }
            public int? CustomerId { get; set; }
            public int? FreightTermId { get; set; }
            public int? SupplierId { get; set; }
            public int? SizingStandardId { get; set; }
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
    }
}
