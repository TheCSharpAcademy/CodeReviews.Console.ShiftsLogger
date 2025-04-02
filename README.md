# Shifts Logger
A simple app for logging employees' shifts

## Technologies used
- ASP.NET Core
- Entity Framework Core
- Microsoft SQL Server
- RestSharp
- Spectre.Console

## Features
- Managing employees
- Logging and managing shifts of employees

(Both employees and shifts are stored in MSSQL database, connection to the database is handled by the API)

## How to setup
In ShiftsLogger.API -> appsettings.json find **"DefaultConnection": ""** and put your connection string to the Microsoft SQL Server database there
