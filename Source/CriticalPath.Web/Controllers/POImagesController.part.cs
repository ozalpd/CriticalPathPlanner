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
    public partial class POImagesController
    {
        [HttpGet]
        [Authorize(Roles = "admin, supervisor, clerk")]
        [Route("POImages/Create/{purchaseOrderId:int?}")]
        public async Task<ActionResult> Create(int? purchaseOrderId)  //GET: /POImages/Create
        {
            if (purchaseOrderId == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var purchaseOrder = await FindAsyncPurchaseOrder(purchaseOrderId.Value);
            if (purchaseOrder == null)
                return HttpNotFound();

            var vm = new POImageVM();
            vm.PurchaseOrder = new PurchaseOrderDTO(purchaseOrder);
            vm.PurchaseOrderId = purchaseOrderId.Value;
            vm.DisplayOrder = (purchaseOrder.Images.Count() + 1) * 1000;

            return View(vm);
        }

        [HttpPost]
        [Authorize(Roles = "admin, supervisor, clerk")]
        [ValidateAntiForgeryToken]
        [Route("POImages/Create/{purchaseOrderId:int?}")]
        public async Task<ActionResult> Create(int? purchaseOrderId, POImageVM vm)  //POST: /POImages/Create
        {
            if (ModelState.IsValid)
            {
                var entity = vm.ToPOImage();
                DataContext.POImages.Add(entity);
                await DataContext.SaveChangesAsync(this);
                return RedirectToAction("Create", new { purchaseOrderId = vm.PurchaseOrderId });
            }

            var purchaseOrder = await FindAsyncPurchaseOrder(vm.PurchaseOrderId);
            vm.PurchaseOrder = new PurchaseOrderDTO(purchaseOrder);

            return View(vm);
        }

        [Authorize(Roles = "admin, supervisor, clerk")]
        public async Task<ActionResult> Edit(int? id)  //GET: /POImages/Edit/5
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            POImage pOImage = await FindAsyncPOImage(id.Value);

            if (pOImage == null)
            {
                return HttpNotFound();
            }

            var pOImageDTO = new POImageVM(pOImage);
            return View(pOImageDTO);
        }

        [Authorize(Roles = "admin, supervisor, clerk")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(POImageVM pOImageDTO)  //POST: /POImages/Edit/5
        {
            if (ModelState.IsValid)
            {
                var entity = pOImageDTO.ToPOImage();
                DataContext.Entry(entity).State = EntityState.Modified;
                await DataContext.SaveChangesAsync(this);
                return RedirectToAction("Index");
            }

            return View(pOImageDTO);
        }

    }
}
