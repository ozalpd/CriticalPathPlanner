SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[POSizeRatios](
    [Id] [int] Identity(1,1) Not Null,
    [DisplayOrder] [int] Not Null,
    [Caption] [nVarChar](16) Not Null,
    [Rate] [int] Not Null,
    [PurchaseOrderId] [int] Not Null Constraint FK_POSizeRatio_PurchaseOrderId Foreign Key References [dbo].[PurchaseOrders]([Id]),
    [ModifyNr] [int] Not Null Default 1,
    [ModifyDate] [DateTime] Not Null Default GetDate(),
    [ModifierId] [VarChar](48) Not Null Constraint FK_POSizeRatio_ModifierId Foreign Key References [dbo].[AspNetUsers]([Id]),
    [ModifierIp] [VarChar](48) Not Null,
    [CreateDate] [DateTime] Not Null Default GetDate(),
    [CreatorId] [VarChar](48) Not Null Constraint FK_POSizeRatio_CreatorId Foreign Key References [dbo].[AspNetUsers]([Id]),
    [CreatorIp] [VarChar](48) Not Null,
  CONSTRAINT [PK_POSizeRatios] PRIMARY KEY CLUSTERED ([Id] ASC)
  WITH (PAD_INDEX  = OFF,
    STATISTICS_NORECOMPUTE  = OFF,
    IGNORE_DUP_KEY = OFF,
    ALLOW_ROW_LOCKS  = ON,
    ALLOW_PAGE_LOCKS  = ON)
  ON [PRIMARY]) ON [PRIMARY]
Go
Create Nonclustered Index [idx_POSizeRatios_DisplayOrder] On [dbo].[POSizeRatios]([DisplayOrder] Asc)
Go
Create Nonclustered Index [idx_POSizeRatios_PurchaseOrderId] On [dbo].[POSizeRatios]([PurchaseOrderId] Asc)
Go
Create Nonclustered Index [idx_POSizeRatios_ModifyDate] On [dbo].[POSizeRatios]([ModifyDate] Desc)
Go
