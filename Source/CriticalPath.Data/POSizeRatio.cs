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
    
    public partial class POSizeRatio : IDisplayOrder, ICreatorId, ICreatorIp, ICreateDate, IModifyNr, IModifierId, IModifierIp, IModifyDate
    {
        public int Id { get; set; }
        public int DisplayOrder { get; set; }
        public string Caption { get; set; }
        public int Rate { get; set; }
        public int PurchaseOrderId { get; set; }
        public int ModifyNr { get; set; }
        public System.DateTime ModifyDate { get; set; }
        public string ModifierId { get; set; }
        public string ModifierIp { get; set; }
        public System.DateTime CreateDate { get; set; }
        public string CreatorId { get; set; }
        public string CreatorIp { get; set; }
    
        public virtual PurchaseOrder PurchaseOrder { get; set; }
        public virtual AspNetUser CreatedUser { get; set; }
        public virtual AspNetUser ModifiedUser { get; set; }
    	/// <summary>
    	/// Clones all properties in a new POSizeRatio instance,
    	/// except PrimaryKey(s)
    	/// </summary>
    	/// <returns>New POSizeRatio instance</returns>
        public POSizeRatio Clone()
        {
            var clone = new POSizeRatio();
            clone.DisplayOrder = DisplayOrder;
            clone.Caption = Caption;
            clone.Rate = Rate;
            clone.PurchaseOrderId = PurchaseOrderId;
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
    
    	// Use below function in a partial class file (eg. POSizeRatio.part.cs)
    	// to add more complexity to clone
        partial void Cloning(POSizeRatio clone);
    }
    
    //Data Transfer Object type for POSizeRatio
    public partial class POSizeRatioDTO
    {
        public POSizeRatioDTO() { }
    
        public POSizeRatioDTO(POSizeRatio entity)
        {
            Id = entity.Id;
            DisplayOrder = entity.DisplayOrder;
            Caption = entity.Caption;
            Rate = entity.Rate;
            PurchaseOrderId = entity.PurchaseOrderId;
        
            Initiliazing(entity);
        }
    
        partial void Initiliazing(POSizeRatio entity);
        
        public virtual POSizeRatio ToPOSizeRatio()
        {
            var entity = new POSizeRatio();
            entity.Id = Id;
            entity.DisplayOrder = DisplayOrder;
            entity.Caption = Caption;
            entity.Rate = Rate;
            entity.PurchaseOrderId = PurchaseOrderId;
    
            Converting(entity);
    
            return entity;
        }
    
        partial void Converting(POSizeRatio entity);
      
        public int Id { get; set; }
        public int DisplayOrder { get; set; }
        public string Caption { get; set; }
        public int Rate { get; set; }
        public int PurchaseOrderId { get; set; }
    }
}
