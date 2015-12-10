using System.Linq;
using System.Data;
using CriticalPath.Data;
using System.Threading.Tasks;
using System.Data.Entity;
using CriticalPath.Web.Controllers;

namespace CriticalPath.Web.Areas.Admin.Controllers
{
    public partial class ProcessStepTemplatesController
    {
        protected override async Task SetProcessStepTemplateDefaults(ProcessStepTemplate processStepTemplate)
        {
            int count = processStepTemplate?.ProcessTemplate == null ? 0 :
                        processStepTemplate.ProcessTemplate.StepTemplates.Count;
            if (count == 0)
            {
                count = await DataContext
                        .ProcessStepTemplates
                        .Where(t => t.ProcessTemplateId == processStepTemplate.ProcessTemplateId)
                        .CountAsync();
            }
            processStepTemplate.DisplayOrder = 10000 * (count + 1);
        }

        public new partial class QueryParameters
        {
            protected override void Constructing()
            {
                PageSize = 20;
                Page = 1;
            }
        }
    }
}
