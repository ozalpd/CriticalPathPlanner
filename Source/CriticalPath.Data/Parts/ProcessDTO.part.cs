using CP.i8n;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CriticalPath.Data
{
    public partial class ProcessDTO : IIsApproved, ICancelled
    {
        [Display(ResourceType = typeof(EntityStrings), Name = "PoNr")]
        public string PoNr { get; set; }

        [DataType(DataType.Date)]
        [Display(ResourceType = typeof(EntityStrings), Name = "OrderDate")]
        public DateTime OrderDate { get; set; }

        [DataType(DataType.Date)]
        [Display(ResourceType = typeof(EntityStrings), Name = "DueDate")]
        public DateTime? DueDate { get; set; }

        [Display(ResourceType = typeof(EntityStrings), Name = "IsRepeat")]
        public bool IsRepeat { get; set; }

        [Range(1, 1000000)]
        [Display(ResourceType = typeof(EntityStrings), Name = "Quantity")]
        public int Quantity { get; set; }

        [Display(ResourceType = typeof(EntityStrings), Name = "Customer")]
        public string CustomerName { get; set; }

        [Display(ResourceType = typeof(EntityStrings), Name = "Supplier")]
        public string SupplierName { get; set; }

        [Display(ResourceType = typeof(EntityStrings), Name = "ProductCode")]
        public string ProductCode { get; set; }

        [Display(ResourceType = typeof(EntityStrings), Name = "Description")]
        public string ProductDescription { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(ResourceType = typeof(EntityStrings), Name = "ImageUrl")]
        public string ImageUrl { get; set; }

        [Display(ResourceType = typeof(EntityStrings), Name = "ParentCategory")]
        public string ParentCategoryName { get; set; }

        [Display(ResourceType = typeof(EntityStrings), Name = "SubCategory")]
        public string CategoryName { get; set; }

        public virtual ICollection<ProcessStepDTO> ProcessSteps { get; set; }
    }
}
