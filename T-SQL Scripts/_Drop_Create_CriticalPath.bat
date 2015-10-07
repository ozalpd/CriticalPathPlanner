@echo .
@echo Deleting Database...
@sqlcmd -S ".\SqlExpress" -d Master -E -Q "IF  EXISTS (SELECT name FROM sys.databases WHERE name = N'CriticalPath') DROP DATABASE [CriticalPath]"

@echo .
@call _Create_CriticalPath.bat
