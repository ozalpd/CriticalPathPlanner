SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductCategories](
    [Id] [int] Identity(1,1) Not Null,
    [Title] [nVarChar](128) Not Null,
    [Code] [nVarChar](48) Null,
    [Description] [nVarChar](256) Null,
    [ParentCategoryId] [int] Null Constraint FK_ProductCategory_ParentCategoryId Foreign Key References [dbo].[ProductCategories]([Id]),
    [ModifyNr] [int] Not Null Default 1,
    [ModifyDate] [DateTime] Not Null Default GetDate(),
    [ModifierId] [VarChar](48) Not Null Constraint FK_ProductCategory_ModifierId Foreign Key References [dbo].[AspNetUsers]([Id]),
    [ModifierIp] [VarChar](48) Not Null,
    [CreateDate] [DateTime] Not Null Default GetDate(),
    [CreatorId] [VarChar](48) Not Null Constraint FK_ProductCategory_CreatorId Foreign Key References [dbo].[AspNetUsers]([Id]),
    [CreatorIp] [VarChar](48) Not Null,
  CONSTRAINT [PK_ProductCategories] PRIMARY KEY CLUSTERED ([Id] ASC)
  WITH (PAD_INDEX  = OFF,
    STATISTICS_NORECOMPUTE  = OFF,
    IGNORE_DUP_KEY = OFF,
    ALLOW_ROW_LOCKS  = ON,
    ALLOW_PAGE_LOCKS  = ON)
  ON [PRIMARY]) ON [PRIMARY]
Go
Create Nonclustered Index [idx_ProductCategories_Title] On [dbo].[ProductCategories]([Title] Asc)
Go
Create Nonclustered Index [idx_ProductCategories_ParentCategoryId] On [dbo].[ProductCategories]([ParentCategoryId] Asc)
Go
Create Nonclustered Index [idx_ProductCategories_ModifyDate] On [dbo].[ProductCategories]([ModifyDate] Desc)
Go
