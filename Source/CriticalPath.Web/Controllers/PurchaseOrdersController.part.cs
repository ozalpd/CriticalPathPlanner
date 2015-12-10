using System;
using System.Linq;
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
    public partial class PurchaseOrdersController
    {
        protected virtual async Task<IQueryable<PurchaseOrder>> GetPurchaseOrderQuery(QueryParameters qParams)
        {
            var query = GetPurchaseOrderQuery();
            if (!string.IsNullOrEmpty(qParams.SearchString))
            {
                query = from a in query
                        where
                           a.Product.ProductCode.Contains(qParams.SearchString) |
                           a.PoNr.Contains(qParams.SearchString) |
                           a.Description.Contains(qParams.SearchString) |
                           a.Customer.CompanyName.Contains(qParams.SearchString) |
                           a.Notes.Contains(qParams.SearchString)
                        select a;
            }
            if (qParams.CustomerId != null)
            {
                query = query.Where(x => x.CustomerId == qParams.CustomerId);
            }

            qParams.TotalCount = await query.CountAsync();

            return query.Skip(qParams.Skip).Take(qParams.PageSize);
        }

        protected override async Task PutCanUserInViewBag()
        {
            ViewBag.canUserApprove = await CanUserApprove();
            ViewBag.canUserCancelPO = await CanUserCancelPO();
            ViewBag.canUserSeeCustomer = await CanUserSeeCustomer();

            await base.PutCanUserInViewBag();
        }

        [Authorize]
        public async Task<ActionResult> Details(int? id)
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

            await PutCanUserInViewBag();
            return View(new PurchaseOrderVM(purchaseOrder));
        }

        [Authorize(Roles = "admin, supervisor")]
        public async Task<ActionResult> Approve(int? id)  //GET: /PurchaseOrders/Approve/5
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var purchaseOrder = await FindAsyncPurchaseOrder(id.Value);

            if (purchaseOrder == null)
                return HttpNotFound();

            if(purchaseOrder.IsApproved)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            if (purchaseOrder.DueDate == null)
            {
                //TODO: Get 42 days from an AppSetting
                purchaseOrder.DueDate = DateTime.Today.AddDays(42);
                purchaseOrder.SupplierDueDate = purchaseOrder.DueDate.Value.AddDays(-3);
            }
            var poVM = new PurchaseOrderEditVM(purchaseOrder);
            await SetSupplierSelectList(purchaseOrder);
            await SetSectListAsync(poVM);
            return View(poVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, supervisor")]
        public async Task<ActionResult> Approve(PurchaseOrderEditVM vm)
        {
            var purchaseOrder = await FindAsyncPurchaseOrder(vm.Id);
            if (purchaseOrder == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            PutVmToPO(vm, purchaseOrder, true);

            if (vm.IsApproved)
            {
                await ApproveSaveAsync(purchaseOrder);
                return RedirectToAction("Create", "Processes", new { purchaseOrderId = purchaseOrder.Id });
            }
            var poVM = new PurchaseOrderEditVM(purchaseOrder);
            await SetSupplierSelectList(purchaseOrder);
            await SetSectListAsync(poVM);
            return View(poVM);
        }

        [Authorize(Roles = "admin, supervisor")]
        public async Task<ActionResult> CancelPO(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var purchaseOrder = await FindAsyncPurchaseOrder(id.Value);

            if (purchaseOrder == null)
                return HttpNotFound();

            if (purchaseOrder.Cancelled)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var purchaseOrderVM = new PurchaseOrderCancelVM(purchaseOrder);
            return View(purchaseOrderVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, supervisor")]
        public async Task<ActionResult> CancelPO(PurchaseOrderCancelVM vm)
        {
            var purchaseOrder = await FindAsyncPurchaseOrder(vm.Id);
            if (purchaseOrder == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            if (!string.IsNullOrEmpty(vm.CancellationReason) && vm.Cancelled)
            {
                purchaseOrder.CancellationReason = vm.CancellationReason;
                CancelCancellation(purchaseOrder);
                foreach (var item in purchaseOrder.Processes)
                {
                    item.CancellationReason = vm.CancellationReason;
                    CancelCancellation(item);
                }
                await DataContext.SaveChangesAsync(this);
                return RedirectToAction("Details", new { id = purchaseOrder.Id });
            }
            var poVM = new PurchaseOrderCancelVM(purchaseOrder);
            return View(poVM);
        }

        [HttpGet]
        [Authorize(Roles = "admin, supervisor, clerk")]
        public async Task<ActionResult> Repeat(int? id)
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

            foreach (var ratio in purchaseOrder.SizeRatios)
            {
                ratio.Id = 0;
            }
            var purchaseOrderVM = new PurchaseOrderCreateVM(purchaseOrder);
            await SetPurchaseOrderDefaults(purchaseOrderVM);
            purchaseOrderVM.IsRepeat = true;
            purchaseOrderVM.ParentPoId = purchaseOrder.Id;
            purchaseOrderVM.ParentPoNr = purchaseOrder.PoNr;

            await SetSupplierSelectList(purchaseOrder);
            await SetSectListAsync(purchaseOrderVM);
            return View("Create", purchaseOrderVM);
        }

        //[HttpPost]
        //[Authorize(Roles = "admin, supervisor, clerk")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Repeat(PurchaseOrderCreateVM purchaseOrderVM)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var entity = purchaseOrderVM.ToPurchaseOrder();
        //        DataContext.PurchaseOrders.Add(entity);
        //        await DataContext.SaveChangesAsync(this);
        //        return RedirectToAction("Details", new { id = entity.Id });
        //    }

        //    PurchaseOrder purchaseOrder = await FindAsyncPurchaseOrder(purchaseOrderVM.ParentPoId ?? 0);
        //    if (purchaseOrder == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    await SetSupplierSelectList(purchaseOrder);
        //    await SetSectListAsync(purchaseOrderVM);
        //    return View("Create", purchaseOrderVM);
        //}


        [HttpGet]
        [Authorize(Roles = "admin, supervisor, clerk")]
        [Route("PurchaseOrders/Create/{customerId:int?}")]
        public async Task<ActionResult> Create(int? customerId)
        {
            var purchaseOrderVM = new PurchaseOrderCreateVM();
            if (customerId != null)
            {
                var customer = await FindAsyncCustomer(customerId.Value);
                if (customer == null)
                    return HttpNotFound();
                purchaseOrderVM.CustomerId = customer.Id;
                purchaseOrderVM.DiscountRate = customer.DiscountRate;
                purchaseOrderVM.Customer = new CustomerDTO(customer);
                purchaseOrderVM.CustomerName = customer.CompanyName;
            }

            await SetPurchaseOrderDefaults(purchaseOrderVM);

            ViewBag.SupplierId = new SelectList(new List<SupplierDTO>(), "Id", "CompanyName", 0);
            await SetSectListAsync(purchaseOrderVM);
            return View(purchaseOrderVM);
        }

        [HttpPost]
        [Authorize(Roles = "admin, supervisor, clerk")]
        [ValidateAntiForgeryToken]
        [Route("PurchaseOrders/Create/{customerId:int?}")]
        public async Task<ActionResult> Create(int? customerId, PurchaseOrderCreateVM purchaseOrderVM)
        {
            purchaseOrderVM.Id = 0;
            if (ModelState.IsValid)
            {
                var entity = purchaseOrderVM.ToPurchaseOrder();
                DataContext.PurchaseOrders.Add(entity);
                await DataContext.SaveChangesAsync(this);
                return RedirectToAction("Details", new { id = entity.Id });
            }

            await SetCustomerSelectListAsync(purchaseOrderVM);
            await SetSupplierSelectList(purchaseOrderVM.SupplierId);
            await SetSectListAsync(purchaseOrderVM);
            return View(purchaseOrderVM);
        }

        [Authorize(Roles = "admin, supervisor, clerk")]
        public async Task<ActionResult> Edit(int? id)
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

            await SetSupplierSelectList(purchaseOrder);
            var purchaseOrderVM = new PurchaseOrderEditVM(purchaseOrder);
            await SetSectListAsync(purchaseOrderVM);
            return View(purchaseOrderVM);
        }

        [Authorize(Roles = "admin, supervisor, clerk")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(PurchaseOrderEditVM vm)
        {
            PurchaseOrder purchaseOrder = await FindAsyncPurchaseOrder(vm.Id);
            PutVmToPO(vm, purchaseOrder, false);
            await DataContext.SaveChangesAsync(this);
            return RedirectToAction("Details", new { id = vm.Id });
        }

        private async Task SetSectListAsync(PurchaseOrderVM poVM)
        {
            ViewBag.SellingCurrencyId = await GetCurrencySelectList(poVM.SellingCurrencyId);
            ViewBag.BuyingCurrencyId = await GetCurrencySelectList(poVM.BuyingCurrencyId ?? 0);
            ViewBag.RoyaltyCurrencyId = await GetCurrencySelectList(poVM.RoyaltyCurrencyId ?? 0);
            ViewBag.RetailCurrencyId = await GetCurrencySelectList(poVM.RetailCurrencyId ?? 0);

            await SetFreightTermSelectListAsync(poVM.FreightTermId);
            //await SetProductSelectListAsync(poVM.Product);
            await SetSizingStandardSelectListAsync(poVM);
        }

        [Authorize(Roles = "admin, supervisor")]
        public async Task<ActionResult> Delete(int? id)  //GET: /PurchaseOrders/Delete/5
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

            if (purchaseOrder.IsApproved)
            {
                var sb = new StringBuilder();
                sb.Append(MessageStrings.CanNotDelete);

                return GetErrorResult(sb, HttpStatusCode.BadRequest);
            }

            int sizeRatioCount = purchaseOrder.SizeRatios.Count;
            if ((sizeRatioCount) > 0)
            {
                var sb = new StringBuilder();

                sb.Append(MessageStrings.CanNotDelete);
                sb.Append(" <b>");
                sb.Append(purchaseOrder.Product.ProductCode);
                sb.Append("</b>.<br/>");

                if (sizeRatioCount > 0)
                {
                    sb.Append(string.Format(MessageStrings.RelatedRecordsExist, sizeRatioCount, EntityStrings.SizeRatios));
                    sb.Append("<br/>");
                }

                return GetErrorResult(sb, HttpStatusCode.BadRequest);
            }

            DataContext.PurchaseOrders.Remove(purchaseOrder);
            try
            {
                await DataContext.SaveChangesAsync(this);
            }
            catch (Exception ex)
            {
                var sb = new StringBuilder();
                sb.Append(MessageStrings.CanNotDelete);
                sb.Append(purchaseOrder.Product.ProductCode);
                sb.Append("<br/>");
                AppendExceptionMsg(ex, sb);

                return GetErrorResult(sb, HttpStatusCode.InternalServerError);
            }

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        private void PutVmToPO(PurchaseOrderVM vm, PurchaseOrder purchaseOrder, bool isApproving)
        {
            purchaseOrder.CustomerPoNr = vm.CustomerPoNr;
            purchaseOrder.Description = vm.Description;
            purchaseOrder.Notes = vm.Notes;
            purchaseOrder.SupplierId = vm.SupplierId;

            if (isApproving)
            {
                purchaseOrder.DueDate = vm.DueDate;
            }
            if (!purchaseOrder.IsApproved || isApproving)
            {
                purchaseOrder.CustomerId = vm.CustomerId > 0 ? vm.CustomerId : purchaseOrder.CustomerId;
                purchaseOrder.Quantity = vm.Quantity > 0 ? vm.Quantity : purchaseOrder.Quantity;
                purchaseOrder.DiscountRate = vm.DiscountRate;
                purchaseOrder.FreightTermId = vm.FreightTermId;
                purchaseOrder.UnitPrice = vm.UnitPrice;
                purchaseOrder.RoyaltyFee = vm.RoyaltyFee;
                purchaseOrder.BuyingPrice = vm.BuyingPrice;
                purchaseOrder.RetailPrice = vm.RetailPrice;
                purchaseOrder.BuyingCurrencyId = vm.BuyingCurrencyId;
                purchaseOrder.SellingCurrencyId = vm.SellingCurrencyId;
                purchaseOrder.RetailCurrencyId = vm.RetailCurrencyId;
                purchaseOrder.RoyaltyCurrencyId = vm.RoyaltyCurrencyId;

                int srd = 0;
                foreach (var item in vm.SizeRatios)
                {
                    var sizeRatio = purchaseOrder.SizeRatios.FirstOrDefault(sr => sr.Id == item.Id);
                    if (sizeRatio != null)
                    {
                        if (item.Rate > 0)
                        {
                            sizeRatio.Rate = item.Rate;
                            //sizeRatio.Caption = item.Caption;
                        }
                        else
                        {
                            DataContext.SizeRatios.Remove(sizeRatio);
                        }
                    }
                    else
                    {
                        purchaseOrder.SizeRatios.Add(item.ToSizeRatio());
                    }
                    srd += item.Rate;
                }
                purchaseOrder.SizeRatioDivisor = srd;
            }
        }

        protected override async Task SetPurchaseOrderDefaults(PurchaseOrderDTO purchaseOrder)
        {
            purchaseOrder.OrderDate = DateTime.Today;
            purchaseOrder.PoNr = await DataContext.CreatePoNr(purchaseOrder.OrderDate);
            purchaseOrder.Id = 0;
            purchaseOrder.ApproveDate = null;
            purchaseOrder.IsApproved = false;

            //TODO:Get count of PO at this month
        }

        public new partial class QueryParameters : BaseController.QueryParameters
        {
            public bool? IsCancelled { get; set; }
            public bool? IsApproved { get; set; }
        }
    }
}
