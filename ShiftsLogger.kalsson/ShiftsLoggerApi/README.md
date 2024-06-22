# ShiftsLogger API

## Table of Contents
- [Introduction](#introduction)
- [Features](#features)
- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Usage](#usage)
- [API Endpoints](#api-endpoints)
- [Technologies Used](#technologies-used)

## Introduction
ShiftsLogger API is a backend service built with ASP.NET Core to manage work shifts.

## Features
- Create a new shift
- Retrieve shifts
- Update a shift
- Delete a shift

## Prerequisites
- [.NET SDK 8.0](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQL Server or SQL Server Express

## Installation
1. **Clone the repository**
2. **Set up the database**
* Ensure SQL Server is running.
* Update the connection string in appsettings.json.
3. **Run database migrations**
4. **Run the API**

## Usage

* Use a tool like Postman to test the API endpoints.

## API Endpoints

* GET /api/Shift: Retrieve all shifts.
* GET /api/Shift/{id}: Retrieve a specific shift by ID.
* POST /api/Shift: Create a new shift.
* PUT /api/Shift/{id}: Update an existing shift.
* DELETE /api/Shift/{id}: Delete a shift.

## Technologies Used

* ASP.NET Core
* Entity Framework Core
* SQL Server