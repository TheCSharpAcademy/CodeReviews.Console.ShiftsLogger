# Shift Logger

A web API and a console-based application to help you manage your employees' shifts.
Developed using C#, Entity Framework Spectre.Console,
and SQL Server Express LocalDB.

## Given Requirements

- [x] This is an application where you should record a worker's shifts.
- [x] You need to create two applications: the Web API and the UI that will call it.
- [x] All validation and user input should happen in the UI app.
- [x] Your API's controller should be lean. Any logic should be handled in a separate "service".
- [x] You should use SQL Server, not SQLite
- [x] You should use the "code first" approach to create your database, using Entity Framework's migrations tool.
- [x] Your front-end project needs to have try-catch blocks around the API calls so it handles unexpected errors
(i.e. the API isn't running or returns a 500 error.)

## Features

- SQL Server database connection

  - The data is stored in a SQL Server database. I connect to it for the CRUD.
  - The database is managed by Entity Framework.
  You should add an initial migration and update the database first.

- Console-based UI to navigate the menus
  - ![image](https://github.com/user-attachments/assets/683792bc-f2bd-474c-ba0a-475780e756ee)
  - ![image](https://github.com/user-attachments/assets/7239de8f-674c-4b73-ba7e-384b07a8cb8f)

- CRUD operations

  - From the Shift Menu, you can create, show, or delete shifts.
  - From the Employee Menu, you can create, update, show, or delete employees.
  To choose an option, you make use of arrow keys and enter.
  - Inputs are validated. For start and end times you can check the given examples.
  - ![image](https://github.com/user-attachments/assets/1ecf1d84-e6fb-4911-b3af-de329c1d6420)
  - You can cancel an operation by entering the string from the configuration file.

- Swagger Documentation
  - ![image](https://github.com/user-attachments/assets/abcf4f69-507c-4727-9330-fb61da1d9db3)

## Challenges

- Getting used to Entity Framework.
- Creating an API.
- Using Swagger to test and document the API.
- Using Postman to test the API.
- Seeding data with Entity Framework.

## Lessons Learned

- XML documentation for Swagger.
- HTTP methods.
- API in .NET Core.
- Testing API via Postman.
- Decorator pattern to enhance DateTimeValidator, creating FutureDateTimeValidator.
- Thread-safe Singleton for HttpClient.
- HasData() function for seeding data in Entity Framework.

## Areas to Improve

- API design.
- HTTP verbs and status code meaning.
- Entity Framework possibilities.

## Resources used

- StackOverflow posts
- ChatGPT
- [Web API Beginner's Series](https://learn.microsoft.com/en-us/shows/back-end-web-development-with-dotnet-for-beginners/)
- [Swagger Testing Video](https://www.youtube.com/watch?v=IYWOWxw7dys)
- [Postman Guide Video](https://www.youtube.com/watch?v=FjgYtQK_zLE)
