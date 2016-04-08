SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PurchaseOrders](
    [Id] [int] Identity(1,1) Not Null,
    [IsApproved] [bit] Not Null,
    [ApproveDate] [DateTime] Null,
    [PoNr] [nVarChar](64) Not Null,
    [RefCode] [nVarChar](32) Null,
    [CustomerRefNr] [nVarChar](32) Null,
    [OrderDate] [DateTime] Not Null,
    [DueDate] [DateTime] Null,
    [IsRepeat] [bit] Not Null,
    [ParentPoId] [int] Null Constraint FK_PurchaseOrder_ParentPoId Foreign Key References [dbo].[PurchaseOrders]([Id]),
    [DesignerId] [int] Null Constraint FK_PurchaseOrder_DesignerId Foreign Key References [dbo].[Employees]([Id]),
    [Merchandiser1Id] [int] Null Constraint FK_PurchaseOrder_Merchandiser1Id Foreign Key References [dbo].[Employees]([Id]),
    [Merchandiser2Id] [int] Null Constraint FK_PurchaseOrder_Merchandiser2Id Foreign Key References [dbo].[Employees]([Id]),
    [ProductId] [int] Not Null Constraint FK_PurchaseOrder_ProductId Foreign Key References [dbo].[Products]([Id]),
    [Description] [nVarChar](256) Null,
    [FabricComposition] [nVarChar](256) Null,
    [Colour] [nVarChar](256) Null,
    [Print] [nVarChar](256) Null,
    [Labelling] [nVarChar](128) Null,
    [WovenLabel] [nVarChar](128) Null,
    [ShipmentHangingFolded] [int] Not Null Default 0,
    [HangerSticker] [bit] Not Null,
    [WashingInstructions] [nVarChar](128) Null,
    [Quantity] [int] Not Null,
    [DiscountRate] [decimal](18, 4) Not Null Default 0,
    [UnitPrice] [decimal](18, 4) Not Null,
    [SellingCurrencyId] [int] Not Null Constraint FK_PurchaseOrder_SellingCurrencyId Foreign Key References [dbo].[Currencies]([Id]),
    [UnitPrice2] [decimal](18, 4) Null,
    [SellingCurrency2Id] [int] Null Constraint FK_PurchaseOrder_SellingCurrency2Id Foreign Key References [dbo].[Currencies]([Id]),
    [LicensorPrice] [decimal](18, 4) Null,
    [LicensorCurrencyId] [int] Null Constraint FK_PurchaseOrder_LicensorCurrencyId Foreign Key References [dbo].[Currencies]([Id]),
    [BuyingPrice] [decimal](18, 4) Null,
    [BuyingCurrencyId] [int] Null Constraint FK_PurchaseOrder_BuyingCurrencyId Foreign Key References [dbo].[Currencies]([Id]),
    [BuyingPrice2] [decimal](18, 4) Null,
    [BuyingCurrency2Id] [int] Null Constraint FK_PurchaseOrder_BuyingCurrency2Id Foreign Key References [dbo].[Currencies]([Id]),
    [RoyaltyFee] [decimal](18, 4) Null Default 0,
    [RoyaltyCurrencyId] [int] Null Constraint FK_PurchaseOrder_RoyaltyCurrencyId Foreign Key References [dbo].[Currencies]([Id]),
    [RetailPrice] [decimal](18, 4) Null,
    [RetailCurrencyId] [int] Null Constraint FK_PurchaseOrder_RetailCurrencyId Foreign Key References [dbo].[Currencies]([Id]),
    [CustomerId] [int] Not Null Constraint FK_PurchaseOrder_CustomerId Foreign Key References [dbo].[Customers]([Id]),
    [CustomerDepartmentId] [int] Null Constraint FK_PurchaseOrder_CustomerDepartmentId Foreign Key References [dbo].[CustomerDepartments]([Id]),
    [CustomerPoNr] [nVarChar](64) Null,
    [FreightTermId] [int] Not Null Constraint FK_PurchaseOrder_FreightTermId Foreign Key References [dbo].[FreightTerms]([Id]),
    [LicensorId] [int] Null Constraint FK_PurchaseOrder_LicensorId Foreign Key References [dbo].[Licensors]([Id]),
    [SupplierId] [int] Null Constraint FK_PurchaseOrder_SupplierId Foreign Key References [dbo].[Suppliers]([Id]),
    [SupplierDueDate] [DateTime] Null,
    [SizingStandardId] [int] Not Null Constraint FK_PurchaseOrder_SizingStandardId Foreign Key References [dbo].[SizingStandards]([Id]),
    [SizeRatioDivisor] [int] Not Null,
    [Notes] [nVarChar](max) Null,
    [Cancelled] [bit] Not Null Default 0,
    [CancelDate] [DateTime] Null,
    [CancellationReason] [nVarChar](max) Null,
    [CancelledUserIp] [VarChar](48) Null,
    [CancelledUserId] [VarChar](48) Null Constraint FK_PurchaseOrder_CancelledUserId Foreign Key References [dbo].[AspNetUsers]([Id]),
    [Closed] [bit] Not Null,
    [ClosedUserIp] [nVarChar](48) Null,
    [ClosedUserId] [nVarChar](48) Null,
    [InitialComments] [nVarChar](256) Null,
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
Create Nonclustered Index [idx_PurchaseOrders_PoNr] On [dbo].[PurchaseOrders]([PoNr] Asc)
Go
Create Nonclustered Index [idx_PurchaseOrders_OrderDate] On [dbo].[PurchaseOrders]([OrderDate] Asc)
Go
Create Nonclustered Index [idx_PurchaseOrders_DueDate] On [dbo].[PurchaseOrders]([DueDate] Asc)
Go
Create Nonclustered Index [idx_PurchaseOrders_ParentPoId] On [dbo].[PurchaseOrders]([ParentPoId] Asc)
Go
Create Nonclustered Index [idx_PurchaseOrders_ProductId] On [dbo].[PurchaseOrders]([ProductId] Asc)
Go
Create Nonclustered Index [idx_PurchaseOrders_Description] On [dbo].[PurchaseOrders]([Description] Asc)
Go
Create Nonclustered Index [idx_PurchaseOrders_CustomerId] On [dbo].[PurchaseOrders]([CustomerId] Asc)
Go
Create Nonclustered Index [idx_PurchaseOrders_CustomerPoNr] On [dbo].[PurchaseOrders]([CustomerPoNr] Asc)
Go
Create Nonclustered Index [idx_PurchaseOrders_Cancelled] On [dbo].[PurchaseOrders]([Cancelled] Asc)
Go
Create Nonclustered Index [idx_PurchaseOrders_ModifyDate] On [dbo].[PurchaseOrders]([ModifyDate] Desc)
Go
