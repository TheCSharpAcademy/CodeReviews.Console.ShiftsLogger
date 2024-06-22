# ShiftsLogger UI

## Table of Contents
- [Introduction](#introduction)
- [Features](#features)
- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Usage](#usage)
- [Configuration](#Configuration)
- [Technologies Used](#technologies-used)

## Introduction
ShiftsLogger UI is a console application that interacts with the ShiftsLogger API to manage work shifts.

## Features
- Start a new shift
- End a shift
- List all shifts
- Update a shift
- Delete a shift

## Prerequisites
- [.NET SDK 8.0](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Spectre.Console](https://spectreconsole.net/)

## Installation
1. **Clone the repository**
2. **Run the UI application**

## Usage

* Start Shift: Start a new shift by entering the employee's name.
* End Shift: End an ongoing shift by entering the shift ID.
* List Shifts: Display all shifts logged in the system.
* Update Shift: Update the details of an existing shift.
* Delete Shift: Remove a shift from the system.

## Configuration

By default, the UI is configured to communicate with the API at https://localhost:44310/api/. If your API is running on 
a different port or URL, you need to update the BaseAddress in the Program.cs file:

=> private static readonly HttpClient client = new HttpClient { BaseAddress = new Uri("https://localhost:44310/api/") }; <=

Change the BaseAddress URI to match your API's address and port.

## Technologies Used

* .NET Core
* Spectre.Console