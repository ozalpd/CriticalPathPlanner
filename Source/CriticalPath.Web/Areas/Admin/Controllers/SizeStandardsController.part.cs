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
using CriticalPath.Data.Resources;

namespace CriticalPath.Web.Areas.Admin.Controllers
{
    public partial class SizeStandardsController
    {

        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Edit(int? id)  //GET: /SizeStandards/Edit/5
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SizeStandard sizeStandard = await FindAsyncSizeStandard(id.Value);

            if (sizeStandard == null)
            {
                return HttpNotFound();
            }

            var sizeStandardVM = new SizeStandardVM(sizeStandard);
            SetSelectLists(sizeStandard);
            return View(sizeStandardVM);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(SizeStandardVM sizeStandardVM)  //POST: /SizeStandards/Edit/5
        {
            if (ModelState.IsValid)
            {
                var entity = sizeStandardVM.ToSizeStandard();
                DataContext.Entry(entity).State = EntityState.Modified;

                var deletingCaptions = new List<SizeCaption>();
                foreach (var sizeCaption in entity.SizeCaptions)
                {
                    if (sizeCaption.Id > 0)
                    {
                        if (string.IsNullOrEmpty(sizeCaption.Caption))
                        {
                            deletingCaptions.Add(sizeCaption);
                        }
                        else
                        {
                            DataContext.Entry(sizeCaption).State = EntityState.Modified;
                        }
                    }
                    else
                    {
                        sizeCaption.SizeStandardId = entity.Id;
                        DataContext.SizeCaptions.Add(sizeCaption);
                    }
                }

                foreach (var item in deletingCaptions)
                {
                    var sizeCaption = await FindAsyncSizeCaption(item.Id);
                    DataContext.SizeCaptions.Remove(sizeCaption);
                }

                await DataContext.SaveChangesAsync(this);
                OnEditSaved(entity);
                return RedirectToAction("Index");
            }

            SetSelectLists(sizeStandardVM.ToSizeStandard());
            return View(sizeStandardVM);
        }

        //Purpose: To set default property values for newly created SizeStandard entity
        //protected override async Task SetSizeStandardDefaults(SizeStandard sizeStandard) { }
    }
}
