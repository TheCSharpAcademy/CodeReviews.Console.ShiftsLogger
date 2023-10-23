# Shift Logger


Console based CRUD application to manage shifts.
The client connects to a Web API.
Developed using C#, .NET, Entity Framework and SQL Server.


# Given Requirements:
- [x] This is an application where you should record a worker's shifts.
- [x] You need to create two applications: the Web API and the UI that will call it.
- [x] All validation and user input should happen in the UI app.
- [x] Your API's controller should be lean. Any logic should be handled in a separate "service".
- [x] You should use SQL Server, not SQLite
- [x] You should use the "code first" approach to create your database, using Entity Framework's migrations tool.
- [x] Your front-end project needs to have try-catch blocks around the API calls so it handles unexpected errors (i.e. the API isn't running or returns a 500 error.)



# Features

* SQL Server database connection

	- The program uses a SQL Server db connection to store and read log information. 

* A console based UI where users can navigate by key presses.
 

* CRUD DB functions

	- From the main menu users can Create, Read, Update or Delete shift entries.




# Challenges
	
- How to set up and structure the project with good code and folder structure/organization.
- How to handle validation and user input.
- How to send http requests from client to the Web API.
	
# Lessons Learned
- Before starting to code, plan the project by writing down entities and methods. Create a basic flowchart of how the program flows. 
- Create a new git branch for each new feature. 
- Seperate the code with classes and methods.

# Areas to Improve
- I want to improve the code by reading up on seperation of conerns.
- I Want to learn more about testing, i.e Unit testing. 


# Resources Used
- Google
- Stackoverflow
- Microsoft documentation
