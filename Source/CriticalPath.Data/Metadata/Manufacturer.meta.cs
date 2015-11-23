using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using CP.i8n;
//------------------------------------------------------------------------------
//
//     This code was generated by OzzCodeGen.
//
//     Manual changes to this file will be overwritten if the code is regenerated.
//
//------------------------------------------------------------------------------

namespace CriticalPath.Data
{
    [MetadataTypeAttribute(typeof(Manufacturer.ManufacturerMetadata))]
    public partial class Manufacturer
	{
        internal sealed partial class ManufacturerMetadata
		{
            // This metadata class is not intended to be instantiated.
            private ManufacturerMetadata() { }

            [StringLength(128, ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "MaxLeght")]
            [Required(ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(EntityStrings), Name = "CompanyName")]
            public string CompanyName { get; set; }

            [StringLength(64, ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "MaxLeght")]
            [Required(ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(EntityStrings), Name = "ManufacturerCode")]
            public string ManufacturerCode { get; set; }

            [Required(ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(EntityStrings), Name = "SupplierId")]
            public int SupplierId { get; set; }

            [Display(ResourceType = typeof(EntityStrings), Name = "Supplier")]
            public Supplier Supplier { get; set; }

            [StringLength(128, ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "MaxLeght")]
            [DataType(DataType.PhoneNumber)]
            [Display(ResourceType = typeof(EntityStrings), Name = "Phone1")]
            public string Phone1 { get; set; }

            [StringLength(64, ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "MaxLeght")]
            [DataType(DataType.PhoneNumber)]
            [Display(ResourceType = typeof(EntityStrings), Name = "Phone2")]
            public string Phone2 { get; set; }

            [StringLength(64, ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "MaxLeght")]
            [DataType(DataType.PhoneNumber)]
            [Display(ResourceType = typeof(EntityStrings), Name = "Phone3")]
            public string Phone3 { get; set; }

            [StringLength(128, ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "MaxLeght")]
            [Required(ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(EntityStrings), Name = "Address1")]
            public string Address1 { get; set; }

            [StringLength(128, ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "MaxLeght")]
            [Display(ResourceType = typeof(EntityStrings), Name = "Address2")]
            public string Address2 { get; set; }

            [StringLength(64, ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "MaxLeght")]
            [Required(ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(EntityStrings), Name = "City")]
            public string City { get; set; }

            [StringLength(32, ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "MaxLeght")]
            [Display(ResourceType = typeof(EntityStrings), Name = "State")]
            public string State { get; set; }

            [StringLength(32, ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "MaxLeght")]
            [DataType(DataType.PostalCode)]
            [Display(ResourceType = typeof(EntityStrings), Name = "ZipCode")]
            public string ZipCode { get; set; }

            [StringLength(32, ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "MaxLeght")]
            [Display(ResourceType = typeof(EntityStrings), Name = "Country")]
            public string Country { get; set; }

            [StringLength(2048, ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "MaxLeght")]
            [DataType(DataType.MultilineText)]
            [Display(ResourceType = typeof(EntityStrings), Name = "Notes")]
            public string Notes { get; set; }

            [Display(ResourceType = typeof(EntityStrings), Name = "Contacts")]
            public ICollection<Contact> Contacts { get; set; }

            [UIHint("BoolRed")]
            [Display(ResourceType = typeof(EntityStrings), Name = "Discontinued")]
            public bool Discontinued { get; set; }

            [DataType(DataType.Date)]
            [Display(ResourceType = typeof(EntityStrings), Name = "DiscontinueDate")]
            public DateTime DiscontinueDate { get; set; }

            [DataType(DataType.MultilineText)]
            [Display(ResourceType = typeof(EntityStrings), Name = "DiscontinueNotes")]
            public string DiscontinueNotes { get; set; }

            [Display(ResourceType = typeof(EntityStrings), Name = "DiscontinuedUser")]
            public AspNetUser DiscontinuedUser { get; set; }

            [Display(ResourceType = typeof(EntityStrings), Name = "ModifiedUser")]
            public AspNetUser ModifiedUser { get; set; }

            [Display(ResourceType = typeof(EntityStrings), Name = "CreatedUser")]
            public AspNetUser CreatedUser { get; set; }

		}
	}
}