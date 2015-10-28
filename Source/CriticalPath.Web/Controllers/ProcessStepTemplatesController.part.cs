using System.Linq;
using System.Data;
using CriticalPath.Data;
using System.Threading.Tasks;
using System.Data.Entity;

namespace CriticalPath.Web.Controllers
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
    }
}
