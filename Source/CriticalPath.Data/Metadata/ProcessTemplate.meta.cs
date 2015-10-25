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
    [MetadataTypeAttribute(typeof(ProcessTemplate.ProcessTemplateMetadata))]
    public partial class ProcessTemplate
	{
        internal sealed partial class ProcessTemplateMetadata
		{
            // This metadata class is not intended to be instantiated.
            private ProcessTemplateMetadata() { }

            [StringLength(128, ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "MaxLeght")]
            [Required(ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(EntityStrings), Name = "TemplateName")]
            public string TemplateName { get; set; }

            [StringLength(128, ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "MaxLeght")]
            [Required(ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(EntityStrings), Name = "DefaultTitle")]
            public string DefaultTitle { get; set; }

            [Display(ResourceType = typeof(EntityStrings), Name = "StepTemplates")]
            public ICollection<ProcessStepTemplate> StepTemplates { get; set; }

            [Display(ResourceType = typeof(EntityStrings), Name = "IsApproved")]
            public bool IsApproved { get; set; }

            [DataType(DataType.Date)]
            [Display(ResourceType = typeof(EntityStrings), Name = "ApproveDate")]
            public DateTime ApproveDate { get; set; }

		}
	}
}