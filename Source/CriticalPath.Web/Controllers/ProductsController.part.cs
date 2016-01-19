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
    public partial class ProductsController 
    {
        [Authorize(Roles = "admin, supervisor, clerk")]
        public async Task<JsonResult> GetProductsWithPrice(QueryParameters qParam)
        {
            var query = GetProductQuery()
                        .Where(p => p.ProductCode.Contains(qParam.SearchString))
                        .Take(qParam.PageSize);
            var list = from p in query
                       select new
                       {
                           id = p.Id,
                           value = p.ProductCode,
                           label = p.ProductCode + " [" + p.Category.ParentCategory.CategoryName + " / " + p.Category.CategoryName + "]",
                           UnitPrice = p.UnitPrice,
                           SellingCurrencyId = p.SellingCurrencyId,
                           LicensorCurrencyId = p.LicensorCurrencyId,
                           RoyaltyFee = p.RoyaltyFee,
                           RoyaltyCurrencyId = p.RoyaltyCurrencyId,
                           RetailPrice = p.RetailPrice,
                           RetailCurrencyId = p.RetailCurrencyId,
                           BuyingPrice = p.BuyingPrice,
                           BuyingCurrencyId = p.BuyingCurrencyId
                       };

            return Json(await list.ToListAsync(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize(Roles = "admin, supervisor, clerk")]
        public async Task<ActionResult> Create()  //GET: /Products/Create
        {
            var product = new ProductCreateVM();

            await SetProductCategorySelectListAsync(product);
            ViewBag.SellingCurrencyId = await GetCurrencySelectList(product.SellingCurrencyId);
            ViewBag.LicensorCurrencyId = await GetCurrencySelectList(product.LicensorCurrencyId ?? 0);
            ViewBag.BuyingCurrencyId = await GetCurrencySelectList(product.BuyingCurrencyId ?? 0);
            ViewBag.RoyaltyCurrencyId = await GetCurrencySelectList(product.RoyaltyCurrencyId ?? 0);
            ViewBag.RetailCurrencyId = await GetCurrencySelectList(product.RetailCurrencyId ?? 0);
            return View(product);
        }

        [HttpPost]
        [Authorize(Roles = "admin, supervisor, clerk")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProductCreateVM vm)
        {
            if (ModelState.IsValid)
            {
                var product = vm.ToProduct();
                var suppliers = await GetSupplierQuery()
                            .Where(s => vm.SuppliersSelected.Contains(s.Id))
                            .ToListAsync();
                foreach (var item in suppliers)
                {
                    if (!product.Suppliers.Contains(item))
                        product.Suppliers.Add(item);
                }

                DataContext.Products.Add(product);

                await DataContext.SaveChangesAsync(this);

                return RedirectToAction("Create", "PurchaseOrders", new { productId=product.Id });
            }

            await SetProductCategorySelectListAsync(vm);
            ViewBag.SellingCurrencyId = await GetCurrencySelectList(vm.SellingCurrencyId);
            ViewBag.LicensorCurrencyId = await GetCurrencySelectList(vm.LicensorCurrencyId ?? 0);
            ViewBag.BuyingCurrencyId = await GetCurrencySelectList(vm.BuyingCurrencyId ?? 0);
            ViewBag.RoyaltyCurrencyId = await GetCurrencySelectList(vm.RoyaltyCurrencyId ?? 0);
            ViewBag.RetailCurrencyId = await GetCurrencySelectList(vm.RetailCurrencyId ?? 0);
            return View(vm);
        }

        [Authorize(Roles = "admin, supervisor, clerk")]
        public async Task<ActionResult> Edit(int? id)  //GET: /Products/Edit/5
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await FindAsyncProduct(id.Value);

            if (product == null)
            {
                return HttpNotFound();
            }

            await SetProductCategorySelectListAsync(product);
            ViewBag.SellingCurrencyId = await GetCurrencySelectList(product.SellingCurrencyId);
            ViewBag.BuyingCurrencyId = await GetCurrencySelectList(product.BuyingCurrencyId ?? 0);
            ViewBag.LicensorCurrencyId = await GetCurrencySelectList(product.LicensorCurrencyId ?? 0);
            ViewBag.RoyaltyCurrencyId = await GetCurrencySelectList(product.RoyaltyCurrencyId ?? 0);
            ViewBag.RetailCurrencyId = await GetCurrencySelectList(product.RetailCurrencyId ?? 0);
            return View(new ProductEditVM(product));
        }

        [Authorize(Roles = "admin, supervisor, clerk")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ProductEditVM vm)  //POST: /Products/Edit/5
        {
            Product product;
            if (ModelState.IsValid)
            {
                product = vm.ToProduct();
                DataContext.Entry(product).State = EntityState.Modified;
                await DataContext.SaveChangesAsync(this);
                await AddRemoveSuppliers(vm);

                return RedirectToAction("Index");
            }
            product = await FindAsyncProduct(vm.Id);
            foreach (var item in product.Suppliers)
            {
                vm.Suppliers.Add(new SupplierDTO(item));
            }

            await SetProductCategorySelectListAsync(vm);
            ViewBag.SellingCurrencyId = await GetCurrencySelectList(vm.SellingCurrencyId);
            ViewBag.LicensorCurrencyId = await GetCurrencySelectList(vm.LicensorCurrencyId ?? 0);
            ViewBag.BuyingCurrencyId = await GetCurrencySelectList(vm.BuyingCurrencyId ?? 0);
            ViewBag.RoyaltyCurrencyId = await GetCurrencySelectList(vm.RoyaltyCurrencyId ?? 0);
            ViewBag.RetailCurrencyId = await GetCurrencySelectList(vm.RetailCurrencyId ?? 0);

            return View(vm);
        }

        protected async Task AddRemoveSuppliers(ProductEditVM vm)
        {
            var product = await GetProductQuery()
                            .Include(p => p.Suppliers)
                            .FirstOrDefaultAsync(p => p.Id == vm.Id);
            if (vm.SuppliersSelected != null)
            {
                var toBeRemoved = new List<Supplier>();
                foreach (var item in product.Suppliers)
                {
                    if (!vm.SuppliersSelected.Contains(item.Id))
                        toBeRemoved.Add(item);
                }
                var suppliers = await GetSupplierQuery()
                                .Where(s => vm.SuppliersSelected.Contains(s.Id))
                                .ToListAsync();
                foreach (var item in suppliers)
                {
                    if (!product.Suppliers.Contains(item))
                        product.Suppliers.Add(item);
                }
                foreach (var item in toBeRemoved)
                {
                    product.Suppliers.Remove(item);
                }
            }
            await DataContext.SaveChangesAsync(this);
        }

        [Authorize(Roles = "admin, supervisor")]
        public async Task<ActionResult> Delete(int? id)  //GET: /Products/Delete/5
        {
            if (id == null)
            {
                return BadRequestTextResult();
            }
            Product product = await FindAsyncProduct(id.Value);

            if (product == null)
            {
                return NotFoundTextResult();
            }

            int purchaseOrdersCount = product.PurchaseOrders.Count;
            if ((purchaseOrdersCount) > 0)
            {
                var sb = new StringBuilder();

                sb.Append(MessageStrings.CanNotDelete);
                sb.Append(" <b>");
                sb.Append(product.ProductCode);
                sb.Append("</b>.<br/>");

                if (purchaseOrdersCount > 0)
                {
                    sb.Append(string.Format(MessageStrings.RelatedRecordsExist, purchaseOrdersCount, EntityStrings.PurchaseOrders));
                    sb.Append("<br/>");
                }

                return StatusCodeTextResult(sb, HttpStatusCode.BadRequest);
            }

            var suppliers = new List<Supplier>();
            foreach (var item in product.Suppliers)
            {
                suppliers.Add(item);
            }
            foreach (var item in suppliers)
            {
                product.Suppliers.Remove(item);
            }

            DataContext.Products.Remove(product);
            try
            {
                await DataContext.SaveChangesAsync(this);
            }
            catch (Exception ex)
            {
                var sb = new StringBuilder();
                sb.Append(MessageStrings.CanNotDelete);
                sb.Append(product.ProductCode);
                sb.Append("<br/>");
                AppendExceptionMsg(ex, sb);

                return StatusCodeTextResult(sb, HttpStatusCode.InternalServerError);
            }

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
        //Purpose: To set default property values for newly created Product entity
        //protected override async Task SetProductDefaults(Product product) { }
    }
}
