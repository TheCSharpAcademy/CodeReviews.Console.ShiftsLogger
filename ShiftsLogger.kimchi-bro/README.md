# Setup
- Define preferred server and database name editing `appsettings.json`file's connection string in `ShiftLoggerAPI` project in `"DefaultConnection"` property
    - Default values:
    `Server=(localdb)\\MSSQLLocalDB;`
    `Database=Shifts;`
- A new database will be created the first time you run the app
- Visit `https://localhost:7088/scalar/` to test API
# Db Migration
- Before any change in database schema (i.e. adding or changing property type, adding new model) perform database migration:
    - Execute following commands in NuGet Package Manager console (PMC)
    ```
    Add-Migration <your-migration-name>
    ```
- You could roll back and cancel all database schema changes if something goes wrong reverting to a previous migration
    ```
    Update-Database <previous-migration-name>
    ```
- Apply changes to you database schema if everything is all right
    ```
    Update-Database
    ```
# Features
- Spectre.Console UI
- Scalar API Client
- MS SQL Server
- Entity Framework Core
- Code-first approach for database creation