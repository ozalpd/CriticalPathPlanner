SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FreightTerms](
    [Id] [int] Identity(1,1) Not Null,
    [IncotermCode] [nVarChar](16) Not Null,
    [Description] [nVarChar](256) Null,
    [IsPublished] [bit] Not Null,
  CONSTRAINT [PK_FreightTerms] PRIMARY KEY CLUSTERED ([Id] ASC)
  WITH (PAD_INDEX  = OFF,
    STATISTICS_NORECOMPUTE  = OFF,
    IGNORE_DUP_KEY = OFF,
    ALLOW_ROW_LOCKS  = ON,
    ALLOW_PAGE_LOCKS  = ON)
  ON [PRIMARY]) ON [PRIMARY]
Go
