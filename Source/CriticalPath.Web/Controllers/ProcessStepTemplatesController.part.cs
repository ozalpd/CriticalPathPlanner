using System.Linq;
using System.Data;
using CriticalPath.Data;

namespace CriticalPath.Web.Controllers
{
    public partial class ProcessStepTemplatesController 
    {
        protected override void SetProcessStepTemplateDefaults(ProcessStepTemplate processStepTemplate)
        {
            int count = processStepTemplate.ProcessTemplate == null ? 0 :
                        processStepTemplate.ProcessTemplate.StepTemplates.Count;

            processStepTemplate.DisplayOrder = 10000 * (count + 1);
        }
    }
}
