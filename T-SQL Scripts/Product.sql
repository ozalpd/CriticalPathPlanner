SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
    [Id] [int] Identity(1,1) Not Null,
    [ProductCode] [nVarChar](64) Not Null,
    [Description] [nVarChar](128) Not Null,
    [ImageUrl] [nVarChar](256) Null,
    [CategoryId] [int] Not Null Constraint FK_Product_CategoryId Foreign Key References [dbo].[ProductCategories]([Id]),
    [Discontinued] [bit] Not Null,
    [DiscontinueDate] [DateTime] Null,
    [DiscontinueNotes] [nVarChar](max) Null,
    [DiscontinuedUserIp] [VarChar](48) Null,
    [DiscontinuedUserId] [VarChar](48) Null Constraint FK_Product_DiscontinuedUserId Foreign Key References [dbo].[AspNetUsers]([Id]),
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
Create Nonclustered Index [idx_Products_CategoryId] On [dbo].[Products]([CategoryId] Asc)
Go
Create Nonclustered Index [idx_Products_ModifyDate] On [dbo].[Products]([ModifyDate] Desc)
Go
