using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Threading.Tasks;
using CriticalPath.Data;
using CriticalPath.Web.Models;
using System.Net;
using System.Web.Mvc;

namespace CriticalPath.Web.Controllers
{
    public partial class OrderItemsController
    {
        [Authorize]
        public async Task<ActionResult> Index(QueryParameters qParams)
        {
            var query = DataContext.GetOrderItemQuery();
            if (!string.IsNullOrEmpty(qParams.SearchString))
            {
                query = from a in query
                        where
                            a.Notes.Contains(qParams.SearchString) ||
                            a.PurchaseOrder.Title.Contains(qParams.SearchString) ||
                            a.PurchaseOrder.Code.Contains(qParams.SearchString) ||
                            a.Product.Title.Contains(qParams.SearchString) ||
                            a.Product.Code.Contains(qParams.SearchString) ||
                            a.Product.Description.Contains(qParams.SearchString)
                        select a;
            }
            if (qParams.PurchaseOrderId != null)
            {
                query = query.Where(x => x.PurchaseOrderId == qParams.PurchaseOrderId);
            }
            if (qParams.ProductId != null)
            {
                query = query.Where(x => x.ProductId == qParams.ProductId);
            }
            qParams.TotalCount = await query.CountAsync();
            PutPagerInViewBag(qParams);
            await PutCanUserInViewBag();

            if (qParams.TotalCount > 0)
            {
                return View(await query.Skip(qParams.Skip).Take(qParams.PageSize).ToListAsync());
            }
            else
            {
                return View(new List<OrderItem>());   //there isn't any record, so no need to run a query
            }
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

        protected override async Task SetOrderItemDefaults(OrderItem orderItem)
        {
            int count = 0;
            if (orderItem.PurchaseOrderId > 0)
            {
                count = await DataContext
                        .OrderItems
                        .Where(o => o.PurchaseOrderId == orderItem.PurchaseOrderId)
                        .CountAsync();
            }
            orderItem.DisplayOrder = 100 * (count + 1);
        }
    }
}
