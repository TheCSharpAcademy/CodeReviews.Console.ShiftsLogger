# Shifts Logger

Full stack employee shift logging application.

## Features

- Manage Employees & their associated shifts
  - Create, list, update, delete employees
  - For these employees, create, list, update, delete shifts.
- Search & modify all shifts
  - View all shifts for all employees in one place. Type to search.

## Run locally

> Note: If any configuration is changed,
> the respective app must be restarted for the config to apply.
> Even though HTTPS should be used, for the ease
> of running this locally the instructions below
> will start an HTTP server.

### 1. Run the API server (backend)

#### Pre-requisites & notes for running server

- SQL server local DB must be installed and running
- Configure DB connection details in `ShiftsLoggerApi/appsettings.json` if you want
  (default settings should be fine)

#### Steps to run server locally

1. `cd <Repo_root>/ShiftsLoggerApi`
2. `dotnet ef database update` to apply database migrations
3. `dotnet run`
4. Server should start running at `http://localhost:5026`
   - Configurable via `ShiftsLoggerApi/Properties/launchSettings.json` if needed

#### 2. Start the console app (frontend)

#### Pre-requisites & notes to run console app

- API endpoint configurable in `ShiftsLoggerUi/appsettings.json` if desired.
  (default endpoint settings should be fine)

#### Steps to run console app

1. `cd <Repo_root>/ShiftsLoggerUi`
2. `dotnet run`

## Tech Stack

### ShiftLoggerApi (Backend)

- C# ASP.NET core Web API (Controllers)
- EF Core ORM (Code-first)

### ShiftsLoggerUi (Frontend)

- C# console app
