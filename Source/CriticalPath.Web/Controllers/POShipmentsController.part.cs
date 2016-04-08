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
        [HttpGet]
        [Authorize(Roles = "admin, supervisor, clerk")]
        [Route("POShipments/Create/{purchaseOrderId:int?}")]
        public async Task<ActionResult> Create(int? purchaseOrderId)  //GET: /POShipments/Create
        {
            if (purchaseOrderId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
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
            return View(vm);
        }

        [HttpPost]
        [Authorize(Roles = "admin, supervisor, clerk")]
        [ValidateAntiForgeryToken]
        [Route("POShipments/Create/{purchaseOrderId:int?}")]
        public async Task<ActionResult> Create(int? purchaseOrderId, POShipmentDTO vm)  //POST: /POShipments/Create
        {
            if (ModelState.IsValid)
            {
                var entity = vm.ToPOShipment();
                DataContext.POShipments.Add(entity);
                await DataContext.SaveChangesAsync(this);
                return RedirectToAction("Index", new { purchaseOrderId = vm.PurchaseOrderId });
            }

            await SetFreightTermSelectListAsync(vm.FreightTermId);
            return View(vm);
        }

        [Authorize(Roles = "admin, supervisor, clerk")]
        public async Task<ActionResult> Edit(int? id)  //GET: /POShipments/Edit/5
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
            return View(vm);
        }

        [Authorize(Roles = "admin, supervisor, clerk")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(POShipmentDTO vm)  //POST: /POShipments/Edit/5
        {
            if (ModelState.IsValid)
            {
                var entity = vm.ToPOShipment();
                DataContext.Entry(entity).State = EntityState.Modified;
                await DataContext.SaveChangesAsync(this);
                return RedirectToAction("Index", new { purchaseOrderId = vm.PurchaseOrderId });
            }

            await SetFreightTermSelectListAsync(vm.FreightTermId);
            return View(vm);
        }
    }
}
