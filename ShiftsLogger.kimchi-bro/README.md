# Setup
- Define preferred server and database name editing `appsettings.json`file's connection string in `ShiftLoggerAPI` project in `"DefaultConnection"` property
    - Default values:
    `Server=(localdb)\\MSSQLLocalDB;`
    `Database=Shifts;`
- A new database will be created the first time you run the app
# Db Migration
- Before any change in database schema (i.e. adding or changing property type, adding new model) perform database migration:
    - Execute following commands in NuGet Package Manager console
    ```
    Add-Migration InitialCreate
    Update-Database
    ```
- That will create initial migration to which you could roll back and cancel all database schema changes if something goes wrong
# Features
- Spectre.Console UI
- MS SQL Server
- Entity Framework
- Code-first approach for database creation