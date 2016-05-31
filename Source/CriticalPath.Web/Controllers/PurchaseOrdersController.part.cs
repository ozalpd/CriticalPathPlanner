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
                           a.Product.Description.Contains(qParams.SearchString) |
                           a.PoNr.Contains(qParams.SearchString) |
                           a.RefCode.Contains(qParams.SearchString) |
                           a.FabricComposition.Contains(qParams.SearchString) |
                           a.Colour.Contains(qParams.SearchString) |
                           a.CustomerRefNr.Contains(qParams.SearchString) |
                           a.Description.Contains(qParams.SearchString) |
                           a.Customer.CompanyName.StartsWith(qParams.SearchString) |
                           a.CustomerDepartment.DepartmentName.StartsWith(qParams.SearchString) |
                           a.Licensor.CompanyName.StartsWith(qParams.SearchString) |
                           a.Notes.Contains(qParams.SearchString)
                        select a;
            }
            if (qParams.ProductId != null)
            {
                query = query.Where(x => x.ProductId == qParams.ProductId);
            }
            if (qParams.DesignerId != null)
            {
                query = query.Where(x => x.DesignerId == qParams.DesignerId);
            }
            if (qParams.MerchandiserId != null)
            {
                query = query.Where(x => x.Merchandiser1Id == qParams.MerchandiserId | x.Merchandiser2Id == qParams.MerchandiserId);
            }
            if (qParams.Merchandiser1Id != null)
            {
                query = query.Where(x => x.Merchandiser1Id == qParams.Merchandiser1Id);
            }
            if (qParams.Merchandiser2Id != null)
            {
                query = query.Where(x => x.Merchandiser2Id == qParams.Merchandiser2Id);
            }
            if (qParams.SellingCurrencyId != null)
            {
                query = query.Where(x => x.SellingCurrencyId == qParams.SellingCurrencyId);
            }
            if (qParams.LicensorCurrencyId != null)
            {
                query = query.Where(x => x.LicensorCurrencyId == qParams.LicensorCurrencyId);
            }
            if (qParams.BuyingCurrencyId != null)
            {
                query = query.Where(x => x.BuyingCurrencyId == qParams.BuyingCurrencyId);
            }
            if (qParams.RoyaltyCurrencyId != null)
            {
                query = query.Where(x => x.RoyaltyCurrencyId == qParams.RoyaltyCurrencyId);
            }
            if (qParams.RetailCurrencyId != null)
            {
                query = query.Where(x => x.RetailCurrencyId == qParams.RetailCurrencyId);
            }
            if (qParams.CustomerId != null)
            {
                query = query.Where(x => x.CustomerId == qParams.CustomerId);
            }
            if (qParams.CustomerDepartmentId != null)
            {
                query = query.Where(x => x.CustomerDepartmentId == qParams.CustomerDepartmentId);
            }
            if (qParams.FreightTermId != null)
            {
                query = query.Where(x => x.FreightTermId == qParams.FreightTermId);
            }
            if (qParams.LicensorId != null)
            {
                query = query.Where(x => x.LicensorId == qParams.LicensorId);
            }
            if (qParams.SupplierId != null)
            {
                query = query.Where(x => x.SupplierId == qParams.SupplierId);
            }
            if (qParams.SizingStandardId != null)
            {
                query = query.Where(x => x.SizingStandardId == qParams.SizingStandardId);
            }
            if (qParams.IsApproved != null)
            {
                query = query.Where(x => x.IsApproved == qParams.IsApproved.Value);
            }
            if (qParams.IsRepeat != null)
            {
                query = query.Where(x => x.IsRepeat == qParams.IsRepeat.Value);
            }
            if (qParams.Closed != null)
            {
                query = query.Where(x => x.Closed == qParams.Closed.Value);
            }
            if (qParams.Cancelled != null)
            {
                query = query.Where(x => x.Cancelled == qParams.Cancelled.Value);
            }
            if (qParams.ApproveDateMin != null)
            {
                query = query.Where(x => x.ApproveDate >= qParams.ApproveDateMin.Value);
            }
            if (qParams.ApproveDateMax != null)
            {
                query = query.Where(x => x.ApproveDate <= qParams.ApproveDateMax.Value);
            }
            if (qParams.OrderDateMin != null)
            {
                query = query.Where(x => x.OrderDate >= qParams.OrderDateMin.Value);
            }
            if (qParams.OrderDateMax != null)
            {
                query = query.Where(x => x.OrderDate <= qParams.OrderDateMax.Value);
            }
            if (qParams.DueDateMin != null)
            {
                query = query.Where(x => x.DueDate >= qParams.DueDateMin.Value);
            }
            if (qParams.DueDateMax != null)
            {
                query = query.Where(x => x.DueDate <= qParams.DueDateMax.Value);
            }
            if (qParams.SupplierDueDateMin != null)
            {
                query = query.Where(x => x.SupplierDueDate >= qParams.SupplierDueDateMin.Value);
            }
            if (qParams.SupplierDueDateMax != null)
            {
                query = query.Where(x => x.SupplierDueDate <= qParams.SupplierDueDateMax.Value);
            }
            if (qParams.CancelDateMin != null)
            {
                query = query.Where(x => x.CancelDate >= qParams.CancelDateMin.Value);
            }
            if (qParams.CancelDateMax != null)
            {
                query = query.Where(x => x.CancelDate <= qParams.CancelDateMax.Value);
            }

            qParams.TotalCount = await query.CountAsync();

            return query.Skip(qParams.Skip).Take(qParams.PageSize);
        }

        [Authorize]
        public async Task<ActionResult> Index(QueryParameters qParams)
        {
            if (qParams.Cancelled == null)
                qParams.Cancelled = false;
            if (!(await CanUserSeeRestrictedAsync()))
                qParams.IsApproved = true;

            var query = await GetPurchaseOrderQuery(qParams);
            await PutCanUserInViewBag();
            var result = new PagedList<PurchaseOrder>(qParams);
            if (qParams.TotalCount > 0)
            {
                result.Items = await query.ToListAsync();
            }
            ViewBag.FilterPanelExpanded = !qParams.Collapsed && (qParams.CustomerId.HasValue || qParams.CustomerDepartmentId.HasValue ||
                                        qParams.DueDateMin.HasValue || qParams.DueDateMax.HasValue || qParams.HideRestricted ||
                                        qParams.MerchandiserId.HasValue || qParams.DesignerId.HasValue ||
                                        qParams.OrderDateMin.HasValue || qParams.OrderDateMax.HasValue ||
                                        qParams.DueDateMin.HasValue || qParams.DueDateMax.HasValue ||
                                        qParams.SupplierDueDateMin.HasValue || qParams.SupplierDueDateMax.HasValue ||
                                        qParams.SupplierId.HasValue || qParams.PageSize != 20);

            PutPagerInViewBag(result);
            ViewBag.qParams = qParams;

            await SetCustomerDepartmentSelectListAsync(qParams.CustomerId ?? 0, qParams.CustomerDepartmentId ?? 0);
            await SetCustomerSelectListAsync(qParams.CustomerId ?? 0);
            await SetSupplierSelectList(qParams.SupplierId ?? 0);

            var designers = await DataContext.GetDesignerDtoList();
            ViewBag.DesignerId = new SelectList(designers, "Id", "FullName", qParams.DesignerId ?? 0);
            var merchandisers = await DataContext.GetMerchandiserDtoList();
            ViewBag.MerchandiserId = new SelectList(merchandisers, "Id", "FullName", qParams.MerchandiserId ?? 0);

            //SetPageSizeSelectList(qParams.PageSize);

            return View(result.Items);
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

            await PutVmToPO(vm, purchaseOrder, true);

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
            await PutCanUserInViewBag();
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
        public async Task<ActionResult> Create(int? customerId, int? productId)
        {
            var vm = new PurchaseOrderCreateVM();
            if (customerId != null)
            {
                var customer = await FindAsyncCustomer(customerId.Value);
                if (customer == null)
                    return HttpNotFound();
                vm.CustomerId = customer.Id;
                vm.DiscountRate = customer.DiscountRate;
                vm.Customer = new CustomerDTO(customer);
                vm.CustomerName = customer.CompanyName;
            }

            if (productId != null)
            {
                var product = await FindAsyncProduct(productId.Value);
                if (product == null)
                    return HttpNotFound();

                vm.ProductId = product.Id;
                vm.Product = new ProductDTO(product);
                vm.ProductCode = product.ProductCode;
                vm.BuyingPrice = product.BuyingPrice;
                vm.BuyingCurrencyId = product.BuyingCurrencyId;
                vm.BuyingPrice2 = product.BuyingPrice2;
                vm.BuyingCurrency2Id = product.BuyingCurrency2Id;
                vm.UnitPrice = product.UnitPrice;
                vm.SellingCurrencyId = product.SellingCurrencyId;
                vm.UnitPrice2 = product.UnitPrice2;
                vm.SellingCurrency2Id = product.SellingCurrency2Id;
                vm.RoyaltyFee = product.RoyaltyFee;
                vm.RoyaltyCurrencyId = product.RoyaltyCurrencyId;
                vm.LicensorCurrencyId = product.LicensorCurrencyId;
                vm.LicensorPrice = product.LicensorPrice;
                vm.RetailCurrencyId = product.RetailCurrencyId;
                vm.RetailPrice = product.RetailPrice;
                vm.Description = product.Description;

                await SetSupplierSelectList(product, 0);
            }

            await SetPurchaseOrderDefaults(vm);

            ViewBag.SupplierId = new SelectList(new List<SupplierDTO>(), "Id", "CompanyName", 0);
            await SetSectListAsync(vm);

            return View(vm);
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
            await SetSupplierSelectList(purchaseOrderVM.SupplierId ?? 0);
            await SetSectListAsync(purchaseOrderVM);
            return View(purchaseOrderVM);
        }

        [Authorize(Roles = "admin, supervisor, clerk")]
        public async Task<ActionResult> Edit(int? id, bool? modal)
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
            var vm = new PurchaseOrderEditVM(purchaseOrder);
            await SetSectListAsync(vm);
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
        public async Task<ActionResult> Edit(PurchaseOrderEditVM vm, bool? modal)
        {
            PurchaseOrder purchaseOrder = await FindAsyncPurchaseOrder(vm.Id);
            await PutVmToPO(vm, purchaseOrder, false);
            await DataContext.SaveChangesAsync(this);
            if (modal ?? false)
            {
                return Json(new { saved = true });
            }
            return RedirectToAction("Details", new { id = vm.Id });
        }

        private async Task SetSectListAsync(PurchaseOrderVM vm)
        {
            await SetCurrencySelectLists(vm);
            await SetFreightTermSelectListAsync(vm.FreightTermId);
            //await SetProductSelectListAsync(poVM.Product);
            await SetSizingStandardSelectListAsync(vm);
            await SetCustomerDepartmentSelectListAsync(vm.CustomerId, vm.CustomerDepartmentId ?? 0);

            var designers = await DataContext.GetDesignerDtoList();
            ViewBag.DesignerId = new SelectList(designers, "Id", "FullName", vm.DesignerId ?? 0);
            var merchandisers = await DataContext.GetMerchandiserDtoList();
            ViewBag.Merchandiser1Id = new SelectList(merchandisers, "Id", "FullName", vm.Merchandiser1Id ?? 0);
            ViewBag.Merchandiser2Id = new SelectList(merchandisers, "Id", "FullName", vm.Merchandiser2Id ?? 0);

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

                return StatusCodeTextResult(sb, HttpStatusCode.BadRequest);
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

                return StatusCodeTextResult(sb, HttpStatusCode.BadRequest);
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

                return StatusCodeTextResult(sb, HttpStatusCode.InternalServerError);
            }

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        private async Task PutVmToPO(PurchaseOrderVM vm, PurchaseOrder purchaseOrder, bool isApproving)
        {
            purchaseOrder.CustomerPoNr = vm.CustomerPoNr;
            purchaseOrder.Description = vm.Description;
            purchaseOrder.Notes = vm.Notes;
            purchaseOrder.SupplierId = vm.SupplierId;

            purchaseOrder.RefCode = vm.RefCode;
            purchaseOrder.CustomerRefNr = vm.CustomerRefNr;

            if (isApproving)
            {
                purchaseOrder.DueDate = vm.DueDate;
            }
            if (!purchaseOrder.IsApproved || isApproving || await IsUserSupervisorAsync() || await IsUserAdminAsync())
            {
                purchaseOrder.Quantity = vm.Quantity > 0 ? vm.Quantity : purchaseOrder.Quantity;
                purchaseOrder.CustomerDepartmentId = vm.CustomerDepartmentId;
                purchaseOrder.InitialComments = vm.InitialComments;
            }
            if (!purchaseOrder.IsApproved || isApproving)
            {
                purchaseOrder.CustomerId = vm.CustomerId > 0 ? vm.CustomerId : purchaseOrder.CustomerId;
                purchaseOrder.DiscountRate = vm.DiscountRate;
                purchaseOrder.FreightTermId = vm.FreightTermId;
                purchaseOrder.UnitPrice = vm.UnitPrice;
                purchaseOrder.UnitPrice2 = vm.UnitPrice2;
                purchaseOrder.RoyaltyFee = vm.RoyaltyFee;
                purchaseOrder.BuyingPrice = vm.BuyingPrice;
                purchaseOrder.BuyingPrice2 = vm.BuyingPrice2;
                purchaseOrder.RetailPrice = vm.RetailPrice;
                purchaseOrder.BuyingCurrencyId = vm.BuyingCurrencyId;
                purchaseOrder.BuyingCurrency2Id = vm.BuyingCurrency2Id;
                purchaseOrder.LicensorCurrencyId = vm.LicensorCurrencyId;
                purchaseOrder.SellingCurrencyId = vm.SellingCurrencyId;
                purchaseOrder.SellingCurrency2Id = vm.SellingCurrency2Id;
                purchaseOrder.RetailCurrencyId = vm.RetailCurrencyId;
                purchaseOrder.RoyaltyCurrencyId = vm.RoyaltyCurrencyId;

                purchaseOrder.LicensorPrice = vm.LicensorPrice;
                purchaseOrder.LicensorCurrencyId = vm.LicensorCurrencyId;
                //purchaseOrder.LicensorId = vm.LicensorId;

                purchaseOrder.ShipmentHangingFolded = vm.ShipmentHangingFolded;
                purchaseOrder.Colour = vm.Colour;
                purchaseOrder.FabricComposition = vm.FabricComposition;
                purchaseOrder.Print = vm.Print;
                purchaseOrder.Labelling = vm.Labelling;
                purchaseOrder.WovenLabel = vm.WovenLabel;
                purchaseOrder.HangerSticker = vm.HangerSticker;
                purchaseOrder.WashingInstructions = vm.WashingInstructions;

                purchaseOrder.DesignerId = vm.DesignerId;
                purchaseOrder.Merchandiser1Id = vm.Merchandiser1Id;
                purchaseOrder.Merchandiser2Id = vm.Merchandiser2Id;

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
                            DataContext.POSizeRatios.Remove(sizeRatio);
                        }
                    }
                    else
                    {
                        purchaseOrder.SizeRatios.Add(item.ToPOSizeRatio());
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

            if (!purchaseOrder.DesignerId.HasValue)
                purchaseOrder.DesignerId = await GetEmployeeId();
            if (!purchaseOrder.Merchandiser1Id.HasValue)
                purchaseOrder.Merchandiser1Id = await GetEmployeeId();
            if (!purchaseOrder.Merchandiser2Id.HasValue)
                purchaseOrder.Merchandiser2Id = await GetEmployeeId();

            //TODO:Get count of PO at this month
        }

        public partial class QueryParameters : BaseController.QueryParameters
        {
            public bool HideRestricted { get; set; }
            public int? MerchandiserId { get; set; }
            /// <summary>
            /// Is filter area collapsed
            /// </summary>
            public bool Collapsed { get; set; }
        }
    }
}
