SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PurchaseOrders](
    [Id] [int] Identity(1,1) Not Null,
    [IsApproved] [bit] Not Null,
    [ApproveDate] [DateTime] Null,
    [IsActive] [bit] Not Null,
    [OrderDate] [DateTime] Not Null,
    [DueDate] [DateTime] Null,
    [CustomerId] [int] Not Null Constraint FK_PurchaseOrder_CustomerId Foreign Key References [dbo].[Customers]([Id]),
    [ProductId] [int] Not Null Constraint FK_PurchaseOrder_ProductId Foreign Key References [dbo].[Products]([Id]),
    [Code] [nVarChar](48) Null,
    [Description] [nVarChar](256) Null,
    [Quantity] [int] Not Null,
    [UnitPrice] [decimal](18, 4) Not Null,
    [SizingStandardId] [int] Not Null Constraint FK_PurchaseOrder_SizingStandardId Foreign Key References [dbo].[SizingStandards]([Id]),
    [SizeRateDivisor] [int] Not Null,
    [Notes] [nVarChar](2048) Null,
    [CancellationDate] [DateTime] Null,
    [CancellationNotes] [nVarChar](max) Null,
    [InactivateUserId] [VarChar](48) Null Constraint FK_PurchaseOrder_InactivateUserId Foreign Key References [dbo].[AspNetUsers]([Id]),
    [ApprovedUserId] [VarChar](48) Null Constraint FK_PurchaseOrder_ApprovedUserId Foreign Key References [dbo].[AspNetUsers]([Id]),
    [ApprovedUserIp] [VarChar](48) Null,
    [ModifyNr] [int] Not Null Default 1,
    [ModifyDate] [DateTime] Not Null Default GetDate(),
    [ModifierId] [VarChar](48) Not Null Constraint FK_PurchaseOrder_ModifierId Foreign Key References [dbo].[AspNetUsers]([Id]),
    [ModifierIp] [VarChar](48) Not Null,
    [CreateDate] [DateTime] Not Null Default GetDate(),
    [CreatorId] [VarChar](48) Not Null Constraint FK_PurchaseOrder_CreatorId Foreign Key References [dbo].[AspNetUsers]([Id]),
    [CreatorIp] [VarChar](48) Not Null,
  CONSTRAINT [PK_PurchaseOrders] PRIMARY KEY CLUSTERED ([Id] ASC)
  WITH (PAD_INDEX  = OFF,
    STATISTICS_NORECOMPUTE  = OFF,
    IGNORE_DUP_KEY = OFF,
    ALLOW_ROW_LOCKS  = ON,
    ALLOW_PAGE_LOCKS  = ON)
  ON [PRIMARY]) ON [PRIMARY]
Go
Create Nonclustered Index [idx_PurchaseOrders_IsApproved] On [dbo].[PurchaseOrders]([IsApproved] Asc)
Go
Create Nonclustered Index [idx_PurchaseOrders_IsActive] On [dbo].[PurchaseOrders]([IsActive] Asc)
Go
Create Nonclustered Index [idx_PurchaseOrders_OrderDate] On [dbo].[PurchaseOrders]([OrderDate] Asc)
Go
Create Nonclustered Index [idx_PurchaseOrders_DueDate] On [dbo].[PurchaseOrders]([DueDate] Asc)
Go
Create Nonclustered Index [idx_PurchaseOrders_CustomerId] On [dbo].[PurchaseOrders]([CustomerId] Asc)
Go
Create Nonclustered Index [idx_PurchaseOrders_ProductId] On [dbo].[PurchaseOrders]([ProductId] Asc)
Go
Create Nonclustered Index [idx_PurchaseOrders_ModifyDate] On [dbo].[PurchaseOrders]([ModifyDate] Desc)
Go
