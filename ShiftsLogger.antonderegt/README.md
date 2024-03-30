# ShiftsLogger

## SQL Server Setup on Mac
First download Docker and Azure Data Studio.

Pull docker image:
```bash
docker pull mcr.microsoft.com/mssql/server:2022-latest
```

Run SQL Server
```bash
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=123456aA" \
   -p 1433:1433 --name sql1 --hostname sql1 -d \
   mcr.microsoft.com/mssql/server:2022-latest
```

In Azure Data Studio use connection string: `Data Source=localhost; Initial Catalog=ShiftsLogger; User id=SA; password=123456aA; Encrypt=false`.