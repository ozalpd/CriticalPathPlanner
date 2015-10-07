SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PuchaseOrders](
    [Id] [int] Identity(1,1) Not Null,
    [Title] [nVarChar](128) Not Null,
    [CustomerId] [int] Not Null Constraint FK_PuchaseOrder_CustomerId Foreign Key References [dbo].[Customers]([Id]),
    [OrderDate] [DateTime] Not Null,
    [DueDate] [DateTime] Null,
    [Code] [nVarChar](48) Null,
    [Description] [nVarChar](256) Null,
    [Notes] [nVarChar](255) Null,
    [ModifyNr] [int] Not Null Default 1,
    [ModifyDate] [DateTime] Not Null Default GetDate(),
    [ModifierId] [VarChar](48) Not Null,
    [ModifierIp] [VarChar](48) Not Null,
    [CreateDate] [DateTime] Not Null Default GetDate(),
    [CreatorId] [VarChar](48) Not Null,
    [CreatorIp] [VarChar](48) Not Null,
    [IsApproved] [bit] Not Null,
    [ApproveDate] [DateTime] Null,
    [ApprovedUserId] [VarChar](48) Null,
    [ApprovedUserIp] [VarChar](48) Null,
  CONSTRAINT [PK_PuchaseOrders] PRIMARY KEY CLUSTERED ([Id] ASC)
  WITH (PAD_INDEX  = OFF,
    STATISTICS_NORECOMPUTE  = OFF,
    IGNORE_DUP_KEY = OFF,
    ALLOW_ROW_LOCKS  = ON,
    ALLOW_PAGE_LOCKS  = ON)
  ON [PRIMARY]) ON [PRIMARY]
Go
Create Nonclustered Index [idx_PuchaseOrders_CustomerId] On [dbo].[PuchaseOrders]([CustomerId] Asc)
Go
Create Nonclustered Index [idx_PuchaseOrders_OrderDate] On [dbo].[PuchaseOrders]([OrderDate] Asc)
Go
Create Nonclustered Index [idx_PuchaseOrders_DueDate] On [dbo].[PuchaseOrders]([DueDate] Asc)
Go
Create Nonclustered Index [idx_PuchaseOrders_ModifyDate] On [dbo].[PuchaseOrders]([ModifyDate] Desc)
Go
Create Nonclustered Index [idx_PuchaseOrders_IsApproved] On [dbo].[PuchaseOrders]([IsApproved] Asc)
Go
