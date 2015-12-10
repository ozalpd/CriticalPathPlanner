/*
   Additional setup commands for database CriticalPath
*/
Print 'Executing command 1...'
Alter Table [dbo].[Processes] Add Constraint FK_Process_CurrentStepId Foreign Key([CurrentStepId]) References [dbo].[ProcessSteps]([Id])
Go
Print 'Executing command 2...'
Alter Table [dbo].[ProcessSteps] Add Constraint FK_ProcessStep_ProcessId Foreign Key([ProcessId]) References [dbo].[Processes]([Id])
Go
