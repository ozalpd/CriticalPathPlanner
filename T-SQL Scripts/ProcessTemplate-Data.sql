/* Template */
SET IDENTITY_INSERT [dbo].[ProcessTemplates] ON 
INSERT [dbo].[ProcessTemplates] ([Id], [TemplateName], [DefaultTitle], [IsApproved], [ModifyNr], [ModifyDate], [ModifierId], [ModifierIp], [CreateDate], [CreatorId], [CreatorIp]) VALUES
 (1, N'Template Nr 1', N'Order Process', 1, 2, CAST(N'2015-10-19 06:09:58.297' AS DateTime), N'74cd776e-c098-4f22-8991-c3c2b788862b', N'::1', CAST(N'2015-10-19 05:41:04.733' AS DateTime), N'74cd776e-c098-4f22-8991-c3c2b788862b', N'::1')
SET IDENTITY_INSERT [dbo].[ProcessTemplates] OFF
GO

SET IDENTITY_INSERT [dbo].[ProcessStepTemplates] ON
INSERT [dbo].[ProcessStepTemplates] ([Id], [Title], [DisplayOrder], [IgnoreInRepeat], [ProcessTemplateId], [RequiredWorkDays], [ModifyNr], [ModifyDate], [ModifierId], [ModifierIp], [CreateDate], [CreatorId], [CreatorIp]) VALUES
    (1, N'Initial Sample', 900, 1, 1, 0, 3, CAST(N'2016-01-12 14:21:48.827' AS DateTime), N'74cd776e-c098-4f22-8991-c3c2b788862b', N'92.45.139.186', CAST(N'2015-10-19 05:46:28.977' AS DateTime), N'74cd776e-c098-4f22-8991-c3c2b788862b', N'::1'),
    (2, N'Base Fabric', 1000, 0, 1, 1, 1, CAST(N'2015-10-19 05:41:52.280' AS DateTime), N'74cd776e-c098-4f22-8991-c3c2b788862b', N'::1', CAST(N'2015-10-19 05:41:52.280' AS DateTime), N'74cd776e-c098-4f22-8991-c3c2b788862b', N'::1'),
    (3, N'Lab Dips', 2000, 1, 1, 1, 1, CAST(N'2015-10-19 05:42:33.990' AS DateTime), N'74cd776e-c098-4f22-8991-c3c2b788862b', N'::1', CAST(N'2015-10-19 05:42:33.990' AS DateTime), N'74cd776e-c098-4f22-8991-c3c2b788862b', N'::1'),
    (4, N'Strike Off', 3000, 0, 1, 1, 1, CAST(N'2015-10-19 05:43:00.220' AS DateTime), N'74cd776e-c098-4f22-8991-c3c2b788862b', N'::1', CAST(N'2015-10-19 05:43:00.220' AS DateTime), N'74cd776e-c098-4f22-8991-c3c2b788862b', N'::1'),
    (5, N'Kimball Rcv Date', 3500, 0, 1, 0, 2, CAST(N'2016-01-12 14:30:38.477' AS DateTime), N'74cd776e-c098-4f22-8991-c3c2b788862b', N'92.45.139.186', CAST(N'2016-01-12 14:29:52.457' AS DateTime), N'74cd776e-c098-4f22-8991-c3c2b788862b', N'92.45.139.186'),
    (6, N'Ratio Rcv Date', 3600, 0, 1, 0, 2, CAST(N'2016-01-12 14:32:20.440' AS DateTime), N'74cd776e-c098-4f22-8991-c3c2b788862b', N'92.45.139.186', CAST(N'2016-01-12 14:31:49.630' AS DateTime), N'74cd776e-c098-4f22-8991-c3c2b788862b', N'92.45.139.186'),
    (7, N'PP Sample', 4000, 0, 1, 0, 3, CAST(N'2016-01-12 14:23:59.227' AS DateTime), N'74cd776e-c098-4f22-8991-c3c2b788862b', N'92.45.139.186', CAST(N'2015-10-19 05:48:01.350' AS DateTime), N'74cd776e-c098-4f22-8991-c3c2b788862b', N'::1'),
    (8, N'PP Approval Date', 4500, 0, 1, 0, 3, CAST(N'2016-01-12 14:27:30.670' AS DateTime), N'74cd776e-c098-4f22-8991-c3c2b788862b', N'92.45.139.186', CAST(N'2015-10-19 05:48:22.077' AS DateTime), N'74cd776e-c098-4f22-8991-c3c2b788862b', N'::1'),
    (9, N'Bulk Fabric Arrival Date', 5000, 0, 1, 4, 3, CAST(N'2016-01-12 14:27:17.830' AS DateTime), N'74cd776e-c098-4f22-8991-c3c2b788862b', N'92.45.139.186', CAST(N'2015-10-19 05:48:42.750' AS DateTime), N'74cd776e-c098-4f22-8991-c3c2b788862b', N'::1'),
    (10, N'Bulk Test Reports', 6000, 0, 1, 3, 2, CAST(N'2016-01-12 14:25:57.413' AS DateTime), N'74cd776e-c098-4f22-8991-c3c2b788862b', N'92.45.139.186', CAST(N'2015-10-19 05:47:04.457' AS DateTime), N'74cd776e-c098-4f22-8991-c3c2b788862b', N'::1'),
    (11, N'Trims', 7000, 0, 1, 3, 2, CAST(N'2016-01-12 14:26:07.223' AS DateTime), N'74cd776e-c098-4f22-8991-c3c2b788862b', N'92.45.139.186', CAST(N'2015-10-19 05:44:41.793' AS DateTime), N'74cd776e-c098-4f22-8991-c3c2b788862b', N'::1'),
    (12, N'Packaging', 8000, 0, 1, 5, 1, CAST(N'2015-10-19 05:47:27.157' AS DateTime), N'74cd776e-c098-4f22-8991-c3c2b788862b', N'::1', CAST(N'2015-10-19 05:47:27.157' AS DateTime), N'74cd776e-c098-4f22-8991-c3c2b788862b', N'::1'),
    (13, N'Cut Date', 12000, 0, 1, 3, 1, CAST(N'2015-10-19 05:49:33.387' AS DateTime), N'74cd776e-c098-4f22-8991-c3c2b788862b', N'::1', CAST(N'2015-10-19 05:49:33.387' AS DateTime), N'74cd776e-c098-4f22-8991-c3c2b788862b', N'::1'),
    (14, N'Gold Seals', 13000, 1, 1, 0, 1, CAST(N'2015-10-19 05:49:59.577' AS DateTime), N'74cd776e-c098-4f22-8991-c3c2b788862b', N'::1', CAST(N'2015-10-19 05:49:59.577' AS DateTime), N'74cd776e-c098-4f22-8991-c3c2b788862b', N'::1'),
    (15, N'Gold Seals Approval', 13500, 0, 1, 0, 2, CAST(N'2016-01-12 14:32:52.560' AS DateTime), N'74cd776e-c098-4f22-8991-c3c2b788862b', N'92.45.139.186', CAST(N'2016-01-12 14:29:17.590' AS DateTime), N'74cd776e-c098-4f22-8991-c3c2b788862b', N'92.45.139.186'),
    (16, N'Ex-Factory Date', 14000, 0, 1, 4, 1, CAST(N'2015-10-19 05:50:26.350' AS DateTime), N'74cd776e-c098-4f22-8991-c3c2b788862b', N'::1', CAST(N'2015-10-19 05:50:26.350' AS DateTime), N'74cd776e-c098-4f22-8991-c3c2b788862b', N'::1'),
    (17, N'Into DC Date', 15000, 0, 1, 4, 1, CAST(N'2015-10-19 05:51:17.243' AS DateTime), N'74cd776e-c098-4f22-8991-c3c2b788862b', N'::1', CAST(N'2015-10-19 05:51:17.243' AS DateTime), N'74cd776e-c098-4f22-8991-c3c2b788862b', N'::1')
Go
SET IDENTITY_INSERT [dbo].[ProcessStepTemplates] OFF
