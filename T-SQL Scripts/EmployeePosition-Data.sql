SET IDENTITY_INSERT [dbo].[EmployeePositions] ON 
INSERT [dbo].[EmployeePositions] ([Id], [Position], [AppDefault], [CreateDate], [CreatorId], [CreatorIp]) VALUES
    (1, N'Designer', 1, CAST(N'2016-01-27 12:06:55.840' AS DateTime), N'74cd776e-c098-4f22-8991-c3c2b788862b', N'::1'),
	(2, N'Merchandiser', 1, CAST(N'2016-01-27 12:11:33.027' AS DateTime), N'74cd776e-c098-4f22-8991-c3c2b788862b', N'::1')
SET IDENTITY_INSERT [dbo].[EmployeePositions] OFF
