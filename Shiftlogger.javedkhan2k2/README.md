# Shift Logger Console Application

## Project Description

This Application is Part of Console Application Project
at [CSharpAcademy](https://thecsharpacademy.com/project/15/drinks).

## The Application Requirements

* This is an application where you should record a worker's shifts.
* You need to create two applications: the Web API and the UI
that will call it.
* All validation and user input should happen in the UI app.
* Your API's controller should be lean. Any logic should
be handled in a separate "service".
* You should use SQL Server, not SQLite
* You should use the "code first" approach to create your
database, using Entity Framework's migrations tool.
* Your front-end project needs to have try-catch blocks
around the API calls so it handles unexpected errors
(i.e. the API isn't running or returns a 500 error.)

## How to run the Application

Microsoft [Secret Manager](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-8.0&tabs=windows)
is used to store and retrieve Database Connection. To run the
application set the ConnectionStrings:DefaultConnection
in the Shiftlogger.javedkhan2k2 Project.  
e.g. dotnet user-secrets ConnectionStrings:DefaultConnection
= Server=localhost;Database=Shiftlogger;
User Id=YourUserID;Password=YourPassword;TrustServerCertificate=True;

## Application Usage

Users can add/delete/edit/update Workers and
Shifts using the Main menu.
