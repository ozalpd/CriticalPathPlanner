SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProcessTemplates](
    [Id] [int] Identity(1,1) Not Null,
    [IsApproved] [bit] Not Null,
    [ApproveDate] [DateTime] Null,
    [TemplateName] [nVarChar](128) Not Null,
    [DefaultTitle] [nVarChar](128) Not Null,
    [ApprovedUserId] [VarChar](48) Null Constraint FK_ProcessTemplate_ApprovedUserId Foreign Key References [dbo].[AspNetUsers]([Id]),
    [ApprovedUserIp] [VarChar](48) Null,
    [ModifyNr] [int] Not Null Default 1,
    [ModifyDate] [DateTime] Not Null Default GetDate(),
    [ModifierId] [VarChar](48) Not Null Constraint FK_ProcessTemplate_ModifierId Foreign Key References [dbo].[AspNetUsers]([Id]),
    [ModifierIp] [VarChar](48) Not Null,
    [CreateDate] [DateTime] Not Null Default GetDate(),
    [CreatorId] [VarChar](48) Not Null Constraint FK_ProcessTemplate_CreatorId Foreign Key References [dbo].[AspNetUsers]([Id]),
    [CreatorIp] [VarChar](48) Not Null,
  CONSTRAINT [PK_ProcessTemplates] PRIMARY KEY CLUSTERED ([Id] ASC)
  WITH (PAD_INDEX  = OFF,
    STATISTICS_NORECOMPUTE  = OFF,
    IGNORE_DUP_KEY = OFF,
    ALLOW_ROW_LOCKS  = ON,
    ALLOW_PAGE_LOCKS  = ON)
  ON [PRIMARY]) ON [PRIMARY]
Go
Create Nonclustered Index [idx_ProcessTemplates_IsApproved] On [dbo].[ProcessTemplates]([IsApproved] Asc)
Go
Create Nonclustered Index [idx_ProcessTemplates_TemplateName] On [dbo].[ProcessTemplates]([TemplateName] Asc)
Go
Create Nonclustered Index [idx_ProcessTemplates_ModifyDate] On [dbo].[ProcessTemplates]([ModifyDate] Desc)
Go
