using CP.i8n;
using System;
using System.ComponentModel.DataAnnotations;
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
            po.CancelDate = DateTime.Now;
            po.CancellationReason = CancellationReason;

            return po;
        }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(EntityStrings), Name = "CancellationReason")]
        public new string CancellationReason { get; set; }
    }
}
