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
    public partial class ProcessStepsController 
    {
        partial void SetViewBags(ProcessStep processStep)
        {
            //TODO: Optimize query
            var queryProcessId = DataContext.Processes;
            //int processId = processStep == null ? 0 : processStep.ProcessId;
            //ViewBag.ProcessId = new SelectList(queryProcessId, "Id", "Title", processId);
        }


        //public new class QueryParameters : BaseController.QueryParameters
        //{

        //}

        //Purpose: To set default property values for newly created ProcessStep entity
        //partial void SetDefaults(ProcessStep processStep) { }
    }
}
