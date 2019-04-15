use GNB;
select * from GNB.dbo.Transactions

dotnet ef DbContext scaffold "Server=(local);Database=GNB;Integrated Security=True;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer  -force