# 📝 Shifts Logger

Shifts Logger is a console-based application that allows users to manage workers and their work shifts through a simple menu-driven interface.
It interacts with a Web API backend to perform CRUD operations on workers and shifts.

## 🚀 Features
- View, add, update, and delete workers.
- View, add, update, and delete shifts.
- User-friendly console menu for easy interaction.
- Validations for input data.

## 🏗️ Tech Stack
- **Backend:** ASP.NET Core Web API (.NET 8), Entity Framework Core, SQL Server
- **Frontend:** Console Application (C#)

## 📜 API Endpoints
### Workers
- `GET /workers` - Get all workers
- `GET /workers/{id}` - Get a single worker by ID
- `POST /workers` - Add a new worker
- `PUT /workers/{id}` - Update worker details
- `DELETE /workers/{id}` - Remove a worker

### Shifts
- `GET /shifts` - Get all shifts
- `GET /shifts/{id}` - Get a single shift by ID
- `POST /shifts` - Add a new shift
- `PUT /shifts/{id}` - Update shift details
- `DELETE /shifts/{id}` - Remove a shift

## 🔧 Setup & Installation
### 1️⃣ Clone the Repository
```sh
git clone https://github.com/Eyad-Mostafa/ShiftsLogger.git
cd ShiftsLogger
```

### 2️⃣ Backend Setup
1. Navigate to the API project folder:
   ```sh
   cd ShiftsLoggerAPI
   ```
2. Restore dependencies and build the project:
   ```sh
   dotnet restore
   dotnet build
   ```
3. Apply database migrations:
   ```sh
   dotnet ef database update
   ```
4. Run the API:
   ```sh
   dotnet run
   ```

### 3️⃣ Frontend Setup
1. Navigate to the UI project folder:
   ```sh
   cd ShiftsLoggerUI
   ```
2. Run the console application:
   ```sh
   dotnet run
   ```

## 🖥️ Usage
Once the console application is running, use the menu options to manage workers and shifts.
```
--- Shifts Logger Menu ---
1. View Workers
2. Add Worker
3. Delete Worker
4. View Shifts
5. Add Shift
6. Update Shift
7. Delete Shift
8. Exit
Choose an option:
```

---

Enjoy coding! 💻🔥

