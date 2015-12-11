using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CriticalPath.Data
{
    public partial class ProcessStep
    {
        public ProcessStepDTO ToProcessStepDTO()
        {
            return new ProcessStepDTO(this);
        }
    }
}
