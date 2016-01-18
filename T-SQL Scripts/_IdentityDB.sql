Print 'Creating DB CriticalPathIdentity...'
Go
/*
IF  EXISTS (SELECT name FROM sys.databases WHERE name = N'CriticalPathIdentity')
    DROP DATABASE [CriticalPathIdentity]
Go
Create Database [CriticalPathIdentity]
Go

USE [CriticalPathIdentity]
GO
*/
/****** Object:  Table [dbo].[AspNetRoles] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [VarChar](48) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED ([Id] ASC)WITH
    (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF,
	 ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]

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
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED (
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF,
	 ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]

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
	[PhoneNumber] [nvarchar](256) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED ([Id] ASC)WITH
    (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF,
	 ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES
        (N'6bd5826a-3cc3-430c-a2be-714c5a95acd0', N'admin'),
        (N'f9ab6164-0a5e-40c6-94ba-1e7a33022886', N'clerk'),
        (N'0cf96bab-6350-442a-bcb2-00895ff5e6da', N'observer'),
        (N'7a4780a3-e438-4ce8-8682-c1a94cf6ec73', N'supervisor'),
        (N'5a2530b0-0a5e-2fa5-a1b2-2e7b42412672', N'supplier')
Go
INSERT [dbo].[AspNetUsers] ([Id], [FirstName], [LastName], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES
        (N'2fa1203a-253b-468b-832a-2b962eb6e485', N'Admin', N'Doe', N'admin@mail.xy', 0, N'AABiePOc3aAXtX2VC3XbPHSangmV1PzTekq8RhNsmMY5iGQi3Q5/GuQfm6ijmIoeaQ==', N'48870c62-c5df-47d7-8c16-85f0047b508e', NULL, 0, 0, NULL, 1, 0, N'admincp'),
        (N'597f0e8d-383d-4def-9529-b47dedf881e9', N'Sezgin', N'Orhan', N'sezgin@wec.gen.tr', 0, N'ADFkyN6X7XN/uW4wUS9/j2lPGaHR4UUiuVbbfbfZhupdkEIZhMtsdN0DGkwkRj67dA==', N'cd21f0f0-053b-4403-9315-7584814cf641', NULL, 0, 0, NULL, 1, 0, N'sezgin.orhan'),
        (N'5fdf0f15-cc3e-44e3-9b88-68082fbc84a2', N'Neşe', N'Hiçyılmaz', N'nese@wec.gen.tr', 0, N'AOAR6UsorSHSHfDHlLVHZN9mlhwzyknL7yIGhEblRsUW9/0rePvnlNcY7E0ZA4d+tQ==', N'43e5001b-13a5-4d63-8fc3-85db129ea619', NULL, 0, 0, NULL, 1, 0, N'nese.hicyilmaz'),
        (N'72d24c7d-8c93-4fb0-a4fc-9f8045338fb0', N'Özer', N'ALTUNAL', N'ozer.altunal@eramod.com', 0, N'AFW81aRRwHiYmHPWf5R+s3jwS9TcR0UkgymFUmKL8SFskTVxi/MfB/Hhzd7EwiBNlg==', N'335876e2-fb72-4fd7-b8b6-e0f70429cf85', NULL, 0, 0, NULL, 1, 0, N'ozer.altunal'),
        (N'74cd776e-c098-4f22-8991-c3c2b788862b', N'Özalp', N'Döndüren', N'ozalp@donduren.com', 0, N'AOfbuldqtbuWf/xpxPrnP3Q3ZiErU4u/VxZoXvAkeoaGywmE87rM8Gq+LdNTf97MKA==', N'8a144ac4-9eaf-4cfa-b8ea-46b34930ce67', NULL, 0, 0, NULL, 1, 0, N'ozalp'),
        (N'c915d504-7778-42a8-9b84-a58a3bc7fa5f', N'Göksel', N'Akgün', N'goksel@wec.gen.tr', 0, N'ANIlw9UsCxumdMNUMsAcLJz/6CxUDcC1r44WDSWgSPIVLtl2JTaEKCIZig4agIXmXA==', N'b7376e15-e303-4523-b80d-6c081175d974', NULL, 0, 0, NULL, 1, 0, N'goksel.akgun'),
        (N'd7e14acf-677f-42c2-9880-d435bd9bc00e', N'Alkan', N'Arslan', N'alkan@wec.gen.tr', 0, N'AJykf87wKx7hKFhLhGfOayahVjUgV0jU3A2XePwm0Qt13nFo7XLLOJ7JyNmKjM9nlg==', N'1992db02-6251-4b32-b655-41a27d5b9b0e', NULL, 0, 0, NULL, 1, 0, N'alkan.arslan'),
        (N'db4a0764-bd48-4490-ab3a-7de4bf2d7a1a', N'Aslı', N'Kırık', N'asli@wec.gen.tr', 0, N'AAPB8uCz0F/sAJTrABWtGMqzmDUVXy7r/Q/xWreNIqnEt1EQa+LQBFcFLLy044VEnA==', N'1acc3c65-f894-49fc-bfb3-4e85ae0aad46', NULL, 0, 0, NULL, 1, 0, N'asli.kirik'),
        (N'f2038f98-342a-4d03-b314-1528dfd40284', N'Hatice', N'Akgün', N'hatice@wec.gen.tr', 0, N'ADbioOHmYInEso0o0GPK1oOD2uKH4mmA4KNpLGC6QJHCsf6ZeGnjnPsXc/Lb5DuQWA==', N'50b915d1-9ecb-43bc-ab18-68f7872e98ba', NULL, 0, 0, NULL, 1, 0, N'hatice.akgun'),
        (N'fa363a7c-8d0a-48ad-80bf-756c33d89139', N'Enes', N'Döner', N'enes@wec.gen.tr', 0, N'ADNPU3KN9gXUsVHoHG0GqSepNL+e2zttrvM5MyBytEUD7Y/oO/qyzlJUZaUBMZQIUA==', N'0199c94e-95ad-4c20-bdfe-64128cef5658', NULL, 0, 0, NULL, 1, 0, N'enes.doner')
Go
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES
        (N'2fa1203a-253b-468b-832a-2b962eb6e485', N'6bd5826a-3cc3-430c-a2be-714c5a95acd0'),
        (N'597f0e8d-383d-4def-9529-b47dedf881e9', N'f9ab6164-0a5e-40c6-94ba-1e7a33022886'),
        (N'5fdf0f15-cc3e-44e3-9b88-68082fbc84a2', N'f9ab6164-0a5e-40c6-94ba-1e7a33022886'),
        (N'72d24c7d-8c93-4fb0-a4fc-9f8045338fb0', N'f9ab6164-0a5e-40c6-94ba-1e7a33022886'),
        (N'74cd776e-c098-4f22-8991-c3c2b788862b', N'6bd5826a-3cc3-430c-a2be-714c5a95acd0'),
        (N'c915d504-7778-42a8-9b84-a58a3bc7fa5f', N'7a4780a3-e438-4ce8-8682-c1a94cf6ec73'),
        (N'd7e14acf-677f-42c2-9880-d435bd9bc00e', N'f9ab6164-0a5e-40c6-94ba-1e7a33022886'),
        (N'db4a0764-bd48-4490-ab3a-7de4bf2d7a1a', N'f9ab6164-0a5e-40c6-94ba-1e7a33022886'),
        (N'f2038f98-342a-4d03-b314-1528dfd40284', N'6bd5826a-3cc3-430c-a2be-714c5a95acd0'),
        (N'f2038f98-342a-4d03-b314-1528dfd40284', N'7a4780a3-e438-4ce8-8682-c1a94cf6ec73'),
        (N'fa363a7c-8d0a-48ad-80bf-756c33d89139', N'f9ab6164-0a5e-40c6-94ba-1e7a33022886')
Go
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
