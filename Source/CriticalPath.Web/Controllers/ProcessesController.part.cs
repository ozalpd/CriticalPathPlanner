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
    public partial class ProcessesController
    {
        protected virtual IQueryable<Process> GetProcessQuery(QueryParameters qParams)
        {
            var query = GetProcessQuery();
            if (!string.IsNullOrEmpty(qParams.SearchString))
            {
                query = from a in query
                        where
                            a.PurchaseOrder.Product.Title.Contains(qParams.SearchString) |
                            a.PurchaseOrder.Product.Code.Contains(qParams.SearchString) |
                            a.PurchaseOrder.Customer.CompanyName.Contains(qParams.SearchString) |
                            a.PurchaseOrder.Customer.CustomerCode.Contains(qParams.SearchString) |
                            a.Supplier.CompanyName.Contains(qParams.SearchString) |
                            a.Supplier.SupplierCode.Contains(qParams.SearchString) |
                            a.Description.Contains(qParams.SearchString)
                        select a;
            }
            if (qParams.PurchaseOrderId != null)
            {
                query = query.Where(x => x.PurchaseOrderId == qParams.PurchaseOrderId);
            }
            if (qParams.SupplierId != null)
            {
                query = query.Where(x => x.SupplierId == qParams.SupplierId);
            }
            if (qParams.ProcessTemplateId != null)
            {
                query = query.Where(x => x.ProcessTemplateId == qParams.ProcessTemplateId);
            }

            return query;
        }

        protected override async Task PutCanUserInViewBag()
        {
            ViewBag.canUserApprove = await CanUserApprove();
            await base.PutCanUserInViewBag();
        }

        [Authorize(Roles = "admin, supervisor")]
        public async Task<ActionResult> Approve(int? id)  //GET: /PurchaseOrders/Edit/5
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var process = await FindAsyncProcess(id.Value);

            if (process == null)
                return HttpNotFound();

            if (process.IsApproved)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            return View(process);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, supervisor")]
        public async Task<ActionResult> Approve(ProcessDTO vm)
        {
            var process = await FindAsyncProcess(vm.Id);
            if (vm.IsApproved && process != null)
            {
                await ApproveSaveAsync(process);
                return RedirectToAction("Details", new { id = process.Id });
            }

            return View(process);
        }


        private async Task SetProcessTemplateSelectList(Process process)
        {
            var queryTemplateId = DataContext
                                    .GetProcessTemplateDtoQuery()
                                    .Where(t => t.IsApproved);
            var templates =await queryTemplateId.ToListAsync();
            int processTemplateId = process == null ? 0 : process.ProcessTemplateId;
            if (processTemplateId == 0 && templates.Count > 0)
            {
                processTemplateId = templates.FirstOrDefault().Id;
            }
            ViewBag.ProcessTemplateId = new SelectList(templates, "Id", "TemplateName", processTemplateId);
        }

        private async Task SetSupplierSelectList(Process process)
        {
            if (process.PurchaseOrder == null)
            {
                process.PurchaseOrder = process.PurchaseOrder ??
                                            await DataContext.PurchaseOrders
                                                .FirstOrDefaultAsync(p => p.Id == process.PurchaseOrderId);
            }
            if (process.PurchaseOrder.Product == null)
            {
                var purchaseOrder = process.PurchaseOrder;
                purchaseOrder.Product = purchaseOrder.Product ??
                            await DataContext.Products
                                    .FirstOrDefaultAsync(p => p.Id == purchaseOrder.ProductId);
            }

            int supplierId = process == null ? 0 : process.SupplierId;
            var suppliers = process.PurchaseOrder.Product.Suppliers;
            if (suppliers == null || suppliers.Count == 0)
            {
                var querySuppliers = DataContext.GetSupplierQuery();
                suppliers = await querySuppliers.ToListAsync();
            }
            else if (supplierId == 0)
            {
                supplierId = suppliers.FirstOrDefault().Id;
            }
            ViewBag.SupplierId = new SelectList(suppliers, "Id", "CompanyName", supplierId);
        }

        partial void OnCreateSaving(Process process)
        {
            var queryTemplate = GetProcessStepTemplateQuery();
            DateTime startDate = process.StartDate;
            foreach (var template in queryTemplate)
            {
                var step = new ProcessStep()
                {
                    Title = template.Title,
                    DisplayOrder = template.DisplayOrder,
                    TemplateId = template.Id,
                    TargetDate = startDate
                };
                startDate =  startDate.AddDays(template.RequiredWorkDays);
                process.ProcessSteps.Add(step);
            }
        }

        protected override Task SetProcessDefaults(Process process)
        {
            //TODO: Get 42 days from an AppSetting, ProcessesController
            process.TargetDate = process.PurchaseOrder?.DueDate ?? DateTime.Today.AddDays(42);
            process.StartDate = DateTime.Today;
            return Task.FromResult(default(object));
        }
    }
}
