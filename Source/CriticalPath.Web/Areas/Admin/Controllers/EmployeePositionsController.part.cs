using System;
using System.Text;
using System.Net;
using System.Web.Mvc;
using System.Threading.Tasks;
using CriticalPath.Data;
using CP.i8n;

namespace CriticalPath.Web.Areas.Admin.Controllers
{
    public partial class EmployeePositionsController
    {

        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Edit(int? id)  //GET: /EmployeePositions/Edit/5
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeePosition employeePosition = await FindAsyncEmployeePosition(id.Value);

            if (employeePosition == null || employeePosition.AppDefault)
            {
                return HttpNotFound();
            }

            return View(employeePosition);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EmployeePositionDTO vm)
        {
            if (ModelState.IsValid)
            {
                var employeePosition = await FindAsyncEmployeePosition(vm.Id);
                if(employeePosition.AppDefault)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                employeePosition.Position = vm.Position;
                await DataContext.SaveChangesAsync(this);

                return RedirectToAction("Index");
            }

            return View(vm);
        }


        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Delete(int? id)  //GET: /EmployeePositions/Delete/5
        {
            if (id == null)
            {
                return BadRequestTextResult();
            }
            EmployeePosition employeePosition = await FindAsyncEmployeePosition(id.Value);

            if (employeePosition == null || employeePosition.AppDefault)
            {
                return NotFoundTextResult();
            }

            DataContext.EmployeePositions.Remove(employeePosition);
            try
            {
                await DataContext.SaveChangesAsync(this);
            }
            catch (Exception ex)
            {
                var sb = new StringBuilder();
                sb.Append(MessageStrings.CanNotDelete);
                sb.Append(employeePosition.Position);
                sb.Append("<br/>");
                AppendExceptionMsg(ex, sb);

                return StatusCodeTextResult(sb, HttpStatusCode.InternalServerError);
            }

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }


        partial void OnCreateSaving(EmployeePosition employeePosition)
        {
            //AppDefault is for default records for being used by the application.
            employeePosition.AppDefault = false;
        }
    }
}
