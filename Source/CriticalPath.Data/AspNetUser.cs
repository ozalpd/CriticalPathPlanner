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
    
    public partial class AspNetUser
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AspNetUser()
        {
            this.InactivatedCompanies = new HashSet<Company>();
            this.InactivatedContacts = new HashSet<Contact>();
            this.ApprovedProcesses = new HashSet<Process>();
            this.CancelledProcesses = new HashSet<Process>();
            this.ApprovedProcessSteps = new HashSet<ProcessStep>();
            this.ApprovedProcessTemplates = new HashSet<ProcessTemplate>();
            this.InactivatedProducts = new HashSet<Product>();
            this.ApprovedPurchaseOrders = new HashSet<PurchaseOrder>();
            this.CancelledPurchaseOrders = new HashSet<PurchaseOrder>();
            this.ProcessStepRevisions = new HashSet<ProcessStepRevision>();
        }
    
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public Nullable<System.DateTime> LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string UserName { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Company> InactivatedCompanies { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Contact> InactivatedContacts { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Process> ApprovedProcesses { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Process> CancelledProcesses { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProcessStep> ApprovedProcessSteps { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProcessTemplate> ApprovedProcessTemplates { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> InactivatedProducts { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PurchaseOrder> ApprovedPurchaseOrders { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PurchaseOrder> CancelledPurchaseOrders { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProcessStepRevision> ProcessStepRevisions { get; set; }
    	/// <summary>
    	/// Clones all properties in a new AspNetUser instance,
    	/// except PrimaryKey(s)
    	/// </summary>
    	/// <returns>New AspNetUser instance</returns>
        public AspNetUser Clone()
        {
            var clone = new AspNetUser();
            clone.FirstName = FirstName;
            clone.LastName = LastName;
            clone.Email = Email;
            clone.EmailConfirmed = EmailConfirmed;
            clone.PasswordHash = PasswordHash;
            clone.SecurityStamp = SecurityStamp;
            clone.PhoneNumber = PhoneNumber;
            clone.PhoneNumberConfirmed = PhoneNumberConfirmed;
            clone.TwoFactorEnabled = TwoFactorEnabled;
            clone.LockoutEndDateUtc = LockoutEndDateUtc;
            clone.LockoutEnabled = LockoutEnabled;
            clone.AccessFailedCount = AccessFailedCount;
            clone.UserName = UserName;
    
            Cloning(clone);
    
            return clone;
        }
    
    	// Use below function in a partial class file (eg. AspNetUser.part.cs)
    	// to add more complexity to clone
        partial void Cloning(AspNetUser clone);
    }
    
    //Data Transfer Object type for AspNetUser
    public partial class AspNetUserDTO
    {
        public AspNetUserDTO() { }
    
        public AspNetUserDTO(AspNetUser entity)
        {
            Id = entity.Id;
            FirstName = entity.FirstName;
            LastName = entity.LastName;
            Email = entity.Email;
            EmailConfirmed = entity.EmailConfirmed;
            PasswordHash = entity.PasswordHash;
            SecurityStamp = entity.SecurityStamp;
            PhoneNumber = entity.PhoneNumber;
            PhoneNumberConfirmed = entity.PhoneNumberConfirmed;
            TwoFactorEnabled = entity.TwoFactorEnabled;
            LockoutEndDateUtc = entity.LockoutEndDateUtc;
            LockoutEnabled = entity.LockoutEnabled;
            AccessFailedCount = entity.AccessFailedCount;
            UserName = entity.UserName;
        
            Initiliazing(entity);
        }
    
        partial void Initiliazing(AspNetUser entity);
        
        public virtual AspNetUser ToAspNetUser()
        {
            var entity = new AspNetUser();
            entity.Id = Id;
            entity.FirstName = FirstName;
            entity.LastName = LastName;
            entity.Email = Email;
            entity.EmailConfirmed = EmailConfirmed;
            entity.PasswordHash = PasswordHash;
            entity.SecurityStamp = SecurityStamp;
            entity.PhoneNumber = PhoneNumber;
            entity.PhoneNumberConfirmed = PhoneNumberConfirmed;
            entity.TwoFactorEnabled = TwoFactorEnabled;
            entity.LockoutEndDateUtc = LockoutEndDateUtc;
            entity.LockoutEnabled = LockoutEnabled;
            entity.AccessFailedCount = AccessFailedCount;
            entity.UserName = UserName;
    
            Converting(entity);
    
            return entity;
        }
    
        partial void Converting(AspNetUser entity);
      
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public Nullable<System.DateTime> LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string UserName { get; set; }
    }
}
