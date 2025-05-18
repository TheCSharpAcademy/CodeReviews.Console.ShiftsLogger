# Shifts Logger Project by C# Academy

## Project Overview:
This project involves building an application to record a worker’s shifts. It consists of two components:
 - A Web API built with ASP.NET Core
- A Console-based UI that consumes the API

Project link: [Shifts Logger](https://www.thecsharpacademy.com/project/17/shifts-logger)

## Project Requirements
- Develop two applications: a Web API and a UI that interacts with it
- All user input and validation must be handled by the UI
- Keep API controllers lean; business logic should go into dedicated services
- Use SQL Server (not SQLite)
- Follow the Code-First approach with Entity Framework Core and use migrations
- Implement error handling in the UI with try-catch blocks around API calls

## Lessons Learned
This was a significant project for me. Before starting, I had no experience with ASP.NET Core and only limited exposure to working with APIs.
Initially, I tried learning through YouTube and articles but found they only scratched the surface. 
I paused the project and enrolled in a Udemy course, which helped me (amongst other things) understand:
- The capabilities of the ASP.NET Core framework
- The middleware pipeline
- Core principles behind Minimal APIs


Once I felt confident, I resumed the project with a clear plan:
- **API**:
    - Built using Minimal API due to the low number of endpoints
    - Used EF Core with Code-First approach
    - Refactored data access logic into repositories for better maintainability
    - Tested using Postman and Swagger
    - Implemented seeding functionality to assist UI testing

- **UI**:
    - Initially faced challenges due to repetitive and hard-to-read code
    - Refactored by introducing:
      - A generic ApiResponse wrapper for API results
      - A generic method for executing API calls

This project helped solidify my understanding of Minimal APIs and improved my skills in EF Core, ASP.NET Core, and RESTful design.

## Areas for improvement:
- While I’m sure there are many areas for refinement, I’m very satisfied with the outcome. The project reflects:
    - Clear separation of concerns
    - Adherence to DRY (Don't Repeat Yourself) and KISS (Keep It Simple, Stupid) principles


## Main resources used:
- [Udemy Course: ASP.NET Core Deep-Dive by Frank Liu](https://www.udemy.com/course/aspnet-core-deep-dive) — Also taught Postman fundamentals
- Microsoft Documentation
- Community discussions on Discord

## Packages Used
| Package | Version |
|---------|---------|
| Microsoft.AspNetCore.OpenApi | 9.0.0 |
| Microsoft.EntityFrameworkCore.SqlServer | 9.0.4 |
| Swashbuckle.AspNetCore | 8.1.1 |
| Microsoft.EntityFrameworkCore.Tools | 9.0.4 |
| Microsoft.EntityFrameworkCore.Design | 9.0.4 |
| Spectre.Console | 0.50.0 |
| RestSharp | 112.1.0 | 
