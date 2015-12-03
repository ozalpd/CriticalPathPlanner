SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Countries](
    [Id] [int] Identity(1,1) Not Null,
    [CountryName] [nVarChar](100) Not Null,
    [TwoLetterIsoCode] [nVarChar](2) Null,
    [ThreeLetterIsoCode] [nVarChar](3) Null,
    [NumericIsoCode] [int] Not Null,
    [DisplayOrder] [int] Not Null,
    [IsPublished] [bit] Not Null,
  CONSTRAINT [PK_Countries] PRIMARY KEY CLUSTERED ([Id] ASC)
  WITH (PAD_INDEX  = OFF,
    STATISTICS_NORECOMPUTE  = OFF,
    IGNORE_DUP_KEY = OFF,
    ALLOW_ROW_LOCKS  = ON,
    ALLOW_PAGE_LOCKS  = ON)
  ON [PRIMARY]) ON [PRIMARY]
Go
