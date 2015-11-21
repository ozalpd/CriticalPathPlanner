SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SizeRatios](
    [Id] [int] Identity(1,1) Not Null,
    [DisplayOrder] [int] Not Null,
    [Caption] [nVarChar](16) Not Null,
    [Rate] [int] Not Null,
    [PurchaseOrderId] [int] Not Null Constraint FK_SizeRatio_PurchaseOrderId Foreign Key References [dbo].[PurchaseOrders]([Id]),
    [ModifyNr] [int] Not Null Default 1,
    [ModifyDate] [DateTime] Not Null Default GetDate(),
    [ModifierId] [VarChar](48) Not Null Constraint FK_SizeRatio_ModifierId Foreign Key References [dbo].[AspNetUsers]([Id]),
    [ModifierIp] [VarChar](48) Not Null,
    [CreateDate] [DateTime] Not Null Default GetDate(),
    [CreatorId] [VarChar](48) Not Null Constraint FK_SizeRatio_CreatorId Foreign Key References [dbo].[AspNetUsers]([Id]),
    [CreatorIp] [VarChar](48) Not Null,
  CONSTRAINT [PK_SizeRatios] PRIMARY KEY CLUSTERED ([Id] ASC)
  WITH (PAD_INDEX  = OFF,
    STATISTICS_NORECOMPUTE  = OFF,
    IGNORE_DUP_KEY = OFF,
    ALLOW_ROW_LOCKS  = ON,
    ALLOW_PAGE_LOCKS  = ON)
  ON [PRIMARY]) ON [PRIMARY]
Go
Create Nonclustered Index [idx_SizeRatios_DisplayOrder] On [dbo].[SizeRatios]([DisplayOrder] Asc)
Go
Create Nonclustered Index [idx_SizeRatios_PurchaseOrderId] On [dbo].[SizeRatios]([PurchaseOrderId] Asc)
Go
Create Nonclustered Index [idx_SizeRatios_ModifyDate] On [dbo].[SizeRatios]([ModifyDate] Desc)
Go
