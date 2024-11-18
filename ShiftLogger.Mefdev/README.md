# Project Name

## Overview
ShiftLogger is a comprehensive tool designed to streamline shift management within organizations. The project consists of two main components: the **UI Application** and the **API Application**. The UI Application provides an intuitive interface for users to log their shifts, while the API Application handles data processing, storage, and business logic.

## Applications

### UI Application
The UI Application offers a user-friendly interface for managing work shifts.

#### Features
- **Shift Logging:** Users can easily log the start and end times of their shifts.

#### Installation
1. **Clone the Repository:**
    ```bash
    git clone https://github.com/mefdev/CodeReviews.Console.ShiftsLogger.git
    ```
2. **Navigate to the UI Directory:**
    ```bash
    cd ShiftLogger.Mefdev/ShiftLoggerUI
    ```
   ```dotnet
    dotnet run
   ```

### API Application
The API Application serves as the backend for ShiftLogger, handling data storage, retrieval, and business logic. It exposes RESTful endpoints that the UI Application interacts with.

#### Features
- **RESTful Endpoints:** Provides API endpoints for managing shifts, users, and reports.
- **Database Integration:** Connects to a SQL database server for persistent data storage.
- **Data Validation:** Ensures all incoming data meets predefined standards.

#### Installation
1. **Clone the Repository:**
    ```bash
    git clone https://github.com/mefdev/CodeReviews.Console.ShiftsLogger.git
    ```
2. **Navigate to the API Directory:**
    ```bash
    cd ShiftsLogger.Mefdev/ShiftLoggerAPI
    ```
3. **Install Dependencies:**
    ```bash
    dotnet restore
    ```
4. **Configure Environment Variables:**
    - Create a `.env` file based on the `.env.example` template.
    - Set up your database connection strings and other necessary configurations.
5. **Apply Database Migrations:**
    ```bash
    dotnet ef database update
    ```
6. **Run the API:**
    ```bash
    dotnet run --launch-profile https
    ```
7. **Access the API Documentation:**
    Navigate to `https://localhost:7263/swagger` to view the API documentation and test endpoints.

## How It Works
1. **Data Handling:** The UI sends requests to the API Application, which processes these requests, interacts with the database, and returns the necessary data.
2. **Data Storage:** All shift data is securely stored in the configured database.

