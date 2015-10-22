SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProcessStepTemplates](
    [Id] [int] Identity(1,1) Not Null,
    [Title] [nVarChar](128) Not Null,
    [DisplayOrder] [int] Not Null,
    [ProcessTemplateId] [int] Not Null Constraint FK_ProcessStepTemplate_ProcessTemplateId Foreign Key References [dbo].[ProcessTemplates]([Id]),
    [RequiredWorkDays] [int] Not Null,
    [ModifyNr] [int] Not Null Default 1,
    [ModifyDate] [DateTime] Not Null Default GetDate(),
    [ModifierId] [VarChar](48) Not Null,
    [ModifierIp] [VarChar](48) Not Null,
    [CreateDate] [DateTime] Not Null Default GetDate(),
    [CreatorId] [VarChar](48) Not Null,
    [CreatorIp] [VarChar](48) Not Null,
  CONSTRAINT [PK_ProcessStepTemplates] PRIMARY KEY CLUSTERED ([Id] ASC)
  WITH (PAD_INDEX  = OFF,
    STATISTICS_NORECOMPUTE  = OFF,
    IGNORE_DUP_KEY = OFF,
    ALLOW_ROW_LOCKS  = ON,
    ALLOW_PAGE_LOCKS  = ON)
  ON [PRIMARY]) ON [PRIMARY]
Go
Create Nonclustered Index [idx_ProcessStepTemplates_DisplayOrder] On [dbo].[ProcessStepTemplates]([DisplayOrder] Asc)
Go
Create Nonclustered Index [idx_ProcessStepTemplates_ProcessTemplateId] On [dbo].[ProcessStepTemplates]([ProcessTemplateId] Asc)
Go
Create Nonclustered Index [idx_ProcessStepTemplates_ModifyDate] On [dbo].[ProcessStepTemplates]([ModifyDate] Desc)
Go
