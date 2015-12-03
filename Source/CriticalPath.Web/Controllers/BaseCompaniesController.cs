using CriticalPath.Data;
using CriticalPath.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CriticalPath.Web.Controllers
{
    public class BaseCompaniesController : BaseController
    {

        //[Authorize(Roles = "admin, supervisor, clerk")]
        [Authorize(Roles = "admin, supervisor")]
        public async Task<ActionResult> Discontinue(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var company = await FindAsyncCompany(id.Value);

            if (company == null)
            {
                return HttpNotFound();
            }

            company.DiscontinueDate = DateTime.Now;

            return View("DiscontinueCompany", new DiscontinueCompanyVM(company));
        }

        [Authorize(Roles = "admin, supervisor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Discontinue(DiscontinueCompanyVM vm)
        {
            if (vm.Discontinued && ModelState.IsValid)
            {
                var company = await FindAsyncCompany(vm.Id);

                SetDiscontinuedUser(company);
                company.DiscontinueNotes = vm.DiscontinueNotes;
                company.Discontinued = vm.Discontinued;
                company.DiscontinueDate = vm.DiscontinueDate;
                await DataContext.SaveChangesAsync(this);
                return RedirectToAction("Index");
            }

            return View("DiscontinueCompany", vm);
        }

        protected override async Task PutCanUserInViewBag()
        {
            ViewBag.canUserAddPurchaseOrder = await CanUserAddPurchaseOrder();
            ViewBag.canUserAddContact = await CanUserAddContact();
            ViewBag.canUserDiscontinue = await CanUserDiscontinue();

            await base.PutCanUserInViewBag();
        }
    }
}