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
    
    public partial class Supplier : Company
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Supplier()
        {
            this.Products = new HashSet<Product>();
            this.Manufacturers = new HashSet<Manufacturer>();
            this.PurchaseOrders = new HashSet<PurchaseOrder>();
        }
    
        public string SupplierCode { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Products { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Manufacturer> Manufacturers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; }
    	/// <summary>
    	/// Clones all properties in a new Supplier instance,
    	/// except PrimaryKey(s)
    	/// </summary>
    	/// <returns>New Supplier instance</returns>
        public Supplier Clone()
        {
            var clone = new Supplier();
            clone.CompanyName = CompanyName;
            clone.Phone1 = Phone1;
            clone.Phone2 = Phone2;
            clone.Phone3 = Phone3;
            clone.Address1 = Address1;
            clone.Address2 = Address2;
            clone.ZipCode = ZipCode;
            clone.City = City;
            clone.State = State;
            clone.CountryId = CountryId;
            clone.Discontinued = Discontinued;
            clone.DiscontinueDate = DiscontinueDate;
            clone.DiscontinueNotes = DiscontinueNotes;
            clone.Notes = Notes;
            clone.ModifyNr = ModifyNr;
            clone.ModifyDate = ModifyDate;
            clone.DiscontinuedUserId = DiscontinuedUserId;
            clone.DiscontinuedUserIp = DiscontinuedUserIp;
            clone.ModifierId = ModifierId;
            clone.ModifierIp = ModifierIp;
            clone.CreateDate = CreateDate;
            clone.CreatorId = CreatorId;
            clone.CreatorIp = CreatorIp;
            clone.SupplierCode = SupplierCode;
    
            Cloning(clone);
    
            return clone;
        }
    
    	// Use below function in a partial class file (eg. Supplier.part.cs)
    	// to add more complexity to clone
        partial void Cloning(Supplier clone);
    }
    
    //Data Transfer Object type for Supplier
    public partial class SupplierDTO
    {
        public SupplierDTO() { }
    
        public SupplierDTO(Supplier entity)
        {
            Id = entity.Id;
            CompanyName = entity.CompanyName;
            Phone1 = entity.Phone1;
            Phone2 = entity.Phone2;
            Phone3 = entity.Phone3;
            Address1 = entity.Address1;
            Address2 = entity.Address2;
            ZipCode = entity.ZipCode;
            City = entity.City;
            State = entity.State;
            CountryId = entity.CountryId;
            Discontinued = entity.Discontinued;
            DiscontinueDate = entity.DiscontinueDate;
            DiscontinueNotes = entity.DiscontinueNotes;
            Notes = entity.Notes;
            SupplierCode = entity.SupplierCode;
        
            Initiliazing(entity);
        }
    
        partial void Initiliazing(Supplier entity);
        
        public virtual Supplier ToSupplier()
        {
            var entity = new Supplier();
            entity.Id = Id;
            entity.CompanyName = CompanyName;
            entity.Phone1 = Phone1;
            entity.Phone2 = Phone2;
            entity.Phone3 = Phone3;
            entity.Address1 = Address1;
            entity.Address2 = Address2;
            entity.ZipCode = ZipCode;
            entity.City = City;
            entity.State = State;
            entity.CountryId = CountryId;
            entity.Discontinued = Discontinued;
            entity.DiscontinueDate = DiscontinueDate;
            entity.DiscontinueNotes = DiscontinueNotes;
            entity.Notes = Notes;
            entity.SupplierCode = SupplierCode;
    
            Converting(entity);
    
            return entity;
        }
    
        partial void Converting(Supplier entity);
      
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Phone3 { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int CountryId { get; set; }
        public bool Discontinued { get; set; }
        public Nullable<System.DateTime> DiscontinueDate { get; set; }
        public string DiscontinueNotes { get; set; }
        public string Notes { get; set; }
        public string SupplierCode { get; set; }
    }
}
