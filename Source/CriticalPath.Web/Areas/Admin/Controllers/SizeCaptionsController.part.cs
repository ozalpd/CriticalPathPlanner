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
        protected override async Task SetSizeCaptionDefaults(SizeCaption sizeCaption)
        {
            int sizeStandardId = sizeCaption.SizeStandard != null ?
                                    sizeCaption.SizeStandard.Id :
                                    sizeCaption.SizeStandardId;
            int count = 0;
            if (sizeStandardId > 0)
            {
                count = await GetSizeCaptionQuery()
                        .Where(s => s.SizeStandardId == sizeStandardId)
                        .CountAsync();
            }
            sizeCaption.DisplayOrder = (count + 1) * 10000;
        }
    }
}
