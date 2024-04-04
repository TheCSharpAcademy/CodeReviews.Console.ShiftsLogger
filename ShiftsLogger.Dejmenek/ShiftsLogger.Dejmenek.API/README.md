# ShiftsLogger API

## Table of Contents
- [General Info](#general-info)
- [Technologies](#technologies)
- [API Endpoints](#api-endpoints)
- [Status Codes](#status-codes)

## General Info
The ShiftsLogger API provides functionalities to manage both shift data and employee information.  
You can create, read, update, and delete shifts and employees.

## Technologies
- C#
- SQL Server
- Entity Framework Core
- ASP.NET Core Web API

## API Endpoints
| HTTP Verbs | Endpoints | Action |
| --- | --- | --- |
| GET | /api/employees | To retrieve all employees |
| GET | /api/employees/:employeeId/shifts | To retrieve employee's shifts |
| GET | /api/shifts | To retrieve all shifts |
| POST | /api/employees | To create a new employee |
| POST | /api/shifts | To create a new shift |
| PUT | /api/employees/:employeeId | To edit the details of a single employee |
| PUT | /api/shifts/:shiftId | To edit the details of a single shift |
| DELETE | /api/employees/:employeeId | To delete a single employee |
| DELETE | /api/shifts/:shiftId | To delete a single shift |

## Status Codes
 | Status Code | Description |
 | --- | --- |
 | 200 | OK |
 | 201 | CREATED |
 | 400 | BAD REQUEST |
 | 404 | NOT FOUND |
 | 500 | INTERNAL SERVER ERROR |