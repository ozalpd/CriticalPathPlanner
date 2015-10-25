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
            DateTime targetDate = process.TargetStartDate;
            DateTime forecastDate = process.ForecastStartDate.HasValue ?
                                    process.ForecastStartDate.Value : DateTime.MinValue;

            foreach (var template in queryTemplate)
            {
                var step = new ProcessStep()
                {
                    Title = template.Title,
                    DisplayOrder = template.DisplayOrder,
                    TemplateId = template.Id,
                    TargetStartDate = targetDate
                };
                targetDate =  targetDate.AddDays(template.RequiredWorkDays);
                step.TargetEndDate = targetDate;
                targetDate = targetDate.AddDays(1);
                if (process.ForecastStartDate.HasValue)
                {
                    step.ForecastStartDate = forecastDate;
                    forecastDate = forecastDate.AddDays(template.RequiredWorkDays);
                    step.ForecastEndDate = forecastDate;
                    forecastDate = forecastDate.AddDays(1);
                }
                process.ProcessSteps.Add(step);
            }
        }

        partial void SetDefaults(Process process)
        {
            process.TargetStartDate = DateTime.Today;
        }

        //public new class QueryParameters : BaseController.QueryParameters
        //{

        //}

    }
}
