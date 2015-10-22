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
        partial void SetViewBags(Process process)
        {
            var queryTemplateId = DataContext.GetProcessTemplateDtoQuery();
            int processTemplateId = process == null ? 0 : process.ProcessTemplateId;
            ViewBag.ProcessTemplateId = new SelectList(queryTemplateId, "Id", "TemplateName", processTemplateId);

            //TODO: Optimize query
            //var queryOrderItemId = DataContext.GetOrderItemDtoQuery();
            //int orderItemId = process == null ? 0 : process.OrderItemId;
            //ViewBag.OrderItemId = new SelectList(queryOrderItemId, "Id", "Product.Title", orderItemId);
        }

        partial void OnCreateSaving(Process process)
        {
            var queryTemplate = DataContext.GetProcessStepTemplateQuery();
            foreach (var step in queryTemplate)
            {
                process.ProcessSteps.Add(new ProcessStep()
                {
                    Title = step.Title,
                    DisplayOrder = step.DisplayOrder,
                    TemplateId = step.Id
                });
            }
        }

        //public new class QueryParameters : BaseController.QueryParameters
        //{

        //}

    }
}
