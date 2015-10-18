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
    public partial class ProcessStepTemplatesController 
    {
        partial void SetViewBags(ProcessStepTemplate processStepTemplate)
        {
            //TODO: Optimize query
            var queryProcessTemplateId = DataContext.ProcessTemplates;
            //int processTemplateId = processStepTemplate == null ? 0 : processStepTemplate.ProcessTemplateId;
            //ViewBag.ProcessTemplateId = new SelectList(queryProcessTemplateId, "Id", "TemplateName", processTemplateId);
        }

        //Purpose: To set default property values for newly created ProcessStepTemplate entity
        //partial void SetDefaults(ProcessStepTemplate processStepTemplate) { }
    }
}
