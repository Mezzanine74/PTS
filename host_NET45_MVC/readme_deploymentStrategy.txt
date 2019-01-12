Deployment strategy


- search for host_NET45 in Visual studio and sql server
  Founded items
	Report : PR_CoverPagesSplitInvoice, PR_CoverPages > referencing mercury logo and root folder
	SQL Server : BackupDatabase stored procedure targetting folder
		SELECT ROUTINE_NAME, ROUTINE_DEFINITION
		FROM INFORMATION_SCHEMA.ROUTINES 
		WHERE ROUTINE_DEFINITION LIKE '%host_NET45%' 
	Visual Studio : Surprisingly there is no reference
	ScheduledWorksOnPTS : Referencing IP adress, no root folder name


REVERSE Strategy

C:\host_NET45_MVC\Certification
C:\host_NET45_MVC\CommercialOffers
C:\host_NET45_MVC\CONTRACT
C:\host_NET45_MVC\DailyReportToPatrick
C:\host_NET45_MVC\DailyReportToPatrickRev
C:\host_NET45_MVC\DeliveryPackingLists
C:\host_NET45_MVC\FeedOlgaFile
C:\host_NET45_MVC\images
C:\host_NET45_MVC\InsuranceCertification
C:\host_NET45_MVC\ItPayments
C:\host_NET45_MVC\NonZeroBalanceProjects
C:\host_NET45_MVC\PaymentList
C:\host_NET45_MVC\ProjectWeeklySummary
C:\host_NET45_MVC\REQUEST
C:\host_NET45_MVC\SqlDatabase_Backups
C:\host_NET45_MVC\video



Issues:
	 DONE !! MVC client side validation not working 

 	 Scripts and styles should be bundled

	is there any way to move all webforms into single folder ?


