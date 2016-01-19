using CP.i8n;
using CriticalPath.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            Customer = entity.Customer != null ? new CustomerDTO(entity.Customer) : null;
            CustomerDepartment = entity.CustomerDepartment != null ? new CustomerDepartmentDTO(entity.CustomerDepartment) : null;
            Licensor = entity.Licensor != null ? new LicensorDTO(entity.Licensor) : null;
            Product = entity.Product != null ? new ProductDTO(entity.Product) : null;
            SizingStandard = entity.SizingStandard != null ? new SizingStandardDTO(entity.SizingStandard) : null;

            SellingCurrency = entity.SellingCurrency != null ? new CurrencyDTO(entity.SellingCurrency) : null;
            LicensorCurrency = entity.SellingCurrency != null ? new CurrencyDTO(entity.LicensorCurrency) : null;
            BuyingCurrency = entity.BuyingCurrency != null ? new CurrencyDTO(entity.BuyingCurrency) : null;
            RoyaltyCurrency = entity.RoyaltyCurrency != null ? new CurrencyDTO(entity.RoyaltyCurrency) : null;
            RetailCurrency = entity.BuyingCurrency != null ? new CurrencyDTO(entity.RetailCurrency) : null;
        }

        [Display(ResourceType = typeof(EntityStrings), Name = "Customer")]
        public CustomerDTO Customer { get; set; }

        [Display(ResourceType = typeof(EntityStrings), Name = "CustomerDepartment")]
        public CustomerDepartmentDTO CustomerDepartment { get; set; }

        [Display(ResourceType = typeof(EntityStrings), Name = "Licensor")]
        public LicensorDTO Licensor { get; set; }

        [Display(ResourceType = typeof(EntityStrings), Name = "Product")]
        public ProductDTO Product { get; set; }

        [Display(ResourceType = typeof(EntityStrings), Name = "SizingStandard")]
        public SizingStandardDTO SizingStandard { get; set; }

        [Display(ResourceType = typeof(EntityStrings), Name = "SellingCurrency")]
        public CurrencyDTO SellingCurrency { get; set; }

        [Display(ResourceType = typeof(EntityStrings), Name = "LicensorCurrency")]
        public CurrencyDTO LicensorCurrency { get; set; }

        [Display(ResourceType = typeof(EntityStrings), Name = "BuyingCurrency")]
        public CurrencyDTO BuyingCurrency { get; set; }

        [Display(ResourceType = typeof(EntityStrings), Name = "RoyaltyCurrency")]
        public CurrencyDTO RoyaltyCurrency { get; set; }

        [Display(ResourceType = typeof(EntityStrings), Name = "RetailCurrency")]
        public CurrencyDTO RetailCurrency { get; set; }
    }
}
