SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sizings](
    [Id] [int] Identity(1,1) Not Null,
    [DisplayOrder] [int] Not Null,
    [Caption] [nVarChar](16) Not Null,
    [SizingStandardId] [int] Not Null Constraint FK_Sizing_SizingStandardId Foreign Key References [dbo].[SizingStandards]([Id]),
    [ModifyNr] [int] Not Null Default 1,
    [ModifyDate] [DateTime] Not Null Default GetDate(),
    [ModifierId] [VarChar](48) Not Null,
    [ModifierIp] [VarChar](48) Not Null,
    [CreateDate] [DateTime] Not Null Default GetDate(),
    [CreatorId] [VarChar](48) Not Null,
    [CreatorIp] [VarChar](48) Not Null,
  CONSTRAINT [PK_Sizings] PRIMARY KEY CLUSTERED ([Id] ASC)
  WITH (PAD_INDEX  = OFF,
    STATISTICS_NORECOMPUTE  = OFF,
    IGNORE_DUP_KEY = OFF,
    ALLOW_ROW_LOCKS  = ON,
    ALLOW_PAGE_LOCKS  = ON)
  ON [PRIMARY]) ON [PRIMARY]
Go
Create Nonclustered Index [idx_Sizings_SizingStandardId] On [dbo].[Sizings]([SizingStandardId] Asc)
Go
Create Nonclustered Index [idx_Sizings_ModifyDate] On [dbo].[Sizings]([ModifyDate] Desc)
Go
