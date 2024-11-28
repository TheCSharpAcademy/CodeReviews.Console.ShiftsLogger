﻿﻿# ShiftsLogger

## Given Requirements:

- [x] This is an application where worker's shifts logging occurs.
- [x] Сreate two applications: the Web API and the UI that will call it.
- [x] Using only Entity Framework, raw SQL isn't allowed.
- [x] All validation and user input should happen in the UI app.
- [x] Your API's controller should be lean. Any logic should be handled in a separate "service".
- [x] Using SQL Server.
- [x] Using the "code first" approach to create the database.
- [x] Front-end project needs to have try-catch blocks around the API calls to handle unexpected errors.

## Features

- SQL Server database connection with Entity Framework.
  > [!IMPORTANT]
  > After downloading the project, you should check appsetting.json and write your own path to connect the db.
  >
  > ![image](https://github.com/TwilightSaw/CodeReviews.Console.ShiftsLogger/blob/main/ShiftsLogger.TwilightSaw/images/appsettings.png)

> [!IMPORTANT]
> Also you should do starting migrations to create db with all necessary tables, simply write `dotnet ef database update` in CLI.
>
> ![image]https://github.com/TwilightSaw/CodeReviews.Console.ShiftsLogger/blob/main/ShiftsLogger.TwilightSaw/images/migrations.png)

- A console based UI where you can navigate by user input.

  ![image](https://github.com/TwilightSaw/CodeReviews.Console.ShiftsLogger/blob/main/ShiftsLogger.TwilightSaw/images/ui.png)

- CRUD functionalities.

- The seperate RESTful API project which handles all main HTTP methods.
  > [!IMPORTANT]
  > You need to start this project in the first place to be able to use the UI part.
  >
  > ![image](https://github.com/TwilightSaw/CodeReviews.Console.ShiftsLogger/blob/main/ShiftsLogger.TwilightSaw/images/api.png)

## Challenges and Learned Lessons

- Creating your first API and controllers to it is thrilling and a little terrifying at the same time, but it's essential part.
- Parsing json files for API usage is not very comfortable task.
- You need to be really careful with asynchronous parts of your code.
- Using Swagger(or analogues) is important as you know if you have done everything right from the API side before you start doing other parts.

## Areas to Improve

- Deepen API knowledge, especially JSON parsing.

## Resources Used

- C# Academy guidelines and roadmap.
- ChatGPT for new information as API creation, EF usage, JSON parsing etc..
- Spectre.Console documentation.
- EF documentation.
- This article about API - https://developer.mozilla.org/en-US/docs/Learn/JavaScript/Client-side_web_APIs/Introduction.
- This Microsoft API tutor - https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-6.0&tabs=visual-studio.
- Various StackOverflow articles.
