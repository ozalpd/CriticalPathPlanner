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

        partial void SetDefaults(ProcessStepTemplate processStepTemplate)
        {
            int count = DataContext
                        .ProcessStepTemplates
                        .Where(t => t.ProcessTemplateId == processStepTemplate.ProcessTemplateId)
                        .Count();

            processStepTemplate.DisplayOrder = 10000 * (count + 1);
        }
    }
}
