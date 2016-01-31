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
            
            Product = entity.Product != null ? new ProductDTO(entity.Product) : null;
            if(entity.Licensor != null)
            {
                Licensor = new LicensorDTO(entity.Licensor);
                LicensorName = Licensor.CompanyName;
            }

            SizingStandard = entity.SizingStandard != null ? new SizingStandardDTO(entity.SizingStandard) : null;

            FreightTerm = entity.FreightTerm != null ? new FreightTermDTO(entity.FreightTerm) : null;

            SellingCurrency = entity.SellingCurrency != null ? new CurrencyDTO(entity.SellingCurrency) : null;
            SellingCurrency2 = entity.SellingCurrency2 != null ? new CurrencyDTO(entity.SellingCurrency2) : null;
            LicensorCurrency = entity.LicensorCurrency != null ? new CurrencyDTO(entity.LicensorCurrency) : null;
            BuyingCurrency = entity.BuyingCurrency != null ? new CurrencyDTO(entity.BuyingCurrency) : null;
            BuyingCurrency2 = entity.BuyingCurrency2 != null ? new CurrencyDTO(entity.BuyingCurrency2) : null;
            RoyaltyCurrency = entity.RoyaltyCurrency != null ? new CurrencyDTO(entity.RoyaltyCurrency) : null;
            RetailCurrency = entity.RetailCurrency != null ? new CurrencyDTO(entity.RetailCurrency) : null;
        }


        public string ProductImage
        {
            get
            {
                if (string.IsNullOrEmpty(_productImage) &&
                    (Product == null || String.IsNullOrEmpty(Product.ImageUrl)))
                {
                    return AppSettings.Urls.NoImageAvailable;
                }
                else if (string.IsNullOrEmpty(_productImage))
                {
                    _productImage = string.Format("{0}/{1}", AppSettings.Urls.ProductImages, Product.ImageUrl);
                }
                return _productImage;
            }
        }
        private string _productImage;

        public string ProductThumb
        {
            get
            {
                if (string.IsNullOrEmpty(_productThumb) &&
                    (Product == null || String.IsNullOrEmpty(Product.ImageUrl)))
                {
                    return AppSettings.Urls.NoImageAvailable;
                }
                else if (string.IsNullOrEmpty(_productThumb))
                {
                    _productThumb = string.Format("{0}/{1}", AppSettings.Urls.ThumbImages, Product.ImageUrl);
                }
                return _productThumb;
            }
        }
        private string _productThumb;

        [Display(ResourceType = typeof(EntityStrings), Name = "Customer")]
        public CustomerDTO Customer { get; set; }

        [Display(ResourceType = typeof(EntityStrings), Name = "CustomerDepartment")]
        public CustomerDepartmentDTO CustomerDepartment { get; set; }

        [Display(ResourceType = typeof(EntityStrings), Name = "Licensor")]
        public LicensorDTO Licensor { get; set; }

        [Display(ResourceType = typeof(EntityStrings), Name = "Licensor")]
        public string LicensorName { get; set; }

        [Display(ResourceType = typeof(EntityStrings), Name = "Product")]
        public ProductDTO Product { get; set; }

        [Display(ResourceType = typeof(EntityStrings), Name = "SizingStandard")]
        public SizingStandardDTO SizingStandard { get; set; }

        [Display(ResourceType = typeof(EntityStrings), Name = "SellingCurrency")]
        public CurrencyDTO SellingCurrency { get; set; }

        [Display(ResourceType = typeof(EntityStrings), Name = "SellingCurrency2")]
        public CurrencyDTO SellingCurrency2 { get; set; }

        [Display(ResourceType = typeof(EntityStrings), Name = "LicensorCurrency")]
        public CurrencyDTO LicensorCurrency { get; set; }

        [Display(ResourceType = typeof(EntityStrings), Name = "BuyingCurrency")]
        public CurrencyDTO BuyingCurrency { get; set; }

        [Display(ResourceType = typeof(EntityStrings), Name = "BuyingCurrency2")]
        public CurrencyDTO BuyingCurrency2 { get; set; }

        [Display(ResourceType = typeof(EntityStrings), Name = "RoyaltyCurrency")]
        public CurrencyDTO RoyaltyCurrency { get; set; }

        [Display(ResourceType = typeof(EntityStrings), Name = "RetailCurrency")]
        public CurrencyDTO RetailCurrency { get; set; }

        [Display(ResourceType = typeof(EntityStrings), Name = "FreightTerm")]
        public FreightTermDTO FreightTerm { get; set; }
    }
}
