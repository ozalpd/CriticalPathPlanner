SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Licensors](
    [Id] [int] Not Null Constraint FK_Licensor_Id Foreign Key References [dbo].[Companies]([Id]),
    [LicensorCode] [nVarChar](64) Not Null,
  CONSTRAINT [PK_Licensors] PRIMARY KEY CLUSTERED ([Id] ASC)
  WITH (PAD_INDEX  = OFF,
    STATISTICS_NORECOMPUTE  = OFF,
    IGNORE_DUP_KEY = OFF,
    ALLOW_ROW_LOCKS  = ON,
    ALLOW_PAGE_LOCKS  = ON)
  ON [PRIMARY]) ON [PRIMARY]
Go
