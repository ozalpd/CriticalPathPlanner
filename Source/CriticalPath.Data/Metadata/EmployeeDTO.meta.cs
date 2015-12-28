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
    [MetadataTypeAttribute(typeof(EmployeeDTO.EmployeeMetadata))]
    public partial class EmployeeDTO
	{
        internal sealed partial class EmployeeMetadata
		{
            // This metadata class is not intended to be instantiated.
            private EmployeeMetadata() { }

            [Display(ResourceType = typeof(EntityStrings), Name = "IsActive")]
            public bool IsActive { get; set; }

            [DataType(DataType.Date)]
            [Display(ResourceType = typeof(EntityStrings), Name = "InactivateDate")]
            public DateTime InactivateDate { get; set; }

		}
	}
}