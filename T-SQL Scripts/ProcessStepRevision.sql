SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProcessStepRevisions](
    [Id] [int] Identity(1,1) Not Null,
    [ProcessStepId] [int] Not Null Constraint FK_ProcessStepRevision_ProcessStepId Foreign Key References [dbo].[ProcessSteps]([Id]),
    [IsCompleted] [bit] Not Null,
    [TargetDate] [DateTime] Null,
    [ForecastDate] [DateTime] Null,
    [RealizedDate] [DateTime] Null,
    [CreateDate] [DateTime] Not Null Default GetDate(),
    [CreatorId] [VarChar](48) Not Null Constraint FK_ProcessStepRevision_CreatorId Foreign Key References [dbo].[AspNetUsers]([Id]),
    [CreatorIp] [VarChar](48) Not Null,
  CONSTRAINT [PK_ProcessStepRevisions] PRIMARY KEY CLUSTERED ([Id] ASC)
  WITH (PAD_INDEX  = OFF,
    STATISTICS_NORECOMPUTE  = OFF,
    IGNORE_DUP_KEY = OFF,
    ALLOW_ROW_LOCKS  = ON,
    ALLOW_PAGE_LOCKS  = ON)
  ON [PRIMARY]) ON [PRIMARY]
Go
