SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
    [Id] [int] Identity(1,1) Not Null,
    [Title] [nVarChar](128) Not Null,
    [Code] [nVarChar](48) Null,
    [Description] [nVarChar](256) Null,
    [ImageUrl] [nVarChar](256) Null,
    [CategoryId] [int] Not Null Constraint FK_Product_CategoryId Foreign Key References [dbo].[ProductCategories]([Id]),
    [SizingStandardId] [int] Not Null Constraint FK_Product_SizingStandardId Foreign Key References [dbo].[SizingStandards]([Id]),
    [IsActive] [bit] Not Null,
    [InactivateDate] [DateTime] Null,
    [InactivateNotes] [nVarChar](max) Null,
    [InactivateUserId] [VarChar](48) Null Constraint FK_Product_InactivateUserId Foreign Key References [dbo].[AspNetUsers]([Id]),
    [ModifyNr] [int] Not Null Default 1,
    [ModifyDate] [DateTime] Not Null Default GetDate(),
    [ModifierId] [VarChar](48) Not Null Constraint FK_Product_ModifierId Foreign Key References [dbo].[AspNetUsers]([Id]),
    [ModifierIp] [VarChar](48) Not Null,
    [CreateDate] [DateTime] Not Null Default GetDate(),
    [CreatorId] [VarChar](48) Not Null Constraint FK_Product_CreatorId Foreign Key References [dbo].[AspNetUsers]([Id]),
    [CreatorIp] [VarChar](48) Not Null,
  CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED ([Id] ASC)
  WITH (PAD_INDEX  = OFF,
    STATISTICS_NORECOMPUTE  = OFF,
    IGNORE_DUP_KEY = OFF,
    ALLOW_ROW_LOCKS  = ON,
    ALLOW_PAGE_LOCKS  = ON)
  ON [PRIMARY]) ON [PRIMARY]
Go
Create Nonclustered Index [idx_Products_Title] On [dbo].[Products]([Title] Asc)
Go
Create Nonclustered Index [idx_Products_CategoryId] On [dbo].[Products]([CategoryId] Asc)
Go
Create Nonclustered Index [idx_Products_SizingStandardId] On [dbo].[Products]([SizingStandardId] Asc)
Go
Create Nonclustered Index [idx_Products_IsActive] On [dbo].[Products]([IsActive] Asc)
Go
Create Nonclustered Index [idx_Products_ModifyDate] On [dbo].[Products]([ModifyDate] Desc)
Go
