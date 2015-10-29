using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using CriticalPath.Data.Resources;
//------------------------------------------------------------------------------
//
//     This code was generated by OzzCodeGen.
//
//     Manual changes to this file will be overwritten if the code is regenerated.
//
//------------------------------------------------------------------------------

namespace CriticalPath.Data
{
    [MetadataTypeAttribute(typeof(Size.SizeMetadata))]
    public partial class Size
	{
        internal sealed partial class SizeMetadata
		{
            // This metadata class is not intended to be instantiated.
            private SizeMetadata() { }

            [Required(ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(EntityStrings), Name = "DisplayOrder")]
            public int DisplayOrder { get; set; }

            [StringLength(16, ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "MaxLeght")]
            [Required(ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(EntityStrings), Name = "Title")]
            public string Title { get; set; }

            [Required(ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(EntityStrings), Name = "SizeStandardId")]
            public int SizeStandardId { get; set; }

            [Display(ResourceType = typeof(EntityStrings), Name = "SizeStandard")]
            public SizeStandard SizeStandard { get; set; }

		}
	}
}