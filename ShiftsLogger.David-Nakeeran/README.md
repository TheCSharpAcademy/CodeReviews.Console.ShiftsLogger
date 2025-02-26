## Shifts Logger

A console based shift management system that allows users to log and manage employee shifts.

## Overview

This project consists of two applications:

- ShiftsLoggerAPI – A Web API that interacts with a database to store and retrieve shift data.

- ShiftsLoggerClient – A console-based client that allows users to send requests to the API and perform CRUD operations.

## How to run application

- Ensure you have .NET SDK latest version

```
git clone https://github.com/your-username/shifts-logger.git
cd shifts-logger
```

- Database set up
  Before running the application for the first time, you need to create a database and apply migrations

1. Ensure SQL Server is running
2. Apply migrations by running the step below, in project directory via terminal

```
dotnet ef database update
```

- Set up User Secrets

The application uses User Secrets to store the database connection string securely. Set up your connection string using the following commands:

```
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost;Database=ShiftsLoggerDB;User=sa;Password=YOURPASSWORD;Encrypt=True;TrustServerCertificate=True"

```

- Run Application

- Open a terminal or command prompt into the project directory.
  Start API first:

```
cd ShiftsLoggerAPI
dotnet run

```

Then, in a second terminal, start the client:

```
cd ShiftsLoggerClient
dotnet run
```

Ensure both the API and client applications are running simultaneously for full functionality

## Requirements

1. **Shifts Logger**:

- This is an application where you should record a worker's shifts.

- You need to create two applications: the Web API and the UI that will call it.

- All validation and user input should happen in the UI app.

- Your API's controller should be lean. Any logic should be handled in a separate "service".

- You should use SQL Server, not SQLite

- You should use the "code first" approach to create your database, using Entity Framework's migrations tool.

2. **Error Handling**:

   - Able to handle all possible errors so that the application never crashes.

3. **Follow DRY Principle**:

   - Avoid code repetition.

4. **Separation of Concerns**:

   - Object-Oriented Programming

## Features

- User Secrets

  - Securely store database connection strings.

- Repository Pattern:

  - Decouples data access logic from business logic, improving maintainability
