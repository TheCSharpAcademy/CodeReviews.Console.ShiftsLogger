# Shifts Logger Application

The **Shifts Logger Application** is a powerful C# .NET application designed to help users track their work shifts efficiently. It leverages **Entity Framework Core** for database operations and uses **SQL Server** as the backend database. The application allows to create an account for users who do not exist in the system, ensuring they can start managing their shifts seamlessly.

## Features

- **User Account Creation**: If a user doesn't exist in the database, the application lets user create an account for them.
- **CRUD Operations on Shifts**:
    - **Create**: Add new shifts for work.
    - **Read**: View all logged shifts.
    - **Update**: Edit details of existing shifts.
    - **Delete**: Remove unwanted or incorrect shift entries.
- **CRUD Operations on Workers**:
    - **Create**: Add new workers.
    - **Read**: View all workers.
    - **Update**: Edit details of existing workers.
    - **Delete**: Remove unwanted or incorrect workers.
- **Entity Framework Core**: Provides a robust and scalable data access layer.
- **SQL Server Integration**: Ensures secure and reliable shift data storage.

## Getting Started

### Prerequisites

To run the Shifts Logger Application, ensure you have the following installed:

- [.NET SDK](https://dotnet.microsoft.com/download) (v6.0 or later)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- A code editor like [Visual Studio](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/)

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/KamilKolanowski/CodeReviews.Console.ShiftsLogger.git
   ```
2. Navigate to the project directory:
   ```bash
   cd ShiftsLogger.KamilKolanowski
   ```
3. Restore dependencies:
   ```bash
   dotnet restore
   ```

## Configuration

### Setting Up the Database

1. Update the connection string in the `appsettings.json` file:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=<YourServerName>;Database=ShiftsLoggerDb;Trusted_Connection=True;MultipleActiveResultSets=true"
     }
   }
   ```
   Replace `<YourServerName>` with the name of your SQL Server instance.

2. Apply database migrations to initialize the database:
   ```bash
   dotnet ef database update
   ```

## Usage

### Running the Application

Start the application:
```bash
dotnet run
```
The API will be accessible at `https://localhost:5000`.

### API Endpoints

#### Authentication
- **Create/Check User**: `POST /api/workers`
    - Request Body: `{ "FirstName": "John", "LastName": "Doe", "Role": "Programmer" }`

#### Shifts
- **Create Shift**: `POST /api/shifts`
    - Request Body: `{ "StartTime": "2025-01-01 08:00:00", "EndTime": "2025-01-01 16:00:00" }`
- **Read Shifts**: `GET /api/shifts`
- **Read Shift By Id**: `GET /api/shifts/{id}`
- **Update Shift**: `PUT /api/shifts/{id}`
    - Request Body: `{ "StartTime": "2025-01-01 08:00:00", "EndTime": "2025-01-01 16:00:00" }`
- **Delete Shift**: `DELETE /api/shifts/{id}`

#### Workers
- **Create Shift**: `POST /api/workers`
    - Request Body: `{ "FirstName": "John", "LastName": "Doe", "Role": "Programmer" }`
- **Read Shifts**: `GET /api/shifts`
- **Read Shift By Id**: `GET /api/shifts/{id}`
- **Update Shift**: `PUT /api/shifts/{id}`
    - Request Body: `{ "StartTime": "2025-01-01 08:00:00", "EndTime": "2025-01-01 16:00:00" }`
- **Delete Shift**: `DELETE /api/shifts/{id}`

*Email is automatically created when adding new user (firstName.lastName@thecsharpacademy.com), or it can be custom, when added from shift logging view, if user doesn't exists*

## Technologies Used

- **Framework**: .NET (C#)
- **Database**: SQL Server
- **ORM**: Entity Framework Core
- **Authentication**: Custom user management logic
- **Environment Configuration**: `appsettings.json`

## Contributing

If you want to contribute, please follow the steps below!

1. Fork the repository.
2. Create a new branch:
   ```bash
   git checkout -b feature/your-feature-name
   ```
3. Commit your changes:
   ```bash
   git commit -m "Add your message here"
   ```
4. Push to the branch:
   ```bash
   git push origin feature/your-feature-name
   ```
5. Open a pull request.

## Contact

For any questions or suggestions, feel free to reach out:

**Author**: Kamil Kolanowski  
**GitHub**: [KamilKolanowski](https://github.com/KamilKolanowski)
