using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CriticalPath.Data
{
    public partial class POImageDTO
    {
        partial void Initiliazing(POImage entity)
        {
            Constructing(entity);
        }

        protected virtual void Constructing(POImage image) { }
    }
}
