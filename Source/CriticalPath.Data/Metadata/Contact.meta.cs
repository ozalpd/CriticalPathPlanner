using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using CriticalPath.Data.Resources;
//------------------------------------------------------------------------------
//
//     This code was generated by OzzCodeGen 10/09/2015 03:17:35
//
//     Manual changes to this file will be overwritten if the code is regenerated.
//
//------------------------------------------------------------------------------

namespace CriticalPath.Data
{
    [MetadataTypeAttribute(typeof(Contact.ContactMetadata))]
    public partial class Contact
	{
        internal sealed partial class ContactMetadata
		{
            // This metadata class is not intended to be instantiated.
            private ContactMetadata() { }

            [StringLength(64, ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "MaxLeght")]
            [Required(ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(EntityStrings), Name = "FirstName")]
            public string FirstName { get; set; }

            [StringLength(64, ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "MaxLeght")]
            [Required(ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(EntityStrings), Name = "LastName")]
            public string LastName { get; set; }

            [StringLength(64, ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "MaxLeght")]
            [EmailAddress]
            [Display(ResourceType = typeof(EntityStrings), Name = "EmailWork")]
            public string EmailWork { get; set; }

            [StringLength(64, ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "MaxLeght")]
            [EmailAddress]
            [Display(ResourceType = typeof(EntityStrings), Name = "EmailHome")]
            public string EmailHome { get; set; }

            [StringLength(64, ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "MaxLeght")]
            [DataType(DataType.PhoneNumber)]
            [Display(ResourceType = typeof(EntityStrings), Name = "PhoneMobile")]
            public string PhoneMobile { get; set; }

            [StringLength(64, ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "MaxLeght")]
            [DataType(DataType.PhoneNumber)]
            [Display(ResourceType = typeof(EntityStrings), Name = "PhoneWork1")]
            public string PhoneWork1 { get; set; }

            [StringLength(64, ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "MaxLeght")]
            [DataType(DataType.PhoneNumber)]
            [Display(ResourceType = typeof(EntityStrings), Name = "PhoneWork2")]
            public string PhoneWork2 { get; set; }

            [StringLength(255, ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "MaxLeght")]
            [DataType(DataType.MultilineText)]
            [Display(ResourceType = typeof(EntityStrings), Name = "Notes")]
            public string Notes { get; set; }

            [Required(ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(EntityStrings), Name = "CompanyId")]
            public int CompanyId { get; set; }

            [Display(ResourceType = typeof(EntityStrings), Name = "Company")]
            public Company Company { get; set; }

		}
	}
}