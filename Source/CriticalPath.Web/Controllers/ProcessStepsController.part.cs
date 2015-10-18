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
    public partial class ProcessStepsController 
    {
        partial void SetViewBags(ProcessStep processStep)
        {
            //TODO: Optimize query
            var queryProcessId = DataContext.Processes;
            //int processId = processStep == null ? 0 : processStep.ProcessId;
            //ViewBag.ProcessId = new SelectList(queryProcessId, "Id", "Title", processId);
        }

        //Purpose: To set default property values for newly created ProcessStep entity
        //partial void SetDefaults(ProcessStep processStep) { }
    }
}
