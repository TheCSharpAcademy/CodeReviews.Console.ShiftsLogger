# Shifts Logger By AhmadJer99 
This Shifts Logger application saves the Start and End shift time of the Employee along with employee name in the SqlServer database which is connected via Entity FrameworkCore ORM. 
This Console app uses WebApi to perform CRUD operation on the database.

## Features
- Uses (WebApi/Entity Framework) stack.
- In this app you can add employee and add their respective shifts, and track them seamlessly.
- Users are able to do basic CRUD operations on employee and shifts using a console UI.
- The database is done using the code-first approach.
- Added a seed database option in the ui to easily fill the database with some data for testing purposes.
- Data is presented beautifully using spectre.console and in tables using consoletablext package.


## Deployment
To deploy this project
- make sure to replace the api url in the appsettings of the ConsoleUI app with your appropriate link
- Make sure to replace connection string in the API appsettings with your database connection string

