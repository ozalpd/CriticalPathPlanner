﻿//------------------------------------------------------------------------------
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
    
    #region Generated Interfaces
    
    public interface IDisplayOrder
    {
        int DisplayOrder { get; set; }
    }
    
    public interface ICreatorId
    {
        string CreatorId { get; set; }
    }
    
    public interface ICreatorIp
    {
        string CreatorIp { get; set; }
    }
    
    public interface ICreateDate
    {
        System.DateTime CreateDate { get; set; }
    }
    
    public interface IModifyNr
    {
        int ModifyNr { get; set; }
    }
    
    public interface IModifierId
    {
        string ModifierId { get; set; }
    }
    
    public interface IModifierIp
    {
        string ModifierIp { get; set; }
    }
    
    public interface IModifyDate
    {
        System.DateTime ModifyDate { get; set; }
    }
    
    public interface IIsApproved
    {
        int Id { get; set; }
        Nullable<System.DateTime> ApproveDate { get; set; }
        bool IsApproved { get; set; }
    }
    
    public interface IApproval
    {
        Nullable<System.DateTime> ApproveDate { get; set; }
        bool IsApproved { get; set; }
        string ApprovedUserId { get; set; }
        string ApprovedUserIp { get; set; }
    }
    
    public interface IHasProduct
    {
        int ProductId { get; set; }
    }
    
    public interface IIsActive
    {
        bool IsActive { get; set; }
    }
    #endregion
}
