using System.Net;
using System.Web.Mvc;
using System.Threading.Tasks;
using CriticalPath.Data;

namespace CriticalPath.Web.Areas.Admin.Controllers
{
    public partial class ProcessTemplatesController
    {
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

            var processTemplate = await FindAsyncProcessTemplate(id.Value);

            if (processTemplate == null)
                return HttpNotFound();

            if (processTemplate.IsApproved)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            return View(processTemplate);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, supervisor")]
        public async Task<ActionResult> Approve(ProcessTemplateDTO vm)
        {
            var processTemplate = await FindAsyncProcessTemplate(vm.Id);
            if (vm.IsApproved && processTemplate != null)
            {
                await ApproveSaveAsync(processTemplate);
                return RedirectToAction("Details", new { id = processTemplate.Id });
            }

            return View(processTemplate);
        }

        //Purpose: To set default property values for newly created ProcessTemplate entity
        //protected override void SetProcessTemplateDefaults(ProcessTemplate processTemplate) { }
    }
}
