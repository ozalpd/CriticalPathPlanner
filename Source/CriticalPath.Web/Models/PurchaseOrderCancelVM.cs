using CP.i8n;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CriticalPath.Data;

namespace CriticalPath.Web.Models
{
    public partial class PurchaseOrderCancelVM : PurchaseOrderVM
    {
        public PurchaseOrderCancelVM() { }
        public PurchaseOrderCancelVM(PurchaseOrder entity) : base(entity) { }

        public override PurchaseOrder ToPurchaseOrder()
        {
            var po = base.ToPurchaseOrder();
            po.CancellationDate = DateTime.Now;
            po.CancellationNotes = CancellationNotes;

            return po;
        }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(EntityStrings), Name = "CancellationNotes")]
        public new string CancellationNotes { get; set; }
    }
}
