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
    
    public partial class PuchaseOrder : ICreatorId, ICreatorIp, ICreateDate, IModifyNr, IModifierId, IModifierIp, IModifyDate, IApproval
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PuchaseOrder()
        {
            this.OrderItems = new HashSet<OrderItem>();
        }
    
        public int Id { get; set; }
        public string Title { get; set; }
        public int CustomerId { get; set; }
        public System.DateTime OrderDate { get; set; }
        public Nullable<System.DateTime> DueDate { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
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
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    	/// <summary>
    	/// Clones all properties in a new PuchaseOrder instance,
    	/// except PrimaryKey(s)
    	/// </summary>
    	/// <returns>New PuchaseOrder instance</returns>
        public PuchaseOrder Clone()
        {
            var clone = new PuchaseOrder();
            clone.Title = Title;
            clone.CustomerId = CustomerId;
            clone.OrderDate = OrderDate;
            clone.DueDate = DueDate;
            clone.Code = Code;
            clone.Description = Description;
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
    
    	// Use below function in a partial class file (eg. PuchaseOrder.part.cs)
    	// to add more complexity to clone
        partial void Cloning(PuchaseOrder clone);
    }
    
    //Data Transfer Object type for PuchaseOrder
    public partial class PuchaseOrderDTO
    {
        public PuchaseOrderDTO() { }
    
        public PuchaseOrderDTO(PuchaseOrder entity)
        {
            Id = entity.Id;
            Title = entity.Title;
            CustomerId = entity.CustomerId;
            OrderDate = entity.OrderDate;
            DueDate = entity.DueDate;
            Code = entity.Code;
            Description = entity.Description;
            Notes = entity.Notes;
            IsApproved = entity.IsApproved;
            ApproveDate = entity.ApproveDate;
        
            Initilazing(entity);
        }
    
        partial void Initilazing(PuchaseOrder entity);
        
        public virtual PuchaseOrder ToPuchaseOrder()
        {
            var entity = new PuchaseOrder();
            entity.Id = Id;
            entity.Title = Title;
            entity.CustomerId = CustomerId;
            entity.OrderDate = OrderDate;
            entity.DueDate = DueDate;
            entity.Code = Code;
            entity.Description = Description;
            entity.Notes = Notes;
            entity.IsApproved = IsApproved;
            entity.ApproveDate = ApproveDate;
    
            Converting(entity);
    
            return entity;
        }
    
        partial void Converting(PuchaseOrder entity);
      
        public int Id { get; set; }
        public string Title { get; set; }
        public int CustomerId { get; set; }
        public System.DateTime OrderDate { get; set; }
        public Nullable<System.DateTime> DueDate { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Notes { get; set; }
        public bool IsApproved { get; set; }
        public Nullable<System.DateTime> ApproveDate { get; set; }
    }
}
