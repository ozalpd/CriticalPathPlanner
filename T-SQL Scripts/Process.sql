SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Processes](
    [Id] [int] Identity(1,1) Not Null,
    [IsApproved] [bit] Not Null,
    [ApproveDate] [DateTime] Null,
    [Title] [nVarChar](128) Not Null,
    [Description] [nVarChar](256) Null,
    [IsCompleted] [bit] Not Null,
    [ProcessTemplateId] [int] Not Null Constraint FK_Process_ProcessTemplateId Foreign Key References [dbo].[ProcessTemplates]([Id]),
    [PurchaseOrderId] [int] Not Null Constraint FK_Process_PurchaseOrderId Foreign Key References [dbo].[PurchaseOrders]([Id]),
    [TargetDate] [DateTime] Not Null,
    [ForecastDate] [DateTime] Null,
    [RealizedDate] [DateTime] Null,
    [ApprovedUserId] [VarChar](48) Null,
    [ApprovedUserIp] [VarChar](48) Null,
    [ModifyNr] [int] Not Null Default 1,
    [ModifyDate] [DateTime] Not Null Default GetDate(),
    [ModifierId] [VarChar](48) Not Null,
    [ModifierIp] [VarChar](48) Not Null,
    [CreateDate] [DateTime] Not Null Default GetDate(),
    [CreatorId] [VarChar](48) Not Null,
    [CreatorIp] [VarChar](48) Not Null,
  CONSTRAINT [PK_Processes] PRIMARY KEY CLUSTERED ([Id] ASC)
  WITH (PAD_INDEX  = OFF,
    STATISTICS_NORECOMPUTE  = OFF,
    IGNORE_DUP_KEY = OFF,
    ALLOW_ROW_LOCKS  = ON,
    ALLOW_PAGE_LOCKS  = ON)
  ON [PRIMARY]) ON [PRIMARY]
Go
Create Nonclustered Index [idx_Processes_IsApproved] On [dbo].[Processes]([IsApproved] Asc)
Go
Create Nonclustered Index [idx_Processes_IsCompleted] On [dbo].[Processes]([IsCompleted] Asc)
Go
Create Nonclustered Index [idx_Processes_ProcessTemplateId] On [dbo].[Processes]([ProcessTemplateId] Asc)
Go
Create Nonclustered Index [idx_Processes_ModifyDate] On [dbo].[Processes]([ModifyDate] Desc)
Go
