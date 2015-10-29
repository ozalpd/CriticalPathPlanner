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
    
    public partial class PurchaseOrder : ICreatorId, ICreatorIp, ICreateDate, IModifyNr, IModifierId, IModifierIp, IModifyDate, IIsApproved, IApproval, IHasProduct
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PurchaseOrder()
        {
            this.Processes = new HashSet<Process>();
            this.QuantitySizeRates = new HashSet<QuantitySizeRate>();
        }
    
        public int Id { get; set; }
        public string Title { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public int SizeStandardId { get; set; }
        public System.DateTime OrderDate { get; set; }
        public Nullable<System.DateTime> DueDate { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string Notes { get; set; }
        public bool IsApproved { get; set; }
        public Nullable<System.DateTime> ApproveDate { get; set; }
        public string ApprovedUserId { get; set; }
        public string ApprovedUserIp { get; set; }
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
        public virtual ICollection<QuantitySizeRate> QuantitySizeRates { get; set; }
        public virtual SizeStandard SizeStandard { get; set; }
    	/// <summary>
    	/// Clones all properties in a new PurchaseOrder instance,
    	/// except PrimaryKey(s)
    	/// </summary>
    	/// <returns>New PurchaseOrder instance</returns>
        public PurchaseOrder Clone()
        {
            var clone = new PurchaseOrder();
            clone.Title = Title;
            clone.CustomerId = CustomerId;
            clone.ProductId = ProductId;
            clone.SizeStandardId = SizeStandardId;
            clone.OrderDate = OrderDate;
            clone.DueDate = DueDate;
            clone.Code = Code;
            clone.Description = Description;
            clone.Quantity = Quantity;
            clone.UnitPrice = UnitPrice;
            clone.Notes = Notes;
            clone.IsApproved = IsApproved;
            clone.ApproveDate = ApproveDate;
            clone.ApprovedUserId = ApprovedUserId;
            clone.ApprovedUserIp = ApprovedUserIp;
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
            Title = entity.Title;
            CustomerId = entity.CustomerId;
            ProductId = entity.ProductId;
            SizeStandardId = entity.SizeStandardId;
            OrderDate = entity.OrderDate;
            DueDate = entity.DueDate;
            Code = entity.Code;
            Description = entity.Description;
            Quantity = entity.Quantity;
            UnitPrice = entity.UnitPrice;
            Notes = entity.Notes;
            IsApproved = entity.IsApproved;
            ApproveDate = entity.ApproveDate;
        
            Initiliazing(entity);
        }
    
        partial void Initiliazing(PurchaseOrder entity);
        
        public virtual PurchaseOrder ToPurchaseOrder()
        {
            var entity = new PurchaseOrder();
            entity.Id = Id;
            entity.Title = Title;
            entity.CustomerId = CustomerId;
            entity.ProductId = ProductId;
            entity.SizeStandardId = SizeStandardId;
            entity.OrderDate = OrderDate;
            entity.DueDate = DueDate;
            entity.Code = Code;
            entity.Description = Description;
            entity.Quantity = Quantity;
            entity.UnitPrice = UnitPrice;
            entity.Notes = Notes;
            entity.IsApproved = IsApproved;
            entity.ApproveDate = ApproveDate;
    
            Converting(entity);
    
            return entity;
        }
    
        partial void Converting(PurchaseOrder entity);
      
        public int Id { get; set; }
        public string Title { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public int SizeStandardId { get; set; }
        public System.DateTime OrderDate { get; set; }
        public Nullable<System.DateTime> DueDate { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string Notes { get; set; }
        public bool IsApproved { get; set; }
        public Nullable<System.DateTime> ApproveDate { get; set; }
    }
}
