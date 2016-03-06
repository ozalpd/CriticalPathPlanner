@echo .
@echo Creating Database...
@sqlcmd -S .\SqlExpress -d Master -E -Q "CREATE DATABASE [CriticalPath]"
@echo .


@echo .
@echo .
@echo 1 - Executing _IdentityDB.sql...
@sqlcmd -S .\SqlExpress -d CriticalPath -E -i _IdentityDB.sql

@echo .
@echo .
@echo 2 - Executing EmployeePosition.sql...
@sqlcmd -S .\SqlExpress -d CriticalPath -E -i EmployeePosition.sql
@sqlcmd -S .\SqlExpress -d CriticalPath -E -i EmployeePosition-Data.sql

@echo .
@echo .
@echo 3 - Executing Employee.sql...
@sqlcmd -S .\SqlExpress -d CriticalPath -E -i Employee.sql
@sqlcmd -S .\SqlExpress -d CriticalPath -E -i Employees.Data.sql

@echo .
@echo .
@echo 4 - Executing FreightTerm.sql...
@sqlcmd -S .\SqlExpress -d CriticalPath -E -i FreightTerm.sql

@echo .
@echo .
@echo 5 - Executing Currency.sql...
@sqlcmd -S .\SqlExpress -d CriticalPath -E -i Currency.sql

@echo .
@echo .
@echo 6 - Executing Country.sql...
@sqlcmd -S .\SqlExpress -d CriticalPath -E -i Country.sql
@sqlcmd -S .\SqlExpress -d CriticalPath -E -i Country-data.sql

@echo .
@echo .
@echo 7 - Executing SizingStandard.sql...
@sqlcmd -S .\SqlExpress -d CriticalPath -E -i SizingStandard.sql

@echo .
@echo .
@echo 8 - Executing Sizing.sql...
@sqlcmd -S .\SqlExpress -d CriticalPath -E -i Sizing.sql

@echo .
@echo .
@echo 9 - Executing Company.sql...
@sqlcmd -S .\SqlExpress -d CriticalPath -E -i Company.sql

@echo .
@echo .
@echo 10 - Executing Contact.sql...
@sqlcmd -S .\SqlExpress -d CriticalPath -E -i Contact.sql

@echo .
@echo .
@echo 11 - Executing Customer.sql...
@sqlcmd -S .\SqlExpress -d CriticalPath -E -i Customer.sql

@echo .
@echo .
@echo 12 - Executing CustomerDepartment.sql...
@sqlcmd -S .\SqlExpress -d CriticalPath -E -i CustomerDepartment.sql

@echo .
@echo .
@echo 13 - Executing Supplier.sql...
@sqlcmd -S .\SqlExpress -d CriticalPath -E -i Supplier.sql

@echo .
@echo .
@echo 14 - Executing Licensor.sql...
@sqlcmd -S .\SqlExpress -d CriticalPath -E -i Licensor.sql

@echo .
@echo .
@echo 15 - Executing Manufacturer.sql...
@sqlcmd -S .\SqlExpress -d CriticalPath -E -i Manufacturer.sql

@echo .
@echo .
@echo 16 - Executing ProductCategory.sql...
@sqlcmd -S .\SqlExpress -d CriticalPath -E -i ProductCategory.sql

@echo .
@echo .
@echo 17 - Executing Product.sql...
@sqlcmd -S .\SqlExpress -d CriticalPath -E -i Product.sql

@echo .
@echo .
@echo 18 - Executing PurchaseOrder.sql...
@sqlcmd -S .\SqlExpress -d CriticalPath -E -i PurchaseOrder.sql

@echo .
@echo .
@echo 19 - Executing POImage.sql...
@sqlcmd -S .\SqlExpress -d CriticalPath -E -i POImage.sql

@echo .
@echo .
@echo 20 - Executing POAttachment.sql...
@sqlcmd -S .\SqlExpress -d CriticalPath -E -i POAttachment.sql

@echo .
@echo .
@echo 21 - Executing POSizeRatio.sql...
@sqlcmd -S .\SqlExpress -d CriticalPath -E -i POSizeRatio.sql

@echo .
@echo .
@echo 22 - Executing ProcessTemplate.sql...
@sqlcmd -S .\SqlExpress -d CriticalPath -E -i ProcessTemplate.sql

@echo .
@echo .
@echo 23 - Executing Process.sql...
@sqlcmd -S .\SqlExpress -d CriticalPath -E -i Process.sql

@echo .
@echo .
@echo 24 - Executing ProcessStepTemplate.sql...
@sqlcmd -S .\SqlExpress -d CriticalPath -E -i ProcessStepTemplate.sql
@sqlcmd -S .\SqlExpress -d CriticalPath -E -i ProcessTemplate-Data.sql

@echo .
@echo .
@echo 25 - Executing ProcessStep.sql...
@sqlcmd -S .\SqlExpress -d CriticalPath -E -i ProcessStep.sql

@echo .
@echo .
@echo 26 - Executing ProcessStepRevision.sql...
@sqlcmd -S .\SqlExpress -d CriticalPath -E -i ProcessStepRevision.sql

@echo .
@echo .
@echo 27 - Executing _FinishingSetup.sql...
@sqlcmd -S .\SqlExpress -d CriticalPath -E -i _FinishingSetup.sql

@echo .
@echo .
@echo 28 - Executing ProductSupplier.sql...
@sqlcmd -S .\SqlExpress -d CriticalPath -E -i ProductSupplier.sql

@echo .
@echo * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
@echo *
@echo * Create scripts for database CriticalPath executed on Server .\SqlExpress
@echo *
@echo * Please check history for errors.
@echo *
@echo * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
@echo .
@pause
