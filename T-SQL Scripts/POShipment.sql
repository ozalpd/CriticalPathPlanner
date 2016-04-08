SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[POShipments](
    [Id] [int] Identity(1,1) Not Null,
    [ShippingNr] [nVarChar](32) Not Null,
    [ShippingDate] [DateTime] Null,
    [DeliveryNr] [nVarChar](32) Null,
    [DeliveryDate] [DateTime] Null,
    [DestinationNr] [nVarChar](32) Null,
    [RefCode] [nVarChar](32) Null,
    [CustomerRefNr] [nVarChar](32) Null,
    [Quantity] [int] Not Null,
    [IsShipped] [bit] Not Null,
    [IsDelivered] [bit] Not Null,
    [PurchaseOrderId] [int] Not Null Constraint FK_POShipment_PurchaseOrderId Foreign Key References [dbo].[PurchaseOrders]([Id]),
    [FreightTermId] [int] Not Null Constraint FK_POShipment_FreightTermId Foreign Key References [dbo].[FreightTerms]([Id]),
    [ModifyNr] [int] Not Null Default 1,
    [ModifyDate] [DateTime] Not Null Default GetDate(),
    [ModifierId] [VarChar](48) Not Null Constraint FK_POShipment_ModifierId Foreign Key References [dbo].[AspNetUsers]([Id]),
    [ModifierIp] [VarChar](48) Not Null,
    [CreateDate] [DateTime] Not Null Default GetDate(),
    [CreatorId] [VarChar](48) Not Null Constraint FK_POShipment_CreatorId Foreign Key References [dbo].[AspNetUsers]([Id]),
    [CreatorIp] [VarChar](48) Not Null,
  CONSTRAINT [PK_POShipments] PRIMARY KEY CLUSTERED ([Id] ASC)
  WITH (PAD_INDEX  = OFF,
    STATISTICS_NORECOMPUTE  = OFF,
    IGNORE_DUP_KEY = OFF,
    ALLOW_ROW_LOCKS  = ON,
    ALLOW_PAGE_LOCKS  = ON)
  ON [PRIMARY]) ON [PRIMARY]
Go
Create Nonclustered Index [idx_POShipments_ShippingNr] On [dbo].[POShipments]([ShippingNr] Asc)
Go
Create Nonclustered Index [idx_POShipments_ShippingDate] On [dbo].[POShipments]([ShippingDate] Asc)
Go
Create Nonclustered Index [idx_POShipments_DeliveryNr] On [dbo].[POShipments]([DeliveryNr] Asc)
Go
Create Nonclustered Index [idx_POShipments_DeliveryDate] On [dbo].[POShipments]([DeliveryDate] Asc)
Go
Create Nonclustered Index [idx_POShipments_DestinationNr] On [dbo].[POShipments]([DestinationNr] Asc)
Go
Create Nonclustered Index [idx_POShipments_IsShipped] On [dbo].[POShipments]([IsShipped] Asc)
Go
Create Nonclustered Index [idx_POShipments_IsDelivered] On [dbo].[POShipments]([IsDelivered] Asc)
Go
Create Nonclustered Index [idx_POShipments_PurchaseOrderId] On [dbo].[POShipments]([PurchaseOrderId] Asc)
Go
Create Nonclustered Index [idx_POShipments_ModifyDate] On [dbo].[POShipments]([ModifyDate] Desc)
Go
