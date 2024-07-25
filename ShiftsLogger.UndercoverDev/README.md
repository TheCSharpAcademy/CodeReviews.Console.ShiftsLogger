# WorkerShifts Project

## Overview

WorkerShifts is a comprehensive solution for managing worker shifts. It consists of two main components:

1. WorkerShiftsAPI: A .NET Core Web API backend
2. WorkerShiftsUI: A frontend application (likely a web or desktop app)

This project allows you to create, read, update, and delete workers and their shifts, providing a robust system for shift management.

## Table of Contents

1. [Features](#features)
2. [Prerequisites](#prerequisites)
3. [Installation](#installation)
4. [Running the Project](#running-the-project)
5. [API Endpoints](#api-endpoints)
6. [Database](#database)
7. [Frontend](#frontend)
8. [Contributing](#contributing)
9. [License](#license)

## Features

- Manage workers (CRUD operations)
- Manage shifts (CRUD operations)
- RESTful API
- Entity Framework Core for database operations
- Swagger UI for API documentation and testing
- Frontend application for easy interaction with the API

## Prerequisites

- .NET 8.0 SDK
- SQL Server
- Visual Studio 2022 or Visual Studio Code
- (Frontend requirements - uses console for interaction)

## Installation

1. Clone the repository:
`git clone https://github.com/N-Endy/WorkerShifts.git`

2. Navigate to the project directory:
`cd WorkerShifts`

3. Restore the NuGet packages:
`dotnet restore`

4. Update the connection string in `appsettings.json` in the WorkerShiftsAPI project to point to your SQL Server instance.

5. Apply the database migrations:
`dotnet ef database update`

## Running the Project

1. Start the API:
`cd WorkerShiftsAPI`
`dotnet run --launch-profile https` or `dotnet run`
The API will be available at `https://localhost:7267`.

2. Start the UI application 
`cd WorkerShiftsUI`
`dotnet run`

## API Endpoints

The API provides the following endpoints:

### Workers

- GET `/api/workers`: Get all workers
- GET `/api/workers/{id}`: Get a specific worker
- POST `/api/workers`: Create a new worker
- PUT `/api/workers/{id}`: Update a worker
- DELETE `/api/workers/{id}`: Delete a worker

### Shifts

- GET `/api/shifts`: Get all shifts
- GET `/api/shifts/{id}`: Get a specific shift
- POST `/api/shifts`: Create a new shift
- PUT `/api/shifts/{id}`: Update a shift
- DELETE `/api/shifts/{id}`: Delete a shift

For detailed API documentation, run the project and navigate to `https://localhost:7267/swagger`.

## Database

The project uses Entity Framework Core with SQL Server. The database schema includes two main entities:

1. Worker
- WorkerId (int, primary key)
- Name (string, required)

2. Shift
- ShiftId (int, primary key)
- StartTime (DateTime, required)
- EndTime (DateTime, required)
- WorkerId (int, foreign key to Worker)

## Frontend

The frontend application is located in the WorkerShiftsUI directory. It provides a user-friendly interface for interacting with the API. (This project currently uses the console for interaction)

## Error Handling

The project uses a custom ApiResponseHandler in the UI to manage API responses consistently. This ensures that errors from the API are properly caught and handled in the frontend application.

## Data Transfer Objects (DTOs)

The API uses DTOs for data validation and transformation. This approach helps in separating the internal data model from the data exposed via the API, providing an additional layer of security and flexibility.

## Dependency Injection

The API utilizes dependency injection, particularly for the WorkerShiftContext. This is configured in the Program.cs file, allowing for easier testing and maintenance of the application.


## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## License

This project is licensed under the MIT License.
