# Shifts Logger
###### By P13 for The C# Academy

## What's the project
In this project, there are two application: a Web API and an console UI.<br>
This simulate a Shifts Logger as its name says. The data must be stored in a database on an SQL servver.<br>
They must be retrieve using http requests in the web API.<br>
Users must be able to enter their shift hours in the console UI.

## Requirements
- The application should record a worker's shifts.

- It consists of two applications: the Web API and the UI that will call it.

- All validation and user input should happen in the UI app.

- The API's controller should be lean. Any logic should be handled in a separate "service".

- SQL Server should be used, not SQLite

- "Code first" approach must be used to create your database, using Entity Framework's migrations tool.

- The front-end project needs to have try-catch blocks around the API calls so it handles unexpected errors (i.e. the API isn't running or returns a 500 error.)

## Structure
The two project are tied to each other:
- The Web API is used to acccess the database and retrieve data.
- The UI is used to make request to the API and retrieve data from it.

For the UI, the projet consist of a simple menu, the user can see and manage shifts and/or users (add,delete,update).

## Ressources
- Introduction to web APIs : [link](https://developer.mozilla.org/en-US/docs/Learn_web_development/Extensions/Client-side_APIs/Introduction)
- Tutorial: Create a controller-based web API with ASP.NET Core: [link](https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-6.0&tabs=visual-studio)
- Back-end Web Development with .NET for Beginners: [link](https://learn.microsoft.com/en-us/shows/back-end-web-development-with-dotnet-for-beginners/)
- How to Document Your Web API Using Swagger: [video](https://www.youtube.com/watch?v=IYWOWxw7dys)
- Postman API tutorial for beginners: [video](https://www.youtube.com/watch?v=FjgYtQK_zLE)