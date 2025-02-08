# Console Shift Logger
## Description
This program allows you to store Employees and their Shifts.
## Employees
Employees were added to separate shifts.
## Database
For the program to work, a database must be created in SQL Server. In this case, Windows credentials were used.
## Configuration
The person who wants to test or use this program must modify the ConnectionString in appsettings.json and adapt it to their SQLServer configuration.
## Problems
The checker doesn't like the Readme.md file. I had to rename it to Readme.txt.
I had a lot of problems between models and Swagger. I solved it by using models for the database and DTOs for Swagger.
## Limitations
Only the last 10 shifts are displayed.
There is no daily hour limit.
Only the last record can be deleted or edited.
## Improvements
Select a month and year to display a summary of Shifs.
Improve the way it is displayed.
