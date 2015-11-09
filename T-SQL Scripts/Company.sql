SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Companies](
    [Id] [int] Identity(1,1) Not Null,
    [CompanyName] [nVarChar](128) Not Null,
    [Phone1] [nVarChar](128) Null,
    [Phone2] [nVarChar](64) Null,
    [Phone3] [nVarChar](64) Null,
    [Address1] [nVarChar](128) Not Null,
    [Address2] [nVarChar](128) Null,
    [City] [nVarChar](64) Not Null,
    [State] [nVarChar](32) Null,
    [ZipCode] [nVarChar](32) Null,
    [Country] [nVarChar](32) Null,
    [Notes] [nVarChar](2048) Null,
    [IsActive] [bit] Not Null,
    [InactivateDate] [DateTime] Null,
    [InactivateNotes] [nVarChar](max) Null,
    [InactivateUserId] [VarChar](48) Null Constraint FK_Company_InactivateUserId Foreign Key References [dbo].[AspNetUsers]([Id]),
    [ModifyNr] [int] Not Null Default 1,
    [ModifyDate] [DateTime] Not Null Default GetDate(),
    [ModifierId] [VarChar](48) Not Null Constraint FK_Company_ModifierId Foreign Key References [dbo].[AspNetUsers]([Id]),
    [ModifierIp] [VarChar](48) Not Null,
    [CreateDate] [DateTime] Not Null Default GetDate(),
    [CreatorId] [VarChar](48) Not Null Constraint FK_Company_CreatorId Foreign Key References [dbo].[AspNetUsers]([Id]),
    [CreatorIp] [VarChar](48) Not Null,
  CONSTRAINT [PK_Companies] PRIMARY KEY CLUSTERED ([Id] ASC)
  WITH (PAD_INDEX  = OFF,
    STATISTICS_NORECOMPUTE  = OFF,
    IGNORE_DUP_KEY = OFF,
    ALLOW_ROW_LOCKS  = ON,
    ALLOW_PAGE_LOCKS  = ON)
  ON [PRIMARY]) ON [PRIMARY]
Go
Create Nonclustered Index [idx_Companies_CompanyName] On [dbo].[Companies]([CompanyName] Asc)
Go
Create Nonclustered Index [idx_Companies_City] On [dbo].[Companies]([City] Asc)
Go
Create Nonclustered Index [idx_Companies_IsActive] On [dbo].[Companies]([IsActive] Asc)
Go
Create Nonclustered Index [idx_Companies_ModifyDate] On [dbo].[Companies]([ModifyDate] Desc)
Go
