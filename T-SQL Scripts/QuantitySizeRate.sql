SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuantitySizeRates](
    [Id] [int] Identity(1,1) Not Null,
    [DisplayOrder] [int] Not Null,
    [PurchaseOrderId] [int] Not Null Constraint FK_QuantitySizeRate_PurchaseOrderId Foreign Key References [dbo].[PurchaseOrders]([Id]),
    [SizeCaptionId] [int] Not Null Constraint FK_QuantitySizeRate_SizeCaptionId Foreign Key References [dbo].[SizeCaptions]([Id]),
    [Rate] [int] Not Null,
    [ModifyNr] [int] Not Null Default 1,
    [ModifyDate] [DateTime] Not Null Default GetDate(),
    [ModifierId] [VarChar](48) Not Null,
    [ModifierIp] [VarChar](48) Not Null,
    [CreateDate] [DateTime] Not Null Default GetDate(),
    [CreatorId] [VarChar](48) Not Null,
    [CreatorIp] [VarChar](48) Not Null,
  CONSTRAINT [PK_QuantitySizeRates] PRIMARY KEY CLUSTERED ([Id] ASC)
  WITH (PAD_INDEX  = OFF,
    STATISTICS_NORECOMPUTE  = OFF,
    IGNORE_DUP_KEY = OFF,
    ALLOW_ROW_LOCKS  = ON,
    ALLOW_PAGE_LOCKS  = ON)
  ON [PRIMARY]) ON [PRIMARY]
Go
Create Nonclustered Index [idx_QuantitySizeRates_DisplayOrder] On [dbo].[QuantitySizeRates]([DisplayOrder] Asc)
Go
Create Nonclustered Index [idx_QuantitySizeRates_PurchaseOrderId] On [dbo].[QuantitySizeRates]([PurchaseOrderId] Asc)
Go
Create Nonclustered Index [idx_QuantitySizeRates_ModifyDate] On [dbo].[QuantitySizeRates]([ModifyDate] Desc)
Go
