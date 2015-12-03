using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using CP.i8n;
//------------------------------------------------------------------------------
//
//     This code was generated by OzzCodeGen.
//
//     Manual changes to this file will be overwritten if the code is regenerated.
//
//------------------------------------------------------------------------------

namespace CriticalPath.Data
{
    [MetadataTypeAttribute(typeof(PurchaseOrderDTO.PurchaseOrderMetadata))]
    public partial class PurchaseOrderDTO
	{
        internal sealed partial class PurchaseOrderMetadata
		{
            // This metadata class is not intended to be instantiated.
            private PurchaseOrderMetadata() { }

            [Display(ResourceType = typeof(EntityStrings), Name = "IsApproved")]
            public bool IsApproved { get; set; }

            [DataType(DataType.Date)]
            [Display(ResourceType = typeof(EntityStrings), Name = "ApproveDate")]
            public DateTime ApproveDate { get; set; }

            [StringLength(64, ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "MaxLeght")]
            [Required(ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(EntityStrings), Name = "PoNr")]
            public string PoNr { get; set; }

            [Required(ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "Required")]
            [DataType(DataType.Date)]
            [Display(ResourceType = typeof(EntityStrings), Name = "OrderDate")]
            public DateTime OrderDate { get; set; }

            [DataType(DataType.Date)]
            [Display(ResourceType = typeof(EntityStrings), Name = "DueDate")]
            public DateTime DueDate { get; set; }

            [Required(ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(EntityStrings), Name = "ProductId")]
            public int ProductId { get; set; }

            [StringLength(256, ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "MaxLeght")]
            [DataType(DataType.Text)]
            [Display(ResourceType = typeof(EntityStrings), Name = "Description")]
            public string Description { get; set; }

            [Required(ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(EntityStrings), Name = "Quantity")]
            public int Quantity { get; set; }

            [Required(ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(EntityStrings), Name = "DiscountRate")]
            public decimal DiscountRate { get; set; }

            [Required(ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(EntityStrings), Name = "UnitPrice")]
            public decimal UnitPrice { get; set; }

            [Required(ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(EntityStrings), Name = "SellingCurrencyId")]
            public int SellingCurrencyId { get; set; }

            [Display(ResourceType = typeof(EntityStrings), Name = "BuyingPrice")]
            public decimal BuyingPrice { get; set; }

            [Display(ResourceType = typeof(EntityStrings), Name = "BuyingCurrencyId")]
            public int BuyingCurrencyId { get; set; }

            [Display(ResourceType = typeof(EntityStrings), Name = "RoyaltyFee")]
            public decimal RoyaltyFee { get; set; }

            [Display(ResourceType = typeof(EntityStrings), Name = "RoyaltyCurrencyId")]
            public int RoyaltyCurrencyId { get; set; }

            [Display(ResourceType = typeof(EntityStrings), Name = "RetailPrice")]
            public decimal RetailPrice { get; set; }

            [Display(ResourceType = typeof(EntityStrings), Name = "RetailCurrencyId")]
            public int RetailCurrencyId { get; set; }

            [Required(ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(EntityStrings), Name = "CustomerId")]
            public int CustomerId { get; set; }

            [StringLength(64, ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "MaxLeght")]
            [Display(ResourceType = typeof(EntityStrings), Name = "CustomerPoNr")]
            public string CustomerPoNr { get; set; }

            [Required(ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(EntityStrings), Name = "FreightTermId")]
            public int FreightTermId { get; set; }

            [Required(ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(EntityStrings), Name = "SupplierId")]
            public int SupplierId { get; set; }

            [Required(ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "Required")]
            [DataType(DataType.Date)]
            [Display(ResourceType = typeof(EntityStrings), Name = "SupplierDueDate")]
            public DateTime SupplierDueDate { get; set; }

            [Required(ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(EntityStrings), Name = "SizingStandardId")]
            public int SizingStandardId { get; set; }

            [Required(ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "Required")]
            [Range(6,1000000)]
            [Display(ResourceType = typeof(EntityStrings), Name = "SizeRatioDivisor")]
            public int SizeRatioDivisor { get; set; }

            [DataType(DataType.MultilineText)]
            [Display(ResourceType = typeof(EntityStrings), Name = "Notes")]
            public string Notes { get; set; }

            [UIHint("BoolRed")]
            [Display(ResourceType = typeof(EntityStrings), Name = "Cancelled")]
            public bool Cancelled { get; set; }

            [DataType(DataType.Date)]
            [Display(ResourceType = typeof(EntityStrings), Name = "CancelDate")]
            public DateTime CancelDate { get; set; }

            [DataType(DataType.MultilineText)]
            [Display(ResourceType = typeof(EntityStrings), Name = "CancellationReason")]
            public string CancellationReason { get; set; }

		}
	}
}