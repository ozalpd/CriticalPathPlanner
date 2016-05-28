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
        public async Task<JsonResult> GetPurchaseOrdersForAutoComplete(QueryParameters qParam)
        {
            var query = GetPurchaseOrderQuery()
                        .Where(x => x.PoNr.Contains(qParam.SearchString))
                        .Take(qParam.PageSize);
            var list = from x in query
                       select new
                       {
                           id = x.Id,
                           value = x.PoNr,
                           label = x.PoNr
                       };

            return Json(await list.ToListAsync(), JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public async Task<ActionResult> GetPurchaseOrder(int? id)
        {
            if (id == null)
            {
                return BadRequestTextResult();
            }
            PurchaseOrder purchaseOrder = await FindAsyncPurchaseOrder(id.Value);

            if (purchaseOrder == null)
            {
                return NotFoundTextResult();
            }

            return Json(new PurchaseOrderDTO(purchaseOrder), JsonRequestBehavior.AllowGet);
        }


        protected override bool CanUserCreate()
        {
            if (!_canUserCreate.HasValue)
            {
                _canUserCreate = Request.IsAuthenticated && (
                                    IsUserAdmin() ||
                                    IsUserSupervisor() ||
                                    IsUserClerk());
            }
            return _canUserCreate.Value;
        }
        protected override async Task<bool> CanUserCreateAsync()
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

        protected override bool CanUserEdit()
        {
            if (!_canUserEdit.HasValue)
            {
                _canUserEdit = Request.IsAuthenticated && (
                                    IsUserAdmin() ||
                                    IsUserSupervisor() ||
                                    IsUserClerk());
            }
            return _canUserEdit.Value;
        }
        protected override async Task<bool> CanUserEditAsync()
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
        
        protected override bool CanUserDelete()
        {
            if (!_canUserDelete.HasValue)
            {
                _canUserDelete = Request.IsAuthenticated && (
                                    IsUserAdmin() ||
                                    IsUserSupervisor());
            }
            return _canUserDelete.Value;
        }
        protected override async Task<bool> CanUserDeleteAsync()
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

        protected override bool CanUserSeeRestricted()
        {
            if (!_canSeeRestricted.HasValue)
            {
                _canSeeRestricted = Request.IsAuthenticated && (
                                    IsUserAdmin() ||
                                    IsUserSupervisor() ||
                                    IsUserClerk());
            }
            return _canSeeRestricted.Value;
        }
        protected override async Task<bool> CanUserSeeRestrictedAsync()
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



        public new partial class QueryParameters : BaseController.QueryParameters
        {
            public QueryParameters() { }
            public QueryParameters(QueryParameters parameters) : base(parameters)
            {
                DesignerId = parameters.DesignerId;
                Merchandiser1Id = parameters.Merchandiser1Id;
                Merchandiser2Id = parameters.Merchandiser2Id;
                ProductId = parameters.ProductId;
                SellingCurrencyId = parameters.SellingCurrencyId;
                SellingCurrency2Id = parameters.SellingCurrency2Id;
                LicensorCurrencyId = parameters.LicensorCurrencyId;
                BuyingCurrencyId = parameters.BuyingCurrencyId;
                BuyingCurrency2Id = parameters.BuyingCurrency2Id;
                RoyaltyCurrencyId = parameters.RoyaltyCurrencyId;
                RetailCurrencyId = parameters.RetailCurrencyId;
                CustomerId = parameters.CustomerId;
                CustomerDepartmentId = parameters.CustomerDepartmentId;
                FreightTermId = parameters.FreightTermId;
                LicensorId = parameters.LicensorId;
                SupplierId = parameters.SupplierId;
                SizingStandardId = parameters.SizingStandardId;
            }
            public int? DesignerId { get; set; }
            public int? Merchandiser1Id { get; set; }
            public int? Merchandiser2Id { get; set; }
            public int? ProductId { get; set; }
            public int? SellingCurrencyId { get; set; }
            public int? SellingCurrency2Id { get; set; }
            public int? LicensorCurrencyId { get; set; }
            public int? BuyingCurrencyId { get; set; }
            public int? BuyingCurrency2Id { get; set; }
            public int? RoyaltyCurrencyId { get; set; }
            public int? RetailCurrencyId { get; set; }
            public int? CustomerId { get; set; }
            public int? CustomerDepartmentId { get; set; }
            public int? FreightTermId { get; set; }
            public int? LicensorId { get; set; }
            public int? SupplierId { get; set; }
            public int? SizingStandardId { get; set; }
            public bool? IsApproved { get; set; }
            public bool? IsRepeat { get; set; }
            public bool? Cancelled { get; set; }
            public bool? Closed { get; set; }
            public DateTime? ApproveDateMin { get; set; }
            public DateTime? ApproveDateMax { get; set; }
            public DateTime? OrderDateMin { get; set; }
            public DateTime? OrderDateMax { get; set; }
            public DateTime? DueDateMin { get; set; }
            public DateTime? DueDateMax { get; set; }
            public DateTime? SupplierDueDateMin { get; set; }
            public DateTime? SupplierDueDateMax { get; set; }
            public DateTime? CancelDateMin { get; set; }
            public DateTime? CancelDateMax { get; set; }
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
