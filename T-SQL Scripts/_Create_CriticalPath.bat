@echo .
@echo Creating Database...
@sqlcmd -S .\SqlExpress -d Master -E -Q "CREATE DATABASE [CriticalPath]"
@echo .


@echo .
@echo .
@echo 1 - Executing Company.sql...
@sqlcmd -S .\SqlExpress -d CriticalPath -E -i Company.sql

@echo .
@echo .
@echo 2 - Executing Contact.sql...
@sqlcmd -S .\SqlExpress -d CriticalPath -E -i Contact.sql

@echo .
@echo .
@echo 3 - Executing Supplier.sql...
@sqlcmd -S .\SqlExpress -d CriticalPath -E -i Supplier.sql

@echo .
@echo .
@echo 4 - Executing Customer.sql...
@sqlcmd -S .\SqlExpress -d CriticalPath -E -i Customer.sql

@echo .
@echo .
@echo 5 - Executing ProductCategory.sql...
@sqlcmd -S .\SqlExpress -d CriticalPath -E -i ProductCategory.sql

@echo .
@echo .
@echo 6 - Executing Product.sql...
@sqlcmd -S .\SqlExpress -d CriticalPath -E -i Product.sql

@echo .
@echo .
@echo 7 - Executing PuchaseOrder.sql...
@sqlcmd -S .\SqlExpress -d CriticalPath -E -i PuchaseOrder.sql

@echo .
@echo .
@echo 8 - Executing OrderItem.sql...
@sqlcmd -S .\SqlExpress -d CriticalPath -E -i OrderItem.sql

@echo .
@echo .
@echo 9 - Executing ProcessTemplate.sql...
@sqlcmd -S .\SqlExpress -d CriticalPath -E -i ProcessTemplate.sql

@echo .
@echo .
@echo 10 - Executing Process.sql...
@sqlcmd -S .\SqlExpress -d CriticalPath -E -i Process.sql

@echo .
@echo .
@echo 11 - Executing ProcessStep.sql...
@sqlcmd -S .\SqlExpress -d CriticalPath -E -i ProcessStep.sql

@echo .
@echo .
@echo 12 - Executing ProcessStepTemplate.sql...
@sqlcmd -S .\SqlExpress -d CriticalPath -E -i ProcessStepTemplate.sql

@echo .
@echo .
@echo 13 - Executing _FinishingSetup.sql...
@sqlcmd -S .\SqlExpress -d CriticalPath -E -i _FinishingSetup.sql

@echo .
@echo .
@echo 14 - Executing _IdentityDB.sql...
@sqlcmd -S .\SqlExpress -d CriticalPath -E -i _IdentityDB.sql

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
