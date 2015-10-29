SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contacts](
    [Id] [int] Identity(1,1) Not Null,
    [FirstName] [nVarChar](64) Not Null,
    [LastName] [nVarChar](64) Not Null,
    [CompanyId] [int] Not Null Constraint FK_Contact_CompanyId Foreign Key References [dbo].[Companies]([Id]),
    [EmailWork] [nVarChar](64) Null,
    [EmailHome] [nVarChar](64) Null,
    [PhoneMobile] [nVarChar](64) Null,
    [PhoneWork1] [nVarChar](64) Null,
    [PhoneWork2] [nVarChar](64) Null,
    [Notes] [nVarChar](255) Null,
    [ModifyNr] [int] Not Null Default 1,
    [ModifyDate] [DateTime] Not Null Default GetDate(),
    [ModifierId] [VarChar](48) Not Null,
    [ModifierIp] [VarChar](48) Not Null,
    [CreateDate] [DateTime] Not Null Default GetDate(),
    [CreatorId] [VarChar](48) Not Null,
    [CreatorIp] [VarChar](48) Not Null,
  CONSTRAINT [PK_Contacts] PRIMARY KEY CLUSTERED ([Id] ASC)
  WITH (PAD_INDEX  = OFF,
    STATISTICS_NORECOMPUTE  = OFF,
    IGNORE_DUP_KEY = OFF,
    ALLOW_ROW_LOCKS  = ON,
    ALLOW_PAGE_LOCKS  = ON)
  ON [PRIMARY]) ON [PRIMARY]
Go
Create Nonclustered Index [idx_Contacts_FirstName] On [dbo].[Contacts]([FirstName] Asc)
Go
Create Nonclustered Index [idx_Contacts_LastName] On [dbo].[Contacts]([LastName] Asc)
Go
Create Nonclustered Index [idx_Contacts_CompanyId] On [dbo].[Contacts]([CompanyId] Asc)
Go
Create Nonclustered Index [idx_Contacts_ModifyDate] On [dbo].[Contacts]([ModifyDate] Desc)
Go
