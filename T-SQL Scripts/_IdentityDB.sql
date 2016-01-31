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
    (N'070b8a93-77ec-4465-8292-0e154851987a', N'Balu', N'N/A', N'balu@wec.gen.tr', 0, N'AL+XRtljLlEz72csoG6FBtXHQOsZnhluYe2YANNqj2/W0eDS/apQKkOdb7tFYXitbA==', N'3f60cfe4-4329-41b1-9bb3-458aec0d60a7', NULL, 0, 0, NULL, 1, 0, N'balu'),
    (N'1101ecbd-1ea4-45f7-9429-e8bd6248d01e', N'Sheena', N'N/A', N'sheena@wec.gen.tr', 0, N'ALPjA1Zte0NnzztWBp+1VihDWSa4GJQBdk2WOgUcJnW26SnWPCbS0x/Zug5k9pxY6A==', N'2361309d-37d3-4c42-ad9f-d804f3017e79', NULL, 0, 0, NULL, 1, 0, N'sheena'),
    (N'14d50429-f130-4ed1-9944-e3a7305e9c9e', N'Selina', N'N/A', N'selina@wec.gen.tr', 0, N'AJNc+YcV5JWsqKfi3A/i7ite5GlDpKnU2QUSJpd/XyXjHgxupJGIXS3ekWdrZymd4Q==', N'9da27c90-1936-433d-b26c-acdef219a898', NULL, 0, 0, NULL, 1, 0, N'selina'),
    (N'262ffe38-670e-4f8d-8c55-90faeff819c4', N'Shailja', N'N/A', N'shailja@wec.gen.tr', 0, N'ACltS7wQ9zNzHjjcMYzMru+2T2U4kt3oAbY86WcLiHegrs8gJoOFf1zFSfMnYa1omw==', N'803461c7-95b3-4ebd-a5a5-b2d5a1661a98', NULL, 0, 0, NULL, 1, 0, N'shailja'),
    (N'2fa1203a-253b-468b-832a-2b962eb6e485', N'Admin', N'Doe', N'admin@mail.xy', 0, N'AABiePOc3aAXtX2VC3XbPHSangmV1PzTekq8RhNsmMY5iGQi3Q5/GuQfm6ijmIoeaQ==', N'48870c62-c5df-47d7-8c16-85f0047b508e', NULL, 0, 0, NULL, 1, 0, N'admincp'),
    (N'34287b42-f008-4dc2-bc02-1649f3f5b323', N'Rosewana', N'N/A', N'rosewana@wec.gen.tr', 0, N'AHVOcpw7cUCA+NTD2CoLB056zgH0sGfFV2/MtGUZoJZDyN1rYnWAAnDaE8c9/TUZKw==', N'a7aa69ff-7c8b-4314-bfff-ef8b9c3f5c9c', NULL, 0, 0, NULL, 1, 0, N'rosewana'),
    (N'420edb56-0114-49af-9778-14f51918a0bc', N'Julie', N'N/A', N'julie@wec.gen.tr', 0, N'AJzwFB6wd0i/uhqgDaRkKcc7o6QrajdzjjvSebZsAE4L9tcvdWwTrrQilRwKWq9ryQ==', N'258f16ca-6a55-4315-a110-c77c05d25c12', NULL, 0, 0, NULL, 1, 0, N'julie'),
    (N'519e148d-8364-43ad-9be8-3b4491da1ef5', N'Charlotte', N'N/A', N'charlotte@wec.gen.tr', 0, N'ACeJLA79kM4wfUmLrlDyho5ITGjQ9ffE6wKrjo/hkUO5X86T9+T3S7Z+EJ8HZw/vOA==', N'f194c239-1e82-4b54-8bcd-8572f85f1882', NULL, 0, 0, NULL, 1, 0, N'charlotte'),
    (N'597f0e8d-383d-4def-9529-b47dedf881e9', N'Sezgin', N'Orhan', N'sezgin@wec.gen.tr', 0, N'ADFkyN6X7XN/uW4wUS9/j2lPGaHR4UUiuVbbfbfZhupdkEIZhMtsdN0DGkwkRj67dA==', N'cd21f0f0-053b-4403-9315-7584814cf641', NULL, 0, 0, NULL, 1, 0, N'sezgin'),
    (N'5fdf0f15-cc3e-44e3-9b88-68082fbc84a2', N'Neşe', N'Hiçyılmaz', N'nese@wec.gen.tr', 0, N'AOAR6UsorSHSHfDHlLVHZN9mlhwzyknL7yIGhEblRsUW9/0rePvnlNcY7E0ZA4d+tQ==', N'43e5001b-13a5-4d63-8fc3-85db129ea619', NULL, 0, 0, NULL, 1, 0, N'nese'),
    (N'62669683-4a19-4a09-b805-676d2d3c3e70', N'Kate', N'N/A', N'kate@wec.gen.tr', 0, N'AMI6/pTF7giysqEdeVSQyrytEGXBkziQVNTkOAAm3U6KbmjHX3IDoHoklR3r6XaDkw==', N'ce5e1db0-3939-494d-a365-64b7a447539c', NULL, 0, 0, NULL, 1, 0, N'kate'),
    (N'63308a64-d7a2-4b42-b252-c5808f5253d0', N'Ray', N'N/A', N'ray@wec.gen.tr', 0, N'ANkQkELBCWmgb4JNqTExFHwFVm8BFzuGNgLoSs33YJBBbwYTQa/E2YfctpFHFmn0AQ==', N'ee32d864-90bd-4bf6-82e3-e8b0b0adc597', NULL, 0, 0, NULL, 1, 0, N'ray'),
    (N'6f7d060b-5936-4bd2-81dc-2f1dd3906508', N'Sarah', N'N/A', N'sarah@wec.gen.tr', 0, N'AKZId+mvpVXwqNfXW73/bx1KxCpnrakaCLodBPfmhBSEsBTVi0QdxZA7OxI98dS2tQ==', N'77b01def-1206-4470-abf1-9419702cc90e', NULL, 0, 0, NULL, 1, 0, N'sarah'),
    (N'72d24c7d-8c93-4fb0-a4fc-9f8045338fb0', N'Özer', N'ALTUNAL', N'ozer.altunal@eramod.com', 0, N'AFW81aRRwHiYmHPWf5R+s3jwS9TcR0UkgymFUmKL8SFskTVxi/MfB/Hhzd7EwiBNlg==', N'335876e2-fb72-4fd7-b8b6-e0f70429cf85', NULL, 0, 0, NULL, 1, 0, N'ozer'),
    (N'74cd776e-c098-4f22-8991-c3c2b788862b', N'Özalp', N'Döndüren', N'ozalp@donduren.com', 0, N'AOfbuldqtbuWf/xpxPrnP3Q3ZiErU4u/VxZoXvAkeoaGywmE87rM8Gq+LdNTf97MKA==', N'8a144ac4-9eaf-4cfa-b8ea-46b34930ce67', NULL, 0, 0, NULL, 1, 0, N'ozalp'),
    (N'7d88978e-1f32-467e-9c9b-bddbd5476f6d', N'Khushboo', N'N/A', N'khushboo@wec.gen.tr', 0, N'AJDBWs6No4t9WLylS16oAVVS/iY3MClk6cFUneSnaej9bkwYAI9PavaSjUNau4K/zg==', N'6d94209c-f51a-456f-a7df-91349d69fd6c', NULL, 0, 0, NULL, 1, 0, N'khushboo'),
    (N'7dd3fe01-f4a0-4c4b-894e-03c71f2bd982', N'Vishal', N'N/A', N'vishal@wec.gen.tr', 0, N'AOq5kxpZlUH5INDxCUeNopDsXrDmuB19eY2BaOZ3jmzd+/D5lW5qVgUNHX5wwmJZ9A==', N'316adeca-2507-4857-a3e7-86d14a7887d4', NULL, 0, 0, NULL, 1, 0, N'vishal'),
    (N'963ec312-6edc-429f-94c6-ef30377a942a', N'Hussein', N'N/A', N'hussein@wec.gen.tr', 0, N'ALhP0i40koqoNzoq2pdzjXvWSqFD8IeKt4WXQsQOLlnM/wUkjE2pXZCCZWdOGuemRQ==', N'3cba445f-ea03-407d-b2e9-428fcf008cdd', NULL, 0, 0, NULL, 1, 0, N'hussein'),
    (N'a188a0bc-77cc-4f6e-8b8b-75c9ae7deab5', N'Kavita', N'N/A', N'kavita@wec.gen.tr', 0, N'AObvSYW/4P+GmKEEeWvWDJK86flmkF1/QjxFbHpG4doz+R+lKuak/NSBsgxtDdcnxQ==', N'aa153d77-dcbb-4070-907b-4a0279646844', NULL, 0, 0, NULL, 1, 0, N'kavita'),
    (N'c915d504-7778-42a8-9b84-a58a3bc7fa5f', N'Göksel', N'Akgün', N'goksel@wec.gen.tr', 0, N'ANIlw9UsCxumdMNUMsAcLJz/6CxUDcC1r44WDSWgSPIVLtl2JTaEKCIZig4agIXmXA==', N'b7376e15-e303-4523-b80d-6c081175d974', NULL, 0, 0, NULL, 1, 0, N'goksel'),
    (N'd7e14acf-677f-42c2-9880-d435bd9bc00e', N'Alkan', N'Arslan', N'alkan@wec.gen.tr', 0, N'AJykf87wKx7hKFhLhGfOayahVjUgV0jU3A2XePwm0Qt13nFo7XLLOJ7JyNmKjM9nlg==', N'1992db02-6251-4b32-b655-41a27d5b9b0e', NULL, 0, 0, NULL, 1, 0, N'alkan'),
    (N'db4a0764-bd48-4490-ab3a-7de4bf2d7a1a', N'Aslı', N'Kırık', N'asli@wec.gen.tr', 0, N'AAPB8uCz0F/sAJTrABWtGMqzmDUVXy7r/Q/xWreNIqnEt1EQa+LQBFcFLLy044VEnA==', N'1acc3c65-f894-49fc-bfb3-4e85ae0aad46', NULL, 0, 0, NULL, 1, 0, N'asli'),
    (N'e0d89e55-5331-468a-a614-fc4857464103', N'Adam', N'N/A', N'adam@wec.gen.tr', 0, N'AGwkqAlTZChkTFJivB2eilkhBIWSm4ibACFyx18jnsHmx/U8LHTzILzoIVmubeDhyA==', N'2ccdbf8d-b155-44d6-b24a-e73200996edf', NULL, 0, 0, NULL, 1, 0, N'adam'),
    (N'e671565e-4d1d-4f2f-a734-65a4a7f515bc', N'Natalie', N'N/A', N'natalie@wec.gen.tr', 0, N'AMbc7TUDshFQ1Cnwvs8vEBoTb7rtRIYr5YAlxou+VIP4Bnw4axpWzDiREmTq/HApFg==', N'b785f403-69ae-4eb0-b87b-d717c3e1bd71', NULL, 0, 0, NULL, 1, 0, N'natalie'),
    (N'ea04ea33-dc2e-46c3-8067-2804e8026947', N'Helen', N'N/A', N'helen@wec.gen.tr', 0, N'AABn/+E509pz0y2N5E0zvGeh9HjP2AoB44sCXrn8Gz29hIECt77qs0861oMsWC80qg==', N'14c86091-d381-4350-9f78-a2f5a2f9ae59', NULL, 0, 0, NULL, 1, 0, N'helen'),
    (N'f2038f98-342a-4d03-b314-1528dfd40284', N'Hatice', N'Akgün', N'hatice@wec.gen.tr', 0, N'ADbioOHmYInEso0o0GPK1oOD2uKH4mmA4KNpLGC6QJHCsf6ZeGnjnPsXc/Lb5DuQWA==', N'50b915d1-9ecb-43bc-ab18-68f7872e98ba', NULL, 0, 0, NULL, 1, 0, N'hatice'),
    (N'fa363a7c-8d0a-48ad-80bf-756c33d89139', N'Enes', N'Döner', N'enes@wec.gen.tr', 0, N'ADNPU3KN9gXUsVHoHG0GqSepNL+e2zttrvM5MyBytEUD7Y/oO/qyzlJUZaUBMZQIUA==', N'0199c94e-95ad-4c20-bdfe-64128cef5658', NULL, 0, 0, NULL, 1, 0, N'enes'),
    (N'fcd1cdc8-4f42-4f03-b550-dacc4c0344e7', N'Janu', N'N/A', N'janu@wec.gen.tr', 0, N'AAnY21EVQdzeUyXfUUN1UqQC06ynNgz1T96AC49x4PD5zkADw4Pokmi8KUzLUeafBA==', N'616ecd8f-2b3c-42f9-a4ad-151230d65c40', NULL, 0, 0, NULL, 1, 0, N'janu')
