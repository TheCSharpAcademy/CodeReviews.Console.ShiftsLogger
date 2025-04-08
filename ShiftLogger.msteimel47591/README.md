# ShiftLogger

This is a C# application for The C# Academy roadmap. It contains a minimal 
Web API that performs CRUD operations on a SQL database and a user interface 
that interacts with the API.

# Project Requirements

To meet the requirements specified by The C# Academy, the project must do 
the following:
- This is an application where you should record a worker's shifts.
- You need to create two applications: the Web API and the UI that will 
  call it.
- All validation and user input should happen in the UI app.
- Your API's controller should be lean. Any logic should be handled in a 
  separate "service".
- You should use SQL Server, not SQLite.
- You should use the "code first" approach to create your database, using 
  Entity Framework's migrations tool.
- Your front-end project needs to have try-catch blocks around the API calls 
  so it handles unexpected errors (i.e. the API isn't running or returns a 
  500 error).

# Notes

One annoying thing about this project is that when it launches, it launches 
two console windows. One is for the user interface and the other is for the 
API, and when you close one window it doesn't close the other. I'm sure 
there's a way to change that but I got tired of messing with it so I just 
moved on to the project requirements.