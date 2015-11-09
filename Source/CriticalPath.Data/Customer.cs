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
    
    public partial class Customer : Company
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customer()
        {
            this.Orders = new HashSet<PurchaseOrder>();
        }
    
        public string CustomerCode { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PurchaseOrder> Orders { get; set; }
    	/// <summary>
    	/// Clones all properties in a new Customer instance,
    	/// except PrimaryKey(s)
    	/// </summary>
    	/// <returns>New Customer instance</returns>
        public Customer Clone()
        {
            var clone = new Customer();
            clone.CompanyName = CompanyName;
            clone.Phone1 = Phone1;
            clone.Phone2 = Phone2;
            clone.Phone3 = Phone3;
            clone.Address1 = Address1;
            clone.Address2 = Address2;
            clone.City = City;
            clone.State = State;
            clone.ZipCode = ZipCode;
            clone.Country = Country;
            clone.IsActive = IsActive;
            clone.InactivateDate = InactivateDate;
            clone.InactivateNotes = InactivateNotes;
            clone.Notes = Notes;
            clone.ModifyNr = ModifyNr;
            clone.ModifyDate = ModifyDate;
            clone.ModifierId = ModifierId;
            clone.ModifierIp = ModifierIp;
            clone.CreateDate = CreateDate;
            clone.CreatorId = CreatorId;
            clone.CreatorIp = CreatorIp;
            clone.InactivateUserId = InactivateUserId;
            clone.CustomerCode = CustomerCode;
    
            Cloning(clone);
    
            return clone;
        }
    
    	// Use below function in a partial class file (eg. Customer.part.cs)
    	// to add more complexity to clone
        partial void Cloning(Customer clone);
    }
    
    //Data Transfer Object type for Customer
    public partial class CustomerDTO
    {
        public CustomerDTO() { }
    
        public CustomerDTO(Customer entity)
        {
            Id = entity.Id;
            CompanyName = entity.CompanyName;
            Phone1 = entity.Phone1;
            Phone2 = entity.Phone2;
            Phone3 = entity.Phone3;
            Address1 = entity.Address1;
            Address2 = entity.Address2;
            City = entity.City;
            State = entity.State;
            ZipCode = entity.ZipCode;
            Country = entity.Country;
            IsActive = entity.IsActive;
            InactivateDate = entity.InactivateDate;
            InactivateNotes = entity.InactivateNotes;
            Notes = entity.Notes;
            CustomerCode = entity.CustomerCode;
        
            Initiliazing(entity);
        }
    
        partial void Initiliazing(Customer entity);
        
        public virtual Customer ToCustomer()
        {
            var entity = new Customer();
            entity.Id = Id;
            entity.CompanyName = CompanyName;
            entity.Phone1 = Phone1;
            entity.Phone2 = Phone2;
            entity.Phone3 = Phone3;
            entity.Address1 = Address1;
            entity.Address2 = Address2;
            entity.City = City;
            entity.State = State;
            entity.ZipCode = ZipCode;
            entity.Country = Country;
            entity.IsActive = IsActive;
            entity.InactivateDate = InactivateDate;
            entity.InactivateNotes = InactivateNotes;
            entity.Notes = Notes;
            entity.CustomerCode = CustomerCode;
    
            Converting(entity);
    
            return entity;
        }
    
        partial void Converting(Customer entity);
      
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Phone3 { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> InactivateDate { get; set; }
        public string InactivateNotes { get; set; }
        public string Notes { get; set; }
        public string CustomerCode { get; set; }
    }
}
