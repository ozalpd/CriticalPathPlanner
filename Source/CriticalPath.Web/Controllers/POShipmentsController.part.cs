using System.Net;
using System.Web.Mvc;
using System.Data.Entity;
using System.Threading.Tasks;
using CriticalPath.Data;
using System;

namespace CriticalPath.Web.Controllers
{
    public partial class POShipmentsController
    {

        [Authorize]
        public async Task<ActionResult> Index(QueryParameters qParams, bool? modal)
        {
            await PutCanUserInViewBag();
            var query = await GetPOShipmentQuery(qParams);
            var result = new PagedList<POShipment>(qParams);
            if (qParams.TotalCount > 0)
            {
                result.Items = await query.ToListAsync();
            }

            ViewBag.purchaseOrderId = qParams.PurchaseOrderId;

            PutPagerInViewBag(result);
            if (modal ?? false)
            {
                ViewBag.Modal = true;
                return PartialView("_Index", result.Items);
            }
            return View(result.Items);
        }

        [HttpGet]
        [Authorize(Roles = "admin, supervisor, clerk")]
        [Route("POShipments/Create/{purchaseOrderId:int?}")]
        public async Task<ActionResult> Create(int? purchaseOrderId, bool? modal)
        {
            if (purchaseOrderId == null)
            {
                return BadRequestTextResult();
            }

            var vm = new POShipmentDTO();
            if (purchaseOrderId != null)
            {
                var purchaseOrder = await FindAsyncPurchaseOrder(purchaseOrderId.Value);
                if (purchaseOrder == null)
                    return HttpNotFound();
                vm.PurchaseOrderId = purchaseOrderId.Value;
                vm.FreightTermId = purchaseOrder.FreightTermId;
                vm.DeliveryDate = purchaseOrder.DueDate;
                vm.ShippingDate = purchaseOrder.SupplierDueDate ?? purchaseOrder.OrderDate.AddDays(42);
            }
            await SetPOShipmentDefaults(vm);
            await SetFreightTermSelectListAsync(vm.FreightTermId);
            if (modal ?? false)
            {
                ViewBag.Modal = true;
                return PartialView("_Create", vm);
            }
            return View(vm);
        }

        [HttpPost]
        [Authorize(Roles = "admin, supervisor, clerk")]
        [ValidateAntiForgeryToken]
        [Route("POShipments/Create/{purchaseOrderId:int?}")]
        public async Task<ActionResult> Create(int? purchaseOrderId, POShipmentDTO vm, bool? modal)
        {
            if (ModelState.IsValid)
            {
                var entity = vm.ToPOShipment();
                DataContext.POShipments.Add(entity);
                await DataContext.SaveChangesAsync(this);
                if (modal ?? false)
                {
                    return Json(new { saved = true });
                }
                return RedirectToAction("Index", new { purchaseOrderId = vm.PurchaseOrderId });
            }

            await SetFreightTermSelectListAsync(vm.FreightTermId);
            if (modal ?? false)
            {
                ViewBag.Modal = true;
                return PartialView("_Create", vm);
            }
            return View(vm);
        }

        [Authorize(Roles = "admin, supervisor, clerk")]
        public async Task<ActionResult> Edit(int? id, bool? modal)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            POShipment shipment = await FindAsyncPOShipment(id.Value);

            if (shipment == null)
            {
                return HttpNotFound();
            }

            var vm = new POShipmentDTO(shipment);
            await SetFreightTermSelectListAsync(vm.FreightTermId);
            if (modal ?? false)
            {
                ViewBag.Modal = true;
                return PartialView("_Edit", vm);
            }
            return View(vm);
        }

        [Authorize(Roles = "admin, supervisor, clerk")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(POShipmentDTO vm, bool? modal)
        {
            if (ModelState.IsValid)
            {
                var entity = vm.ToPOShipment();
                DataContext.Entry(entity).State = EntityState.Modified;
                await DataContext.SaveChangesAsync(this);
                if (modal ?? false)
                {
                    return Json(new { saved = true });
                }
                return RedirectToAction("Index", new { purchaseOrderId = vm.PurchaseOrderId });
            }

            await SetFreightTermSelectListAsync(vm.FreightTermId);
            if (modal ?? false)
            {
                ViewBag.Modal = true;
                return PartialView("_Edit", vm);
            }
            return View(vm);
        }
    }
}
