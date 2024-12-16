## Shifts Logger

# Overview

Shifts Logger is a console application that performs CRUD (Create, Read, Update, Delete) operations for managing shifts. The application interacts with a backend API built using **C#**, **Entity Framework**, and a database. The frontend (UI) is built using **Spectre.Console** to provide a rich, interactive console experience.

## Features

# API
- The API handles communication with the database and performs CRUD operations on shift records.
- Built using **ASP.NET Core** with **Entity Framework** to interact with the database.
- Provides endpoints for managing shifts:
  - **GET** `/api/shifts`: Retrieve all shifts.
  - **GET** `/api/shifts/{id}`: Retrieve a specific shift by ID.
  - **POST** `/api/shifts`: Create a new shift.
  - **PUT** `/api/shifts/{id}`: Update an existing shift.
  - **DELETE** `/api/shifts/{id}`: Delete a shift by ID.

# UI
- The user interface is built using **Spectre.Console**, a library for creating beautiful and interactive console applications.
- The UI allows users to:
  - View all shifts.
  - Select and update shifts interactively.
  - Delete shifts.
  - Choose options like updating the shift name, start time, or end time via a multi-selection prompt.

## Technologies used:

# Backend
- **ASP.NET Core**: A cross-platform framework for building modern, cloud-based, and internet-connected applications. It is used to create the API that communicates with the database and handles CRUD operations for shifts.
- **Entity Framework Core**: An object-relational mapper (ORM) for .NET. It is used to interact with the database, allowing you to query and manipulate data in an object-oriented way.
- **SQL Server**: A relational database management system used to store shift data. It can be replaced with any other database that supports Entity Framework Core.

# Frontend (UI)
- **Spectre.Console**: A library for building rich and interactive console applications. It is used to create the user interface in the console, providing a better user experience with features like multi-selection prompts, tables, and colored text.

# Development Tools
- **.NET SDK**: The software development kit required to build and run the application. It includes the .NET runtime, libraries, and tools for building .NET applications.
- **Visual Studio 2022: Integrated development environment (IDE) used for writing and debugging the code.

# Others
- **Swagger**: A tool used for API documentation. It can be integrated with ASP.NET Core to provide a web-based interface for testing the API endpoints (optional, but useful for API development).
- **Postman**: A tool for testing API endpoints. You can use Postman to manually test the API by sending requests and inspecting responses.

## Challenges:

- It was hard at first to understand how API works but i got used it

