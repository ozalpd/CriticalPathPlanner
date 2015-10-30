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
using CriticalPath.Data.Resources;

namespace CriticalPath.Web.Areas.Admin.Controllers
{
    public partial class SizeCaptionsController 
    {
        partial void SetSelectLists(SizeCaption sizeCaption)
        {
            //TODO: Optimize query
            var querySizeStandardId = DataContext.SizeStandards;
            int sizeStandardId = sizeCaption == null ? 0 : sizeCaption.SizeStandardId;
            ViewBag.SizeStandardId = new SelectList(querySizeStandardId, "Id", "Title", sizeStandardId);
        }


        //Purpose: To set default property values for newly created SizeCaption entity
        //protected override async Task SetSizeCaptionDefaults(SizeCaption sizeCaption) { }
    }
}
