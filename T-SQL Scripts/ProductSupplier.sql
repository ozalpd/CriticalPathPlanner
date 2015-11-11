SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductsSuppliers](
    [ProductId] [int] Not Null Constraint FK_Suppliers_ProductId Foreign Key References [dbo].[Products]([Id]),
    [SupplierId] [int] Not Null Constraint FK_Products_SupplierId Foreign Key References [dbo].[Suppliers]([Id]),
  CONSTRAINT [PK_ProductsSuppliers] PRIMARY KEY CLUSTERED ([ProductId] ASC, [SupplierId] ASC)
  WITH (PAD_INDEX  = OFF,
    STATISTICS_NORECOMPUTE  = OFF,
    IGNORE_DUP_KEY = OFF,
    ALLOW_ROW_LOCKS  = ON,
    ALLOW_PAGE_LOCKS  = ON)
  ON [PRIMARY]) ON [PRIMARY]
Go
