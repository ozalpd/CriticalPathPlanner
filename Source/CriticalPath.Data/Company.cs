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
    
    public abstract partial class Company : ICreatorId, ICreatorIp, ICreateDate, IModifyNr, IModifierId, IModifierIp, IModifyDate, IDiscontinued, IDiscontinuedUser
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Company()
        {
            this.Contacts = new HashSet<Contact>();
        }
    
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
        public bool Discontinued { get; set; }
        public Nullable<System.DateTime> DiscontinueDate { get; set; }
        public string DiscontinueNotes { get; set; }
        public string Notes { get; set; }
        public int ModifyNr { get; set; }
        public System.DateTime ModifyDate { get; set; }
        public string DiscontinuedUserId { get; set; }
        public string DiscontinuedUserIp { get; set; }
        public string ModifierId { get; set; }
        public string ModifierIp { get; set; }
        public System.DateTime CreateDate { get; set; }
        public string CreatorId { get; set; }
        public string CreatorIp { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Contact> Contacts { get; set; }
        public virtual AspNetUser DiscontinuedUser { get; set; }
        public virtual AspNetUser ModifiedUser { get; set; }
        public virtual AspNetUser CreatedUser { get; set; }
    }
}
