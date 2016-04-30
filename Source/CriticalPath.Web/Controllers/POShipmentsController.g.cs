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
    public partial class POShipmentsController : BaseController 
    {
        protected virtual async Task<IQueryable<POShipment>> GetPOShipmentQuery(QueryParameters qParams)
        {
            var query = GetPOShipmentQuery();
            if (!string.IsNullOrEmpty(qParams.SearchString))
            {
                query = from a in query
                        where
                            a.ShippingNr.Contains(qParams.SearchString) | 
                            a.DeliveryNr.Contains(qParams.SearchString) | 
                            a.DestinationNr.Contains(qParams.SearchString) | 
                            a.RefCode.Contains(qParams.SearchString) | 
                            a.CustomerRefNr.Contains(qParams.SearchString) 
                        select a;
            }
            if (qParams.PurchaseOrderId != null)
            {
                query = query.Where(x => x.PurchaseOrderId == qParams.PurchaseOrderId);
            }
            if (qParams.FreightTermId != null)
            {
                query = query.Where(x => x.FreightTermId == qParams.FreightTermId);
            }

            qParams.TotalCount = await query.CountAsync();
            return query.Skip(qParams.Skip).Take(qParams.PageSize);
        }

        protected virtual async Task<List<POShipmentDTO>> GetPOShipmentDtoList(QueryParameters qParams)
        {
            var query = await GetPOShipmentQuery(qParams);
            var list = qParams.TotalCount > 0 ? await query.ToListAsync() : new List<POShipment>();
            var result = new List<POShipmentDTO>();
            foreach (var item in list)
            {
                result.Add(new POShipmentDTO(item));
            }

            return result;
        }

        [Authorize]
        public async Task<ActionResult> GetPOShipmentList(QueryParameters qParams)
        {
            var result = await GetPOShipmentDtoList(qParams);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public async Task<ActionResult> GetPOShipmentPagedList(QueryParameters qParams)
        {
            var items = await GetPOShipmentDtoList(qParams);
            var result = new PagedList<POShipmentDTO>(qParams, items);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public async Task<JsonResult> GetPOShipmentsForAutoComplete(QueryParameters qParam)
        {
            var query = GetPOShipmentQuery()
                        .Where(x => x.ShippingNr.Contains(qParam.SearchString))
                        .Take(qParam.PageSize);
            var list = from x in query
                       select new
                       {
                           id = x.Id,
                           value = x.ShippingNr,
                           label = x.ShippingNr
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
            POShipment pOShipment = await FindAsyncPOShipment(id.Value);

            if (pOShipment == null)
            {
                return HttpNotFound();
            }

            await PutCanUserInViewBag();
            if (modal ?? false)
            {
                return PartialView("_Details", pOShipment);
            }
            return View(pOShipment);
        }

        [Authorize]
        public async Task<ActionResult> GetPOShipment(int? id)
        {
            if (id == null)
            {
                return BadRequestTextResult();
            }
            POShipment pOShipment = await FindAsyncPOShipment(id.Value);

            if (pOShipment == null)
            {
                return NotFoundTextResult();
            }

            return Json(new POShipmentDTO(pOShipment), JsonRequestBehavior.AllowGet);
        }



        [Authorize(Roles = "admin, supervisor")]
        public async Task<ActionResult> Delete(int? id)  //GET: /POShipments
        {
            if (id == null)
            {
                return BadRequestTextResult();
            }
            POShipment pOShipment = await FindAsyncPOShipment(id.Value);

            if (pOShipment == null)
            {
                return NotFoundTextResult();
            }

            DataContext.POShipments.Remove(pOShipment);
            try
            {
                await DataContext.SaveChangesAsync(this);
            }
            catch (Exception ex)
            {
                var sb = new StringBuilder();
                sb.Append(MessageStrings.CanNotDelete);
                sb.Append(pOShipment.ShippingNr);
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
                PurchaseOrderId = parameters.PurchaseOrderId;
                FreightTermId = parameters.FreightTermId;
            }
            public int? PurchaseOrderId { get; set; }
            public int? FreightTermId { get; set; }
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
