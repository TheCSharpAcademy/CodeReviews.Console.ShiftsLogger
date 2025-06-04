# Getting Started

> [!NOTE]
> The `InitialCreate` migration was created.
>
> It will be applied on startup of the API application and create the database and tables.

## Prerequisites

- .NET 9 SDK
- A code editor like Visual Studio or Visual Studio Code
- SQL Server
- SQL Server Management Studio (optional)

## Installation

1. Clone the repository
   - `https://github.com/nwdorian/ShiftsLogger.git`
2. Configure the appsettings.json
   - Update the connection string
3. Navigate to the API project folder
   - `cd .\WebApi`
4. Build the Web API application using .NET CLI
   - `dotnet build`
5. Navigate to the Console project folder
   - `cd .\ConsoleUI`
6. Build the Console application using the .NET CLI
   - `dotnet build`

## Running the application

1. Run the API application from the API project folder using the .NET CLI
    - `cd .\WebApi\ShiftsLogger.WebApi`
    - `dotnet run`
2. Run the Console application from the Console project folder using the .NET CLI
    - `cd .\ConsoleUI\ShiftsLogger.ConsoleUI`
    - `dotnet run`
