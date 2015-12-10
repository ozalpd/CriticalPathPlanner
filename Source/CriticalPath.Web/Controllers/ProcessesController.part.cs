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
using OzzUtils.Web.Mvc;

namespace CriticalPath.Web.Controllers
{
    public partial class ProcessesController
    {
        protected virtual async Task<IQueryable<Process>> GetProcessQuery(QueryParameters qParams)
        {
            var query = GetProcessQuery();
            if (!string.IsNullOrEmpty(qParams.SearchString))
            {
                query = from a in query
                        where
                            a.PurchaseOrder.PoNr.Contains(qParams.SearchString) |
                            a.PurchaseOrder.Product.ProductCode.Contains(qParams.SearchString) |
                            a.PurchaseOrder.Product.Description.Contains(qParams.SearchString) |
                            a.PurchaseOrder.Customer.CompanyName.Contains(qParams.SearchString) |
                            a.PurchaseOrder.Customer.CustomerCode.Contains(qParams.SearchString) |
                            a.PurchaseOrder.Supplier.CompanyName.Contains(qParams.SearchString) |
                            a.PurchaseOrder.Supplier.SupplierCode.Contains(qParams.SearchString) |
                            a.Description.Contains(qParams.SearchString)
                        select a;
            }
            if (qParams.PurchaseOrderId != null)
            {
                query = query.Where(x => x.PurchaseOrderId == qParams.PurchaseOrderId);
            }
            //if (qParams.SupplierId != null)
            //{
            //    query = query.Where(x => x.PurchaseOrder.SupplierId == qParams.SupplierId);
            //}
            if (qParams.ProcessTemplateId != null)
            {
                query = query.Where(x => x.ProcessTemplateId == qParams.ProcessTemplateId);
            }

            qParams.TotalCount = await query.CountAsync();
            return query.Skip(qParams.Skip).Take(qParams.PageSize);
        }

        protected virtual async Task<List<ProcessDTO>> GetProcessDtoList(QueryParameters qParams)
        {
            var query = await GetProcessQuery(qParams);
            var list = qParams.TotalCount > 0 ? await DataContext.GetProcessDtoQuery(query).ToListAsync() : new List<ProcessDTO>();

            return list;
        }

        [Authorize]
        public async Task<ActionResult> GetProcessList(QueryParameters qParams)
        {
            var result = await GetProcessDtoList(qParams);
            return Content(result.ToJson(), "text/json");
        }

        [Authorize]
        public async Task<ActionResult> GetProcessPagedList(QueryParameters qParams)
        {
            var items = await GetProcessDtoList(qParams);
            var result = new PagedList<ProcessDTO>(qParams, items);
            return Content(result.ToJson(), "text/json");
        }

        [Authorize]
        public async Task<ActionResult> GetProcess(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Process process = await FindAsyncProcess(id.Value);

            if (process == null)
            {
                return HttpNotFound();
            }

            return Content((new ProcessDTO(process)).ToJson(), "text/json");
        }

        [Authorize]
        public async Task<ActionResult> Index(QueryParameters qParams)
        {
            var items = await GetProcessDtoList(qParams);
            await PutCanUserInViewBag();
            ViewBag.totalCount = qParams.TotalCount;
            var result = new PagedList<ProcessDTO>(qParams, items);
            ViewBag.result = result.ToJson();

            return View();
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

        protected override async Task<bool> CanUserSeeRestricted()
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

        [HttpGet]
        [Authorize(Roles = "admin, supervisor, clerk")]
        [Route("Processes/Create/{purchaseOrderId:int?}")]
        public async Task<ActionResult> Create(int? purchaseOrderId)  //GET: /Processes/Create
        {
            var process = new Process();
            if (purchaseOrderId != null)
            {
                var purchaseOrder = await FindAsyncPurchaseOrder(purchaseOrderId.Value);
                if (purchaseOrder == null)
                    return HttpNotFound();
                process.PurchaseOrder = purchaseOrder;
            }
            await SetProcessDefaults(process);
            await SetProcessTemplateSelectList(process);
            return View(process);
        }

        [HttpPost]
        [Authorize(Roles = "admin, supervisor, clerk")]
        [ValidateAntiForgeryToken]
        [Route("Processes/Create/{purchaseOrderId:int?}")]
        public async Task<ActionResult> Create(int? purchaseOrderId, Process process)  //POST: /Processes/Create
        {
            DataContext.SetInsertDefaults(process, this);

            if (ModelState.IsValid)
            {
                await OnCreateSaving(process);

                DataContext.Processes.Add(process);
                await DataContext.SaveChangesAsync(this);

                await OnCreateSaved(process);
                return RedirectToAction("Index", "ProcessSteps", new { processId = process.Id, pageSize = process.ProcessSteps.Count });
            }

            await SetProcessTemplateSelectList(process);
            return View(process);
        }

        async Task OnCreateSaved(Process process)
        {
            var currentStep = process
                                .ProcessSteps
                                .OrderBy(s => s.DisplayOrder)
                                .FirstOrDefault();
            if (currentStep != null)
            {
                process.CurrentStepId = currentStep.Id;
                await DataContext.SaveChangesAsync(this);
            }
        }

        async Task OnCreateSaving(Process process)
        {
            var purchaseOrder = process.PurchaseOrder;
            if (purchaseOrder == null)
                purchaseOrder = await FindAsyncPurchaseOrder(process.PurchaseOrderId);

            var query = GetProcessStepTemplateQuery();
            query = query.Where(s => s.ProcessTemplateId == process.ProcessTemplateId)
                                .OrderBy(s => s.DisplayOrder);
            if (purchaseOrder.IsRepeat)
                query = query.Where(s => !s.IgnoreInRepeat);

            var templates = await query.ToListAsync();
            DateTime startDate = process.StartDate;
            foreach (var template in query)
            {
                var step = new ProcessStep()
                {
                    Title = template.Title,
                    DisplayOrder = template.DisplayOrder,
                    TemplateId = template.Id,
                    TargetDate = template.RequiredWorkDays > 0 ? startDate : process.StartDate
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

        public new partial class QueryParameters
        {
            protected override void Constructing()
            {
                Page = 1;
                PageSize = 20;
            }
        }
    }
}
