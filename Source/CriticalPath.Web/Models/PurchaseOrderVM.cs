using CriticalPath.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CriticalPath.Web.Models
{
    public partial class PurchaseOrderVM : PurchaseOrderDTO
    {
        public PurchaseOrderVM(PurchaseOrder entity) : base(entity) { }
        public PurchaseOrderVM() { }

        protected override void Constructing(PurchaseOrder entity)
        {
            base.Constructing(entity);
            SizingStandard = new SizingStandardDTO(entity.SizingStandard);
            Customer = new CustomerDTO(entity.Customer);
        }

        public CustomerDTO Customer { get; set; }
        public SizingStandardDTO SizingStandard { get; set; }
    }
}
