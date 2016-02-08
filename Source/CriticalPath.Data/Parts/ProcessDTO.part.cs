using CP.i8n;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CriticalPath.Data
{
    public partial class ProcessDTO : IIsApproved, ICancelled
    {
        partial void Initiliazing(Process entity)
        {
            Constructing(entity);    
        }

        protected virtual void Constructing(Process entity)
        {
            if (entity.PurchaseOrder != null)
            {
                var po = entity.PurchaseOrder;
                PoNr = po.PoNr;
                OrderDate = entity.PurchaseOrder.OrderDate;
                DueDate = entity.PurchaseOrder.DueDate;
                IsRepeat = entity.PurchaseOrder.IsRepeat;
                Quantity = entity.PurchaseOrder.Quantity;
                ProductId = po.ProductId;
                if (po.Customer != null)
                {
                    CustomerName = po.Customer.CompanyName;
                }
                if (po.Product != null)
                {
                    ProductCode = po.Product.ProductCode;
                    ProductDescription = po.Product.Description;
                    ImageUrl = po.Product.ImageUrl;
                    if (po.Product.Category != null)
                    {
                        CategoryName = po.Product.Category.CategoryName;
                    }
                    if (po.Product.Category?.ParentCategory != null)
                    {
                        ParentCategoryName = po.Product.Category?.ParentCategory.CategoryName;
                    }
                }
                if (po.Supplier != null)
                {
                    SupplierName = po.Supplier.CompanyName;
                }
            }

            ProcessSteps = new List<ProcessStepDTO>();
            if (entity.ProcessSteps != null)
            {
                foreach (var step in entity.ProcessSteps)
                {
                    ProcessSteps.Add(new ProcessStepDTO(step));
                }
            }
        }

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

        [Display(ResourceType = typeof(EntityStrings), Name = "DepartmentName")]
        public string DepartmentName { get; set; }

        [Display(ResourceType = typeof(EntityStrings), Name = "Supplier")]
        public string SupplierName { get; set; }

        [Display(ResourceType = typeof(EntityStrings), Name = "ProductId")]
        public int ProductId { get; set; }

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