Go
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES
    (N'070b8a93-77ec-4465-8292-0e154851987a', N'f9ab6164-0a5e-40c6-94ba-1e7a33022886'),
    (N'1101ecbd-1ea4-45f7-9429-e8bd6248d01e', N'f9ab6164-0a5e-40c6-94ba-1e7a33022886'),
    (N'14d50429-f130-4ed1-9944-e3a7305e9c9e', N'f9ab6164-0a5e-40c6-94ba-1e7a33022886'),
    (N'262ffe38-670e-4f8d-8c55-90faeff819c4', N'f9ab6164-0a5e-40c6-94ba-1e7a33022886'),
    (N'2fa1203a-253b-468b-832a-2b962eb6e485', N'6bd5826a-3cc3-430c-a2be-714c5a95acd0'),
    (N'34287b42-f008-4dc2-bc02-1649f3f5b323', N'f9ab6164-0a5e-40c6-94ba-1e7a33022886'),
    (N'420edb56-0114-49af-9778-14f51918a0bc', N'f9ab6164-0a5e-40c6-94ba-1e7a33022886'),
    (N'519e148d-8364-43ad-9be8-3b4491da1ef5', N'f9ab6164-0a5e-40c6-94ba-1e7a33022886'),
    (N'597f0e8d-383d-4def-9529-b47dedf881e9', N'f9ab6164-0a5e-40c6-94ba-1e7a33022886'),
    (N'5fdf0f15-cc3e-44e3-9b88-68082fbc84a2', N'f9ab6164-0a5e-40c6-94ba-1e7a33022886'),
    (N'62669683-4a19-4a09-b805-676d2d3c3e70', N'f9ab6164-0a5e-40c6-94ba-1e7a33022886'),
    (N'63308a64-d7a2-4b42-b252-c5808f5253d0', N'f9ab6164-0a5e-40c6-94ba-1e7a33022886'),
    (N'6f7d060b-5936-4bd2-81dc-2f1dd3906508', N'f9ab6164-0a5e-40c6-94ba-1e7a33022886'),
    (N'72d24c7d-8c93-4fb0-a4fc-9f8045338fb0', N'f9ab6164-0a5e-40c6-94ba-1e7a33022886'),
    (N'74cd776e-c098-4f22-8991-c3c2b788862b', N'6bd5826a-3cc3-430c-a2be-714c5a95acd0'),
    (N'7d88978e-1f32-467e-9c9b-bddbd5476f6d', N'f9ab6164-0a5e-40c6-94ba-1e7a33022886'),
    (N'7dd3fe01-f4a0-4c4b-894e-03c71f2bd982', N'f9ab6164-0a5e-40c6-94ba-1e7a33022886'),
    (N'963ec312-6edc-429f-94c6-ef30377a942a', N'f9ab6164-0a5e-40c6-94ba-1e7a33022886'),
    (N'a188a0bc-77cc-4f6e-8b8b-75c9ae7deab5', N'f9ab6164-0a5e-40c6-94ba-1e7a33022886'),
    (N'c915d504-7778-42a8-9b84-a58a3bc7fa5f', N'7a4780a3-e438-4ce8-8682-c1a94cf6ec73'),
    (N'd7e14acf-677f-42c2-9880-d435bd9bc00e', N'f9ab6164-0a5e-40c6-94ba-1e7a33022886'),
    (N'db4a0764-bd48-4490-ab3a-7de4bf2d7a1a', N'f9ab6164-0a5e-40c6-94ba-1e7a33022886'),
    (N'e0d89e55-5331-468a-a614-fc4857464103', N'f9ab6164-0a5e-40c6-94ba-1e7a33022886'),
    (N'e671565e-4d1d-4f2f-a734-65a4a7f515bc', N'f9ab6164-0a5e-40c6-94ba-1e7a33022886'),
    (N'ea04ea33-dc2e-46c3-8067-2804e8026947', N'f9ab6164-0a5e-40c6-94ba-1e7a33022886'),
    (N'f2038f98-342a-4d03-b314-1528dfd40284', N'6bd5826a-3cc3-430c-a2be-714c5a95acd0'),
    (N'f2038f98-342a-4d03-b314-1528dfd40284', N'7a4780a3-e438-4ce8-8682-c1a94cf6ec73'),
    (N'fa363a7c-8d0a-48ad-80bf-756c33d89139', N'f9ab6164-0a5e-40c6-94ba-1e7a33022886'),
    (N'fcd1cdc8-4f42-4f03-b550-dacc4c0344e7', N'f9ab6164-0a5e-40c6-94ba-1e7a33022886')
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
