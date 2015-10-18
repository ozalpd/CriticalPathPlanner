SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProcessSteps](
    [Id] [int] Identity(1,1) Not Null,
    [Title] [nVarChar](128) Not Null,
    [IsCompleted] [bit] Not Null,
    [Description] [nVarChar](256) Null,
    [DisplayOrder] [int] Not Null,
    [ProcessId] [int] Not Null Constraint FK_ProcessStep_ProcessId Foreign Key References [dbo].[Processes]([Id]),
    [TargetDate] [DateTime] Not Null,
    [ForecastDate] [DateTime] Not Null,
    [RealizedDate] [DateTime] Not Null,
    [IsApproved] [bit] Not Null,
    [ApproveDate] [DateTime] Null,
    [ApprovedUserId] [VarChar](48) Null,
    [ApprovedUserIp] [VarChar](48) Null,
    [ModifyNr] [int] Not Null Default 1,
    [ModifyDate] [DateTime] Not Null Default GetDate(),
    [ModifierId] [VarChar](48) Not Null,
    [ModifierIp] [VarChar](48) Not Null,
    [CreateDate] [DateTime] Not Null Default GetDate(),
    [CreatorId] [VarChar](48) Not Null,
    [CreatorIp] [VarChar](48) Not Null,
  CONSTRAINT [PK_ProcessSteps] PRIMARY KEY CLUSTERED ([Id] ASC)
  WITH (PAD_INDEX  = OFF,
    STATISTICS_NORECOMPUTE  = OFF,
    IGNORE_DUP_KEY = OFF,
    ALLOW_ROW_LOCKS  = ON,
    ALLOW_PAGE_LOCKS  = ON)
  ON [PRIMARY]) ON [PRIMARY]
Go
Create Nonclustered Index [idx_ProcessSteps_IsCompleted] On [dbo].[ProcessSteps]([IsCompleted] Asc)
Go
Create Nonclustered Index [idx_ProcessSteps_DisplayOrder] On [dbo].[ProcessSteps]([DisplayOrder] Asc)
Go
Create Nonclustered Index [idx_ProcessSteps_ProcessId] On [dbo].[ProcessSteps]([ProcessId] Asc)
Go
Create Nonclustered Index [idx_ProcessSteps_IsApproved] On [dbo].[ProcessSteps]([IsApproved] Asc)
Go
Create Nonclustered Index [idx_ProcessSteps_ModifyDate] On [dbo].[ProcessSteps]([ModifyDate] Desc)
Go
