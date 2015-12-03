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
    [MetadataTypeAttribute(typeof(Country.CountryMetadata))]
    public partial class Country
	{
        internal sealed partial class CountryMetadata
		{
            // This metadata class is not intended to be instantiated.
            private CountryMetadata() { }

            [StringLength(100, ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "MaxLeght")]
            [Required(ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(EntityStrings), Name = "CountryName")]
            public string CountryName { get; set; }

            [StringLength(2, ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "MaxLeght")]
            [Display(ResourceType = typeof(EntityStrings), Name = "TwoLetterIsoCode")]
            public string TwoLetterIsoCode { get; set; }

            [StringLength(3, ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "MaxLeght")]
            [Display(ResourceType = typeof(EntityStrings), Name = "ThreeLetterIsoCode")]
            public string ThreeLetterIsoCode { get; set; }

            [Required(ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(EntityStrings), Name = "NumericIsoCode")]
            public int NumericIsoCode { get; set; }

            [Required(ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(EntityStrings), Name = "DisplayOrder")]
            public int DisplayOrder { get; set; }

            [Display(ResourceType = typeof(EntityStrings), Name = "IsPublished")]
            public bool IsPublished { get; set; }

		}
	}
}