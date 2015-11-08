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

namespace CriticalPath.Web.Areas.Admin.Controllers
{
    public partial class SizingStandardsController
    {
        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Create()  //GET: /SizingStandards/Create
        {
            var sizingStandardVM = new SizingStandardVM();
            await SetSizingStandardDefaults(sizingStandardVM);
            return View(sizingStandardVM);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SizingStandardVM sizingStandardVM)  //POST: /SizingStandards/Create
        {
            DataContext.SetInsertDefaults(sizingStandardVM, this);

            if (ModelState.IsValid)
            {
                var entity = sizingStandardVM.ToSizingStandard();
                DataContext.SizingStandards.Add(entity);
                await DataContext.SaveChangesAsync(this);
                await DataContext.RefreshSizingStandardDtoList();
                return RedirectToAction("Index");
            }

            return View(sizingStandardVM);
        }

        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Edit(int? id)  //GET: /SizingStandards/Edit/5
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SizingStandard sizingStandard = await FindAsyncSizingStandard(id.Value);

            if (sizingStandard == null)
            {
                return HttpNotFound();
            }

            var sizingStandardVM = new SizingStandardVM(sizingStandard);
            return View(sizingStandardVM);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(SizingStandardVM sizingStandardVM)  //POST: /SizingStandards/Edit/5
        {
            if (ModelState.IsValid)
            {
                var entity = sizingStandardVM.ToSizingStandard();
                DataContext.Entry(entity).State = EntityState.Modified;

                var deletingCaptions = new List<Sizing>();
                foreach (var sizing in entity.Sizings)
                {
                    if (sizing.Id > 0)
                    {
                        if (string.IsNullOrEmpty(sizing.Caption))
                        {
                            deletingCaptions.Add(sizing);
                        }
                        else
                        {
                            DataContext.Entry(sizing).State = EntityState.Modified;
                        }
                    }
                    else
                    {
                        sizing.SizingStandardId = entity.Id;
                        DataContext.Sizings.Add(sizing);
                    }
                }

                foreach (var item in deletingCaptions)
                {
                    var sizing = await FindAsyncSizing(item.Id);
                    DataContext.Sizings.Remove(sizing);
                }

                await DataContext.SaveChangesAsync(this);
                await DataContext.RefreshSizingStandardDtoList();
                return RedirectToAction("Index");
            }

            return View(sizingStandardVM);
        }
    }
}
