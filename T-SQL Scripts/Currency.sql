SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Currencies](
    [Id] [int] Identity(1,1) Not Null,
    [CurrencyName] [nVarChar](50) Not Null,
    [CurrencyCode] [nVarChar](5) Not Null,
    [CurrencySymbol] [nVarChar](max) Not Null,
    [IsActive] [bit] Not Null Default 1,
  CONSTRAINT [PK_Currencies] PRIMARY KEY CLUSTERED ([Id] ASC)
  WITH (PAD_INDEX  = OFF,
    STATISTICS_NORECOMPUTE  = OFF,
    IGNORE_DUP_KEY = OFF,
    ALLOW_ROW_LOCKS  = ON,
    ALLOW_PAGE_LOCKS  = ON)
  ON [PRIMARY]) ON [PRIMARY]
Go
