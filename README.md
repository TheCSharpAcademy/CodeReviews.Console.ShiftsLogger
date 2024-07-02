
# Shifts Logger Project

## Overview

This project implements a Shifts Logger system with a Web API backend and a Console UI frontend. It allows for managing employees and their work shifts, providing CRUD operations for both entities.

## Project Structure

The solution consists of three main parts:
1. Shared Library (SharedLibrary)
2. Web API (ShiftsLoggerAPI)
3. Console UI (ShiftLoggerUI)

### Shared Library

Contains shared models, DTOs, and validation logic used by both the API and UI projects.

### Web API

Implements the backend services using ASP.NET Core Web API with Entity Framework Core for data access.

### Console UI

A console application that interacts with the Web API, providing a user interface for managing employees and shifts.

## Setup and Configuration

1. Database Setup:
   - The project uses Entity Framework Core with SQL Server.
   - Update the connection string in `appsettings.json` of the API project.
   - Run EF Core migrations to create the database:
     ```
     dotnet ef database update
     ```

2. Starting the Projects:
   - Set both ShiftsLoggerAPI and ShiftLoggerUI as startup projects.

3. API Base URL:
   - The UI project is configured to use `https://localhost:7045` as the API base URL.
   - If needed, update this in the `App` constructor in the UI project.

## Key Features

- CRUD operations for Employees and Shifts
- Validation logic for both Employees and Shifts
- Console-based user interface with menu-driven operations
- Use of AutoMapper for object mapping
- Implementation of the Repository pattern and Service layer in the API

## Areas for Improvement

1. Validation Approach:
   - The current use of exceptions for validation in the shared library is not ideal.
   - Implement a more robust validation strategy, possibly using FluentValidation.

2. Business Logic:
   - Expand business validations to cover more edge cases and scenarios.

3. UI Layer Structure:
   - Separate display logic from data retrieval in methods like `GetEmployeeById`, `GetAllEmployees`, etc.
   - Implement distinct sections for Employee and Shift operations in the menu system.

4. Enhanced Shift Functionality:
   - Add more filtering options and advanced functionalities for shift management.

5. Configuration Management:
   - Move the API base URL in the UI project to a configuration file.

## Conclusion

This project serves as a good foundation for a Shifts Logger system. While functional, there are several areas where it can be enhanced to make it more robust, maintainable, and feature-rich.
