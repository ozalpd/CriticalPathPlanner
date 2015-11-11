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
    [MetadataTypeAttribute(typeof(Sizing.SizingMetadata))]
    public partial class Sizing
	{
        internal sealed partial class SizingMetadata
		{
            // This metadata class is not intended to be instantiated.
            private SizingMetadata() { }

            [Required(ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(EntityStrings), Name = "DisplayOrder")]
            public int DisplayOrder { get; set; }

            [StringLength(16, ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "MaxLeght")]
            [Required(ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(EntityStrings), Name = "Caption")]
            public string Caption { get; set; }

            [Required(ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(EntityStrings), Name = "SizingStandardId")]
            public int SizingStandardId { get; set; }

            [Display(ResourceType = typeof(EntityStrings), Name = "SizingStandard")]
            public SizingStandard SizingStandard { get; set; }

            [Display(ResourceType = typeof(EntityStrings), Name = "ModifiedUser")]
            public AspNetUser ModifiedUser { get; set; }

            [Display(ResourceType = typeof(EntityStrings), Name = "CreatedUser")]
            public AspNetUser CreatedUser { get; set; }

		}
	}
}