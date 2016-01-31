SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeePositions](
    [Id] [int] Identity(1,1) Not Null,
    [Position] [nVarChar](64) Not Null,
    [AppDefault] [bit] Not Null,
    [CreateDate] [DateTime] Not Null Default GetDate(),
    [CreatorId] [VarChar](48) Not Null Constraint FK_EmployeePosition_CreatorId Foreign Key References [dbo].[AspNetUsers]([Id]),
    [CreatorIp] [VarChar](48) Not Null,
  CONSTRAINT [PK_EmployeePositions] PRIMARY KEY CLUSTERED ([Id] ASC)
  WITH (PAD_INDEX  = OFF,
    STATISTICS_NORECOMPUTE  = OFF,
    IGNORE_DUP_KEY = OFF,
    ALLOW_ROW_LOCKS  = ON,
    ALLOW_PAGE_LOCKS  = ON)
  ON [PRIMARY]) ON [PRIMARY]
Go
