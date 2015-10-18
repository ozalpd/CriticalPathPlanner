using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using CriticalPath.Web.Models;
using System.Net;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using CriticalPath.Data;

namespace CriticalPath.Web.Controllers
{
    public partial class ProcessesController 
    {
        partial void SetViewBags(Process process)
        {
            //TODO: Optimize query
            var queryProcessTemplateId = DataContext.ProcessTemplates;
            int processTemplateId = process == null ? 0 : process.ProcessTemplateId;
            ViewBag.ProcessTemplateId = new SelectList(queryProcessTemplateId, "Id", "TemplateName", processTemplateId);
            //TODO: Optimize query
            var queryOrderItemId = DataContext.OrderItems;
            //int orderItemId = process == null ? 0 : process.OrderItemId;
            //ViewBag.OrderItemId = new SelectList(queryOrderItemId, "Id", "Product.Title", orderItemId);
        }

        //Purpose: To set default property values for newly created Process entity
        //partial void SetDefaults(Process process) { }
    }
}
