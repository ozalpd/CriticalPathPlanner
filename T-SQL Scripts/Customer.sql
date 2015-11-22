SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
    [Id] [int] Not Null Constraint FK_Customer_Id Foreign Key References [dbo].[Companies]([Id]),
    [CustomerCode] [nVarChar](128) Not Null,
    [DiscountRate] [decimal](18, 4) Not Null Default 0,
  CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED ([Id] ASC)
  WITH (PAD_INDEX  = OFF,
    STATISTICS_NORECOMPUTE  = OFF,
    IGNORE_DUP_KEY = OFF,
    ALLOW_ROW_LOCKS  = ON,
    ALLOW_PAGE_LOCKS  = ON)
  ON [PRIMARY]) ON [PRIMARY]
Go
