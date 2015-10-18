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
    public partial class ProcessStepTemplatesController 
    {
        partial void SetViewBags(ProcessStepTemplate processStepTemplate)
        {
            //TODO: Optimize query
            var queryProcessTemplateId = DataContext.ProcessTemplates;
            //int processTemplateId = processStepTemplate == null ? 0 : processStepTemplate.ProcessTemplateId;
            //ViewBag.ProcessTemplateId = new SelectList(queryProcessTemplateId, "Id", "TemplateName", processTemplateId);
        }


        //public new class QueryParameters : BaseController.QueryParameters
        //{

        //}

        //Purpose: To set default property values for newly created ProcessStepTemplate entity
        //partial void SetDefaults(ProcessStepTemplate processStepTemplate) { }
    }
}
