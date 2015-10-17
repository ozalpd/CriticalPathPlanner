SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderItems](
    [Id] [int] Identity(1,1) Not Null,
    [PuchaseOrderId] [int] Not Null Constraint FK_OrderItem_PuchaseOrderId Foreign Key References [dbo].[PuchaseOrders]([Id]),
    [ProductId] [int] Not Null Constraint FK_OrderItem_ProductId Foreign Key References [dbo].[Products]([Id]),
    [Quantity] [int] Not Null,
    [Notes] [nVarChar](255) Null,
    [ModifyNr] [int] Not Null Default 1,
    [ModifyDate] [DateTime] Not Null Default GetDate(),
    [ModifierId] [VarChar](48) Not Null,
    [ModifierIp] [VarChar](48) Not Null,
    [CreateDate] [DateTime] Not Null Default GetDate(),
    [CreatorId] [VarChar](48) Not Null,
    [CreatorIp] [VarChar](48) Not Null,
  CONSTRAINT [PK_OrderItems] PRIMARY KEY CLUSTERED ([Id] ASC)
  WITH (PAD_INDEX  = OFF,
    STATISTICS_NORECOMPUTE  = OFF,
    IGNORE_DUP_KEY = OFF,
    ALLOW_ROW_LOCKS  = ON,
    ALLOW_PAGE_LOCKS  = ON)
  ON [PRIMARY]) ON [PRIMARY]
Go
Create Nonclustered Index [idx_OrderItems_PuchaseOrderId] On [dbo].[OrderItems]([PuchaseOrderId] Asc)
Go
Create Nonclustered Index [idx_OrderItems_ProductId] On [dbo].[OrderItems]([ProductId] Asc)
Go
Create Nonclustered Index [idx_OrderItems_ModifyDate] On [dbo].[OrderItems]([ModifyDate] Desc)
Go
