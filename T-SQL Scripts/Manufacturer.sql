SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Manufacturers](
    [Id] [int] Not Null Constraint FK_Manufacturer_Id Foreign Key References [dbo].[Companies]([Id]),
    [ManufacturerCode] [nVarChar](64) Not Null,
    [SupplierId] [int] Not Null Constraint FK_Manufacturer_SupplierId Foreign Key References [dbo].[Suppliers]([Id]),
  CONSTRAINT [PK_Manufacturers] PRIMARY KEY CLUSTERED ([Id] ASC)
  WITH (PAD_INDEX  = OFF,
    STATISTICS_NORECOMPUTE  = OFF,
    IGNORE_DUP_KEY = OFF,
    ALLOW_ROW_LOCKS  = ON,
    ALLOW_PAGE_LOCKS  = ON)
  ON [PRIMARY]) ON [PRIMARY]
Go
