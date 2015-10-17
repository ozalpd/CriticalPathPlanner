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
    
    public partial class OrderItem : ICreatorId, ICreatorIp, ICreateDate, IModifyNr, IModifierId, IModifierIp, IModifyDate
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OrderItem()
        {
            this.Processes = new HashSet<Process>();
        }
    
        public int Id { get; set; }
        public int PuchaseOrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string Notes { get; set; }
        public int ModifyNr { get; set; }
        public System.DateTime ModifyDate { get; set; }
        public string ModifierId { get; set; }
        public string ModifierIp { get; set; }
        public System.DateTime CreateDate { get; set; }
        public string CreatorId { get; set; }
        public string CreatorIp { get; set; }
    
        public virtual Product Product { get; set; }
        public virtual PuchaseOrder PuchaseOrder { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Process> Processes { get; set; }
    	/// <summary>
    	/// Clones all properties in a new OrderItem instance,
    	/// except PrimaryKey(s)
    	/// </summary>
    	/// <returns>New OrderItem instance</returns>
        public OrderItem Clone()
        {
            var clone = new OrderItem();
            clone.PuchaseOrderId = PuchaseOrderId;
            clone.ProductId = ProductId;
            clone.Quantity = Quantity;
            clone.Notes = Notes;
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
    
    	// Use below function in a partial class file (eg. OrderItem.part.cs)
    	// to add more complexity to clone
        partial void Cloning(OrderItem clone);
    }
    
    //Data Transfer Object type for OrderItem
    public partial class OrderItemDTO
    {
        public OrderItemDTO() { }
    
        public OrderItemDTO(OrderItem entity)
        {
            Id = entity.Id;
            PuchaseOrderId = entity.PuchaseOrderId;
            ProductId = entity.ProductId;
            Quantity = entity.Quantity;
            Notes = entity.Notes;
        
            Initilazing(entity);
        }
    
        partial void Initilazing(OrderItem entity);
        
        public virtual OrderItem ToOrderItem()
        {
            var entity = new OrderItem();
            entity.Id = Id;
            entity.PuchaseOrderId = PuchaseOrderId;
            entity.ProductId = ProductId;
            entity.Quantity = Quantity;
            entity.Notes = Notes;
    
            Converting(entity);
    
            return entity;
        }
    
        partial void Converting(OrderItem entity);
      
        public int Id { get; set; }
        public int PuchaseOrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string Notes { get; set; }
    }
}
