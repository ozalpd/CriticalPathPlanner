SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
    [Id] [int] Identity(1,1) Not Null,
    [AspNetUserId] [VarChar](48) Not Null Constraint FK_Employee_AspNetUserId Foreign Key References [dbo].[AspNetUsers]([Id]),
    [IsActive] [bit] Not Null,
    [InactivateDate] [DateTime] Null,
    [ModifyNr] [int] Not Null Default 1,
    [ModifyDate] [DateTime] Not Null Default GetDate(),
    [ModifierId] [VarChar](48) Not Null Constraint FK_Employee_ModifierId Foreign Key References [dbo].[AspNetUsers]([Id]),
    [ModifierIp] [VarChar](48) Not Null,
    [CreateDate] [DateTime] Not Null Default GetDate(),
    [CreatorId] [VarChar](48) Not Null Constraint FK_Employee_CreatorId Foreign Key References [dbo].[AspNetUsers]([Id]),
    [CreatorIp] [VarChar](48) Not Null,
  CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED ([Id] ASC)
  WITH (PAD_INDEX  = OFF,
    STATISTICS_NORECOMPUTE  = OFF,
    IGNORE_DUP_KEY = OFF,
    ALLOW_ROW_LOCKS  = ON,
    ALLOW_PAGE_LOCKS  = ON)
  ON [PRIMARY]) ON [PRIMARY]
Go
Create Nonclustered Index [idx_Employees_ModifyDate] On [dbo].[Employees]([ModifyDate] Desc)
Go
