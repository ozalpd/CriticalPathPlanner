/* Template */
SET IDENTITY_INSERT [dbo].[ProcessTemplates] ON 
INSERT [dbo].[ProcessTemplates] ([Id], [TemplateName], [DefaultTitle], [IsApproved], [ModifyNr], [ModifyDate], [ModifierId], [ModifierIp], [CreateDate], [CreatorId], [CreatorIp]) VALUES
 (1, N'Template Nr 1', N'Order Process', 1, 2, CAST(N'2015-10-19 06:09:58.297' AS DateTime), N'74cd776e-c098-4f22-8991-c3c2b788862b', N'::1', CAST(N'2015-10-19 05:41:04.733' AS DateTime), N'74cd776e-c098-4f22-8991-c3c2b788862b', N'::1')
SET IDENTITY_INSERT [dbo].[ProcessTemplates] OFF
GO

SET IDENTITY_INSERT [dbo].[ProcessStepTemplates] ON
INSERT [dbo].[ProcessStepTemplates] ([Id], [Title], [DisplayOrder], [ProcessTemplateId], [RequiredWorkDays], [IgnoreInRepeat], [ModifyNr], [ModifyDate], [ModifierId], [ModifierIp], [CreateDate], [CreatorId], [CreatorIp]) VALUES
    (1, N'Base Fabric', 1000, 1, 1, 0, 1, CAST(N'2015-10-19 05:41:52.280' AS DateTime), N'74cd776e-c098-4f22-8991-c3c2b788862b', N'::1', CAST(N'2015-10-19 05:41:52.280' AS DateTime), N'74cd776e-c098-4f22-8991-c3c2b788862b', N'::1'),
    (2, N'Lab Dips', 2000, 1, 1, 1, 1, CAST(N'2015-10-19 05:42:33.990' AS DateTime), N'74cd776e-c098-4f22-8991-c3c2b788862b', N'::1', CAST(N'2015-10-19 05:42:33.990' AS DateTime), N'74cd776e-c098-4f22-8991-c3c2b788862b', N'::1'),
    (3, N'Strike Off', 3000, 1, 1, 0, 1, CAST(N'2015-10-19 05:43:00.220' AS DateTime), N'74cd776e-c098-4f22-8991-c3c2b788862b', N'::1', CAST(N'2015-10-19 05:43:00.220' AS DateTime), N'74cd776e-c098-4f22-8991-c3c2b788862b', N'::1'),
    (4, N'Bulk Fabric', 4000, 1, 1, 0, 1, CAST(N'2015-10-19 05:43:53.443' AS DateTime), N'74cd776e-c098-4f22-8991-c3c2b788862b', N'::1', CAST(N'2015-10-19 05:43:53.443' AS DateTime), N'74cd776e-c098-4f22-8991-c3c2b788862b', N'::1'),
    (5, N'Trims', 5000, 1, 3, 0, 1, CAST(N'2015-10-19 05:44:41.793' AS DateTime), N'74cd776e-c098-4f22-8991-c3c2b788862b', N'::1', CAST(N'2015-10-19 05:44:41.793' AS DateTime), N'74cd776e-c098-4f22-8991-c3c2b788862b', N'::1'),
    (6, N'Correct Ref Sample', 6000, 1, 3, 0, 1, CAST(N'2015-10-19 05:46:28.977' AS DateTime), N'74cd776e-c098-4f22-8991-c3c2b788862b', N'::1', CAST(N'2015-10-19 05:46:28.977' AS DateTime), N'74cd776e-c098-4f22-8991-c3c2b788862b', N'::1'),
    (7, N'Bulk Test Reports', 7000, 1, 3, 0, 1, CAST(N'2015-10-19 05:47:04.457' AS DateTime), N'74cd776e-c098-4f22-8991-c3c2b788862b', N'::1', CAST(N'2015-10-19 05:47:04.457' AS DateTime), N'74cd776e-c098-4f22-8991-c3c2b788862b', N'::1'),
    (8, N'Packaging', 8000, 1, 5, 0, 1, CAST(N'2015-10-19 05:47:27.157' AS DateTime), N'74cd776e-c098-4f22-8991-c3c2b788862b', N'::1', CAST(N'2015-10-19 05:47:27.157' AS DateTime), N'74cd776e-c098-4f22-8991-c3c2b788862b', N'::1'),
    (9, N'Red Seal', 9000, 1, 0, 1, 1, CAST(N'2015-10-19 05:48:01.350' AS DateTime), N'74cd776e-c098-4f22-8991-c3c2b788862b', N'::1', CAST(N'2015-10-19 05:48:01.350' AS DateTime), N'74cd776e-c098-4f22-8991-c3c2b788862b', N'::1'),
    (10, N'Green Seal', 10000, 1, 0, 1, 1, CAST(N'2015-10-19 05:48:22.077' AS DateTime), N'74cd776e-c098-4f22-8991-c3c2b788862b', N'::1', CAST(N'2015-10-19 05:48:22.077' AS DateTime), N'74cd776e-c098-4f22-8991-c3c2b788862b', N'::1'),
    (11, N'Bulk Fabric Arrival Date', 1100, 1, 4, 0, 1, CAST(N'2015-10-19 05:48:42.750' AS DateTime), N'74cd776e-c098-4f22-8991-c3c2b788862b', N'::1', CAST(N'2015-10-19 05:48:42.750' AS DateTime), N'74cd776e-c098-4f22-8991-c3c2b788862b', N'::1'),
    (12, N'Cut Date', 12000, 1, 3, 0, 1, CAST(N'2015-10-19 05:49:33.387' AS DateTime), N'74cd776e-c098-4f22-8991-c3c2b788862b', N'::1', CAST(N'2015-10-19 05:49:33.387' AS DateTime), N'74cd776e-c098-4f22-8991-c3c2b788862b', N'::1'),
    (13, N'Gold Seals', 13000, 1, 0, 1, 1, CAST(N'2015-10-19 05:49:59.577' AS DateTime), N'74cd776e-c098-4f22-8991-c3c2b788862b', N'::1', CAST(N'2015-10-19 05:49:59.577' AS DateTime), N'74cd776e-c098-4f22-8991-c3c2b788862b', N'::1'),
    (14, N'Ex-Factory Date', 14000, 1, 4, 0, 1, CAST(N'2015-10-19 05:50:26.350' AS DateTime), N'74cd776e-c098-4f22-8991-c3c2b788862b', N'::1', CAST(N'2015-10-19 05:50:26.350' AS DateTime), N'74cd776e-c098-4f22-8991-c3c2b788862b', N'::1'),
    (15, N'Into DC Date', 15000, 1, 4, 0, 1, CAST(N'2015-10-19 05:51:17.243' AS DateTime), N'74cd776e-c098-4f22-8991-c3c2b788862b', N'::1', CAST(N'2015-10-19 05:51:17.243' AS DateTime), N'74cd776e-c098-4f22-8991-c3c2b788862b', N'::1')
Go
SET IDENTITY_INSERT [dbo].[ProcessStepTemplates] OFF
