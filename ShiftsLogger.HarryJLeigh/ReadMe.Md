Shifts Logger Application

----

Overview
---

Shifts Logger is a C# and .NET application designed to record workers' shifts. It includes a backend API that interacts with SQL Server, utilizing shift service class functions to update the database and provide HTTP responses. The frontend UI is built to streamline user interactions and ensure input validation.

The project is divided into three main components:

1. Shared: Contains reusable code, such as data models
2. ShiftsLoggerAPI: The Web API that provides access to shift management functionality.
3. ShiftsLoggerUI: The frontend user interface that interacts with the backend API.
---

**IMPORTANT NOTE:** To run the API in ShiftsLoggerAPI project, locate the appsettings.json file and update the DefaultConnection in the ConnectionStrings section to point to your SQL Server. Then, create a database named ShiftsDb in SQL Server.

- To run the entire application, start the ShiftsLoggerAPI first, then run the ShiftsLoggerUI to view the console.
---

## Requirements

---

- You need to create two applications: the Web API and the UI that will call it.
- All validation and user input should happen in the UI app.
- Your API's controller should be lean. Any logic should be handled in a separate "service".
- You should use SQL Server, not SQLite
- You should use the "code first" approach to create your database, using Entity Framework's migrations tool.
- Your front-end project needs to have try-catch blocks around the API calls so it handles unexpected errors (i.e. the API isn't running or returns a 500 error.)

## Features

---
###  1. Shift recording:

- Record workers shifts by with details start time, end time and shift duration
- Shift duration is calculated automatically 
- Create, update, or delete records

### 2. Backend API (ShiftsLoggerAPI) with RESTful Design:
- Provides an API for creating, reading, updating, and deleting shifts.
- Supports standard RESTful HTTP operations: GET, POST, PUT, DELETE.
- Uses Entity Framework for data management and SQL Server for data storage.
- RestSharp simplifies HTTP client requests, enabling integration between UI and API.

### 3. Frontend UI (ShiftsLoggerUI):
- Text-based UI built using Spectre Console for an intuitive user experience.
- Supports input validation, making sure all data entries are correct and complete.
- Allows users to view, add, edit, and delete shifts through a simple menu.

## Lessons Learned

---

### 1. Separation of Concerns:
- How to divide a project into components (Shared, ShiftsLoggerAPI, ShiftsLoggerUI).
### 2. API Design
- How to design a RESTful API with consistent HTTP methods. Get, Post, Put and Delete.
### 3. Entity Framework Efficiency:
- Using Entity Framework reduced boilerplate code and streamlined database operations.
### 4. Input Validation for Data Integrity:
- Thorough validation at the UI level ensured accurate data entry.
### 5. Error Handling Using Try-Catch:
- Implementing try and catch blocks prevented application crashes and provided user-friendly feedback.
### 6. API Testing with Swagger and Postman:
- Swagger helped document and test endpoints, while Postman provided detailed testing, resulting in a reliable API.


## Challenges Faced

---
### 1. Managing API/UI Integration:
- Integrating the frontend UI with the backend API was challenging, particularly ensuring that the API calls were accurate and that the data was handled properly throughout the process.
### 2. Handling Unexpected Errors:
- Researched the use of try-catch blocks to prevent application crashes and provide meaningful feedback to users.
### 3. Working with Separate Applications in One Solution
- Managing multiple applications within a single solution was challenging, particularly in maintaining clear separation and ensuring the correct folder architecture was used.