//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CriticalPath.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class PurchaseOrder : ICreatorId, ICreatorIp, ICreateDate, IModifyNr, IModifierId, IModifierIp, IModifyDate, IIsApproved, IApproval, IHasProduct, ICancelled, ICancellation, IRetailPrice, IRoyaltyFee, IBuyingPrice, ISellingPrice, IAllPrice
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PurchaseOrder()
        {
            this.Processes = new HashSet<Process>();
            this.SizeRatios = new HashSet<SizeRatio>();
        }
    
        public int Id { get; set; }
        public string PoNr { get; set; }
        public System.DateTime OrderDate { get; set; }
        public Nullable<System.DateTime> DueDate { get; set; }
        public string Description { get; set; }
        public int CustomerId { get; set; }
        public string CustomerPoNr { get; set; }
        public int ProductId { get; set; }
        public int SizingStandardId { get; set; }
        public int Quantity { get; set; }
        public decimal DiscountRate { get; set; }
        public decimal UnitPrice { get; set; }
        public int SellingCurrencyId { get; set; }
        public Nullable<decimal> BuyingPrice { get; set; }
        public Nullable<int> BuyingCurrencyId { get; set; }
        public Nullable<decimal> RoyaltyFee { get; set; }
        public Nullable<int> RoyaltyCurrencyId { get; set; }
        public Nullable<decimal> RetailPrice { get; set; }
        public Nullable<int> RetailCurrencyId { get; set; }
        public int SizeRatioDivisor { get; set; }
        public int FreightTermId { get; set; }
        public int SupplierId { get; set; }
        public Nullable<System.DateTime> SupplierDueDate { get; set; }
        public string Notes { get; set; }
        public bool IsApproved { get; set; }
        public Nullable<System.DateTime> ApproveDate { get; set; }
        public string ApprovedUserId { get; set; }
        public string ApprovedUserIp { get; set; }
        public bool Cancelled { get; set; }
        public Nullable<System.DateTime> CancelDate { get; set; }
        public string CancellationReason { get; set; }
        public string CancelledUserId { get; set; }
        public string CancelledUserIp { get; set; }
        public int ModifyNr { get; set; }
        public System.DateTime ModifyDate { get; set; }
        public string ModifierId { get; set; }
        public string ModifierIp { get; set; }
        public System.DateTime CreateDate { get; set; }
        public string CreatorId { get; set; }
        public string CreatorIp { get; set; }
    
        public virtual Customer Customer { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Process> Processes { get; set; }
        public virtual Product Product { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SizeRatio> SizeRatios { get; set; }
        public virtual SizingStandard SizingStandard { get; set; }
        public virtual AspNetUser ApprovedUser { get; set; }
        public virtual AspNetUser CreatedUser { get; set; }
        public virtual AspNetUser CancelledUser { get; set; }
        public virtual AspNetUser ModifiedUser { get; set; }
        public virtual FreightTerm FreightTerm { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual Currency BuyingCurrency { get; set; }
        public virtual Currency RetailCurrency { get; set; }
        public virtual Currency RoyaltyCurrency { get; set; }
        public virtual Currency SellingCurrency { get; set; }
    	/// <summary>
    	/// Clones all properties in a new PurchaseOrder instance,
    	/// except PrimaryKey(s)
    	/// </summary>
    	/// <returns>New PurchaseOrder instance</returns>
        public PurchaseOrder Clone()
        {
            var clone = new PurchaseOrder();
            clone.PoNr = PoNr;
            clone.OrderDate = OrderDate;
            clone.DueDate = DueDate;
            clone.Description = Description;
            clone.CustomerId = CustomerId;
            clone.CustomerPoNr = CustomerPoNr;
            clone.ProductId = ProductId;
            clone.SizingStandardId = SizingStandardId;
            clone.Quantity = Quantity;
            clone.DiscountRate = DiscountRate;
            clone.UnitPrice = UnitPrice;
            clone.SellingCurrencyId = SellingCurrencyId;
            clone.BuyingPrice = BuyingPrice;
            clone.BuyingCurrencyId = BuyingCurrencyId;
            clone.RoyaltyFee = RoyaltyFee;
            clone.RoyaltyCurrencyId = RoyaltyCurrencyId;
            clone.RetailPrice = RetailPrice;
            clone.RetailCurrencyId = RetailCurrencyId;
            clone.SizeRatioDivisor = SizeRatioDivisor;
            clone.FreightTermId = FreightTermId;
            clone.SupplierId = SupplierId;
            clone.SupplierDueDate = SupplierDueDate;
            clone.Notes = Notes;
            clone.IsApproved = IsApproved;
            clone.ApproveDate = ApproveDate;
            clone.ApprovedUserId = ApprovedUserId;
            clone.ApprovedUserIp = ApprovedUserIp;
            clone.Cancelled = Cancelled;
            clone.CancelDate = CancelDate;
            clone.CancellationReason = CancellationReason;
            clone.CancelledUserId = CancelledUserId;
            clone.CancelledUserIp = CancelledUserIp;
            clone.ModifyNr = ModifyNr;
            clone.ModifyDate = ModifyDate;
            clone.ModifierId = ModifierId;
            clone.ModifierIp = ModifierIp;
            clone.CreateDate = CreateDate;
            clone.CreatorId = CreatorId;
            clone.CreatorIp = CreatorIp;
    
            Cloning(clone);
    
            return clone;
        }
    
    	// Use below function in a partial class file (eg. PurchaseOrder.part.cs)
    	// to add more complexity to clone
        partial void Cloning(PurchaseOrder clone);
    }
    
    //Data Transfer Object type for PurchaseOrder
    public partial class PurchaseOrderDTO
    {
        public PurchaseOrderDTO() { }
    
        public PurchaseOrderDTO(PurchaseOrder entity)
        {
            Id = entity.Id;
            PoNr = entity.PoNr;
            OrderDate = entity.OrderDate;
            DueDate = entity.DueDate;
            Description = entity.Description;
            CustomerId = entity.CustomerId;
            CustomerPoNr = entity.CustomerPoNr;
            ProductId = entity.ProductId;
            SizingStandardId = entity.SizingStandardId;
            Quantity = entity.Quantity;
            DiscountRate = entity.DiscountRate;
            UnitPrice = entity.UnitPrice;
            SellingCurrencyId = entity.SellingCurrencyId;
            BuyingPrice = entity.BuyingPrice;
            BuyingCurrencyId = entity.BuyingCurrencyId;
            RoyaltyFee = entity.RoyaltyFee;
            RoyaltyCurrencyId = entity.RoyaltyCurrencyId;
            RetailPrice = entity.RetailPrice;
            RetailCurrencyId = entity.RetailCurrencyId;
            SizeRatioDivisor = entity.SizeRatioDivisor;
            FreightTermId = entity.FreightTermId;
            SupplierId = entity.SupplierId;
            SupplierDueDate = entity.SupplierDueDate;
            Notes = entity.Notes;
            IsApproved = entity.IsApproved;
            ApproveDate = entity.ApproveDate;
            Cancelled = entity.Cancelled;
            CancelDate = entity.CancelDate;
            CancellationReason = entity.CancellationReason;
        
            Initiliazing(entity);
        }
    
        partial void Initiliazing(PurchaseOrder entity);
        
        public virtual PurchaseOrder ToPurchaseOrder()
        {
            var entity = new PurchaseOrder();
            entity.Id = Id;
            entity.PoNr = PoNr;
            entity.OrderDate = OrderDate;
            entity.DueDate = DueDate;
            entity.Description = Description;
            entity.CustomerId = CustomerId;
            entity.CustomerPoNr = CustomerPoNr;
            entity.ProductId = ProductId;
            entity.SizingStandardId = SizingStandardId;
            entity.Quantity = Quantity;
            entity.DiscountRate = DiscountRate;
            entity.UnitPrice = UnitPrice;
            entity.SellingCurrencyId = SellingCurrencyId;
            entity.BuyingPrice = BuyingPrice;
            entity.BuyingCurrencyId = BuyingCurrencyId;
            entity.RoyaltyFee = RoyaltyFee;
            entity.RoyaltyCurrencyId = RoyaltyCurrencyId;
            entity.RetailPrice = RetailPrice;
            entity.RetailCurrencyId = RetailCurrencyId;
            entity.SizeRatioDivisor = SizeRatioDivisor;
            entity.FreightTermId = FreightTermId;
            entity.SupplierId = SupplierId;
            entity.SupplierDueDate = SupplierDueDate;
            entity.Notes = Notes;
            entity.IsApproved = IsApproved;
            entity.ApproveDate = ApproveDate;
            entity.Cancelled = Cancelled;
            entity.CancelDate = CancelDate;
            entity.CancellationReason = CancellationReason;
    
            Converting(entity);
    
            return entity;
        }
    
        partial void Converting(PurchaseOrder entity);
      
        public int Id { get; set; }
        public string PoNr { get; set; }
        public System.DateTime OrderDate { get; set; }
        public Nullable<System.DateTime> DueDate { get; set; }
        public string Description { get; set; }
        public int CustomerId { get; set; }
        public string CustomerPoNr { get; set; }
        public int ProductId { get; set; }
        public int SizingStandardId { get; set; }
        public int Quantity { get; set; }
        public decimal DiscountRate { get; set; }
        public decimal UnitPrice { get; set; }
        public int SellingCurrencyId { get; set; }
        public Nullable<decimal> BuyingPrice { get; set; }
        public Nullable<int> BuyingCurrencyId { get; set; }
        public Nullable<decimal> RoyaltyFee { get; set; }
        public Nullable<int> RoyaltyCurrencyId { get; set; }
        public Nullable<decimal> RetailPrice { get; set; }
        public Nullable<int> RetailCurrencyId { get; set; }
        public int SizeRatioDivisor { get; set; }
        public int FreightTermId { get; set; }
        public int SupplierId { get; set; }
        public Nullable<System.DateTime> SupplierDueDate { get; set; }
        public string Notes { get; set; }
        public bool IsApproved { get; set; }
        public Nullable<System.DateTime> ApproveDate { get; set; }
        public bool Cancelled { get; set; }
        public Nullable<System.DateTime> CancelDate { get; set; }
        public string CancellationReason { get; set; }
    }
}
