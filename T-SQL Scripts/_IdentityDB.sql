Print 'Creating DB CriticalPathIdentity...'
Go
IF  EXISTS (SELECT name FROM sys.databases WHERE name = N'CriticalPathIdentity')
    DROP DATABASE [CriticalPathIdentity]
Go
Create Database [CriticalPathIdentity]
Go

USE [CriticalPathIdentity]
GO
/****** Object:  Table [dbo].[AspNetRoles] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [VarChar](48) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserClaims] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [VarChar](48) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserLogins] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [VarChar](48) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserRoles] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [VarChar](48) NOT NULL,
	[RoleId] [VarChar](48) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUsers] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [VarChar](48) NOT NULL,
	[FirstName] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'6bd5826a-3cc3-430c-a2be-714c5a95acd0', N'admin')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'f9ab6164-0a5e-40c6-94ba-1e7a33022886', N'clerk')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'0cf96bab-6350-442a-bcb2-00895ff5e6da', N'observer')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'7a4780a3-e438-4ce8-8682-c1a94cf6ec73', N'supervisor')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'571bc504-3048-4f5c-840a-18f60d0f4394', N'0cf96bab-6350-442a-bcb2-00895ff5e6da')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'74cd776e-c098-4f22-8991-c3c2b788862b', N'6bd5826a-3cc3-430c-a2be-714c5a95acd0')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'568b10ed-ae60-45db-ab11-20e8b39a0cf5', N'7a4780a3-e438-4ce8-8682-c1a94cf6ec73')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'cd3f849d-8092-404f-9cc1-77da484196aa', N'f9ab6164-0a5e-40c6-94ba-1e7a33022886')
INSERT [dbo].[AspNetUsers] ([Id], [FirstName], [LastName], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'2fa1203a-253b-468b-832a-2b962eb6e485', N'User', N'Doe', N'user@mail.xy', 0, N'AABiePOc3aAXtX2VC3XbPHSangmV1PzTekq8RhNsmMY5iGQi3Q5/GuQfm6ijmIoeaQ==', N'48870c62-c5df-47d7-8c16-85f0047b508e', NULL, 0, 0, NULL, 1, 0, N'user1')
INSERT [dbo].[AspNetUsers] ([Id], [FirstName], [LastName], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'568b10ed-ae60-45db-ab11-20e8b39a0cf5', N'Super', N'Doe', N'super1@mail.xy', 0, N'ABfAt8ispEiwKN+NJ7er5r/mw4xn0G1yLpIypXlCocHSJYt23c7ep+5C1/TQr8q9og==', N'715e642d-880a-40db-92e9-f278503f71d2', NULL, 0, 0, NULL, 1, 0, N'super1@mail.xy')
INSERT [dbo].[AspNetUsers] ([Id], [FirstName], [LastName], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'571bc504-3048-4f5c-840a-18f60d0f4394', N'Observer', N'Doe', N'observer@mail.xy', 0, N'AIfaspCxcbz8lgLpYQO2pxkE7Tw0SK+QYhE3WwZmuqg43XX44oZ8t0OkC5M1qYLAQA==', N'a2aaa87c-9bf0-43d4-b172-bae6512f9c05', NULL, 0, 0, NULL, 1, 0, N'observer@mail.xy')
INSERT [dbo].[AspNetUsers] ([Id], [FirstName], [LastName], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'74cd776e-c098-4f22-8991-c3c2b788862b', N'Özalp', N'Döndüren', N'ozalp@donduren.com', 0, N'AOfbuldqtbuWf/xpxPrnP3Q3ZiErU4u/VxZoXvAkeoaGywmE87rM8Gq+LdNTf97MKA==', N'8a144ac4-9eaf-4cfa-b8ea-46b34930ce67', NULL, 0, 0, NULL, 1, 0, N'ozalp@donduren.com')
INSERT [dbo].[AspNetUsers] ([Id], [FirstName], [LastName], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'cd3f849d-8092-404f-9cc1-77da484196aa', N'Clerk', N'Doe', N'clerk@mail.xy', 0, N'AKOXiL88L2FYx7nYswAJX2WCCFGqUX4tUh8DZCw+xWvRX1cnSgPCGaI1EzP5bORZHw==', N'881fa8a1-f026-4d67-94d8-109a1aafca09', NULL, 0, 0, NULL, 1, 0, N'clerk@mail.xy')
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO
Print 'CriticalPathIdentity Created!'
