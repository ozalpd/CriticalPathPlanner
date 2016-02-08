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
    [MetadataTypeAttribute(typeof(POAttachmentDTO.POAttachmentMetadata))]
    public partial class POAttachmentDTO
	{
        internal sealed partial class POAttachmentMetadata
		{
            // This metadata class is not intended to be instantiated.
            private POAttachmentMetadata() { }

            [Required(ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(EntityStrings), Name = "PurchaseOrderId")]
            public int PurchaseOrderId { get; set; }

            [StringLength(256, ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "MaxLeght")]
            [Required(ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "Required")]
            [DataType(DataType.Url)]
            [Display(ResourceType = typeof(EntityStrings), Name = "FileUrl")]
            public string FileUrl { get; set; }

            [StringLength(64, ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "MaxLeght")]
            [Required(ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(EntityStrings), Name = "FileTitle")]
            public string FileTitle { get; set; }

            [StringLength(64, ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "MaxLeght")]
            [Required(ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(EntityStrings), Name = "FileName")]
            public string FileName { get; set; }

            [StringLength(16, ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "MaxLeght")]
            [Required(ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(EntityStrings), Name = "FileType")]
            public string FileType { get; set; }

            [Required(ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(EntityStrings), Name = "DisplayOrder")]
            public int DisplayOrder { get; set; }

		}
	}
}