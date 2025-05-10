# Shifts Logger application
This is a shifts logger application developed using C#, ASP.NET Core, Entity Framework & SQL Server. The app consists of an API offering CRUD methods for logging Shifts & the console app that consumes it.

## Requirements / Description
1) This is an application where you should record a worker's shifts.
2) You need to create two applications: the Web API and the UI that will call it.
3) All validation and user input should happen in the UI app.
4) Your API's controller should be lean. Any logic should be handled in a separate "service".
5) You should use SQL Server, not SQLite
6) You should use the "code first" approach to create your database, using Entity Framework's migrations tool.
7) Your front-end project needs to have try-catch blocks around the API calls so it handles unexpected errors (i.e. the API isn't running or returns a 500 error.)

## Before using the application
* After cloning the application, update the *DefaultConnection* property in appsettings.json with your connection string to target your SQL Server
* Start the application, making sure the selected startup project is the API project.
* While the API is running in the background, open another instance of the app solution, start it again this time with the startup project being the console application.

## General Info
* The console application consists of menu presenting CRUD options for Shifts.
* Each menu option hits an endpoint of the API project which carries out the required CRUD operation