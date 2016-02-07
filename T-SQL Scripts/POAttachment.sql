SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[POAttachments](
    [Id] [int] Identity(1,1) Not Null,
    [PurchaseOrderId] [int] Not Null Constraint FK_POAttachment_PurchaseOrderId Foreign Key References [dbo].[PurchaseOrders]([Id]),
    [FileUrl] [nVarChar](256) Not Null,
    [FileTitle] [nVarChar](64) Not Null,
    [FileName] [nVarChar](64) Not Null,
    [FileType] [nVarChar](16) Not Null,
    [DisplayOrder] [int] Not Null,
    [ModifyNr] [int] Not Null Default 1,
    [ModifyDate] [DateTime] Not Null Default GetDate(),
    [ModifierId] [VarChar](48) Not Null Constraint FK_POAttachment_ModifierId Foreign Key References [dbo].[AspNetUsers]([Id]),
    [ModifierIp] [VarChar](48) Not Null,
    [CreateDate] [DateTime] Not Null Default GetDate(),
    [CreatorId] [VarChar](48) Not Null Constraint FK_POAttachment_CreatorId Foreign Key References [dbo].[AspNetUsers]([Id]),
    [CreatorIp] [VarChar](48) Not Null,
  CONSTRAINT [PK_POAttachments] PRIMARY KEY CLUSTERED ([Id] ASC)
  WITH (PAD_INDEX  = OFF,
    STATISTICS_NORECOMPUTE  = OFF,
    IGNORE_DUP_KEY = OFF,
    ALLOW_ROW_LOCKS  = ON,
    ALLOW_PAGE_LOCKS  = ON)
  ON [PRIMARY]) ON [PRIMARY]
Go
Create Nonclustered Index [idx_POAttachments_ModifyDate] On [dbo].[POAttachments]([ModifyDate] Desc)
Go
