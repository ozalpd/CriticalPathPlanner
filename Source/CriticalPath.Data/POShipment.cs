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
    
    public partial class POShipment : ICreatorId, ICreatorIp, ICreateDate, IModifyNr, IModifierId, IModifierIp, IModifyDate
    {
        public int Id { get; set; }
        public string ShippingNr { get; set; }
        public System.DateTime ShippingDate { get; set; }
        public string DeliveryNr { get; set; }
        public Nullable<System.DateTime> DeliveryDate { get; set; }
        public string DestinationNr { get; set; }
        public string RefCode { get; set; }
        public string CustomerRefNr { get; set; }
        public int Quantity { get; set; }
        public bool IsShipped { get; set; }
        public bool IsDelivered { get; set; }
        public int PurchaseOrderId { get; set; }
        public int FreightTermId { get; set; }
        public int ModifyNr { get; set; }
        public System.DateTime ModifyDate { get; set; }
        public string ModifierId { get; set; }
        public string ModifierIp { get; set; }
        public System.DateTime CreateDate { get; set; }
        public string CreatorId { get; set; }
        public string CreatorIp { get; set; }
    
        public virtual AspNetUser CreatedUser { get; set; }
        public virtual AspNetUser ModifiedUser { get; set; }
        public virtual FreightTerm FreightTerm { get; set; }
        public virtual PurchaseOrder PurchaseOrder { get; set; }
    	/// <summary>
    	/// Clones all properties in a new POShipment instance,
    	/// except PrimaryKey(s)
    	/// </summary>
    	/// <returns>New POShipment instance</returns>
        public POShipment Clone()
        {
            var clone = new POShipment();
            clone.ShippingNr = ShippingNr;
            clone.ShippingDate = ShippingDate;
            clone.DeliveryNr = DeliveryNr;
            clone.DeliveryDate = DeliveryDate;
            clone.DestinationNr = DestinationNr;
            clone.RefCode = RefCode;
            clone.CustomerRefNr = CustomerRefNr;
            clone.Quantity = Quantity;
            clone.IsShipped = IsShipped;
            clone.IsDelivered = IsDelivered;
            clone.PurchaseOrderId = PurchaseOrderId;
            clone.FreightTermId = FreightTermId;
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
    
    	// Use below function in a partial class file (eg. POShipment.part.cs)
    	// to add more complexity to clone
        partial void Cloning(POShipment clone);
    }
    
    //Data Transfer Object type for POShipment
    public partial class POShipmentDTO
    {
        public POShipmentDTO() { }
    
        public POShipmentDTO(POShipment entity)
        {
            Id = entity.Id;
            ShippingNr = entity.ShippingNr;
            ShippingDate = entity.ShippingDate;
            DeliveryNr = entity.DeliveryNr;
            DeliveryDate = entity.DeliveryDate;
            DestinationNr = entity.DestinationNr;
            RefCode = entity.RefCode;
            CustomerRefNr = entity.CustomerRefNr;
            Quantity = entity.Quantity;
            IsShipped = entity.IsShipped;
            IsDelivered = entity.IsDelivered;
            PurchaseOrderId = entity.PurchaseOrderId;
            FreightTermId = entity.FreightTermId;
        
            Initiliazing(entity);
        }
    
        partial void Initiliazing(POShipment entity);
        
        public virtual POShipment ToPOShipment()
        {
            var entity = new POShipment();
            entity.Id = Id;
            entity.ShippingNr = ShippingNr;
            entity.ShippingDate = ShippingDate;
            entity.DeliveryNr = DeliveryNr;
            entity.DeliveryDate = DeliveryDate;
            entity.DestinationNr = DestinationNr;
            entity.RefCode = RefCode;
            entity.CustomerRefNr = CustomerRefNr;
            entity.Quantity = Quantity;
            entity.IsShipped = IsShipped;
            entity.IsDelivered = IsDelivered;
            entity.PurchaseOrderId = PurchaseOrderId;
            entity.FreightTermId = FreightTermId;
    
            Converting(entity);
    
            return entity;
        }
    
        partial void Converting(POShipment entity);
      
        public int Id { get; set; }
        public string ShippingNr { get; set; }
        public System.DateTime ShippingDate { get; set; }
        public string DeliveryNr { get; set; }
        public Nullable<System.DateTime> DeliveryDate { get; set; }
        public string DestinationNr { get; set; }
        public string RefCode { get; set; }
        public string CustomerRefNr { get; set; }
        public int Quantity { get; set; }
        public bool IsShipped { get; set; }
        public bool IsDelivered { get; set; }
        public int PurchaseOrderId { get; set; }
        public int FreightTermId { get; set; }
    }
}
