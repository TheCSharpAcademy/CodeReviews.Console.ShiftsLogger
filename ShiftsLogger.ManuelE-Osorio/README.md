# ShiftsLogger

The application is a shifts logger solution with two parts: a WebAPI and a Client app. 

# WEB API

The web api stores the data of the employees and their shifts in a SqlServer database and handles requests from the client app.

## Usage

The API has two controllers: EmployeesController ("/api/Employees") and ShiftsControllers ("/api/Shifts"). The Employees Controller is intented for administrators of the app and has the following methods:

1) Get (by name): The user can add a query string ("/api/Employees?name=Name) with the initial name to search for the employees name and check their information.
2) Get (by id): The user can add the id in the url ("/api/Employees/id") to search for the employee asigned to that id.
3) Put: This method is used in the creation of new employees. The body of the message has to contain the employee name and if its an admin.
4) Post: Used to modify an existing user. The body of the message has to contain the employee id, the name to modify and if its an admin.

The Shifts Controller is intended for normal users with the following methods:

1) Get: The user is able to find their latest 5 shifts in the database.
2) Put: This method is intended for the creation of a new shift. The API validates that all the previous shifts are finished before starting a new one.
3) Patch: This method in intended for finishing the current shift. The API validates that the existing shift hasn't been finished previosly.

## To Do

1) Implement authentication for Admin users.
2) Implement security measures for the API.
3) Increase validation for shifts: end shifts that are longer than x amount of hours, prevent the creation of more than x shifts per day, etc.

# Client APP

In the Client app the employees are able to enter their id, check their last 5 shifts, start a new one or end their current shift.

## Usage

When starting the app, the user is prompted for their login information (pending validation.) Depending on the user privileges they are presented with a User View or an Admin View.

### User View

The user is presented with the following options:

1) Start a new shift: This option starts a new shift at the current date and time.
2) Finish the current shift: This option finishes an existing shift at the current date and time.
3) Display your last 5 shifts.

### Admin View

The user is presented with the following options, aditional from the user view options:

4) Search employees by name: The user is able to search the database of the employees by the starting name and check their information.
5) Find employee by id: The user is able to get the detailed information of an employee by their id.
6) Add new employee: This method handles the creation of a new employee and assigns them privileges.
7) Modify a employee: This method is able to change the name of an employee and/or their privileges.

## To Do

1) Handle user login credentials.

## References

1) https://learn.microsoft.com/en-us/dotnet/framework/data/adonet/sql-server-data-type-mappings?redirectedfrom=MSDN
2) https://developer.mozilla.org/en-US/docs/Web/HTTP/Status
3) https://learn.microsoft.com/en-us/aspnet/web-api/overview/web-api-routing-and-actions/routing-in-aspnet-web-api
4) https://learn.microsoft.com/en-us/aspnet/web-api/overview/web-api-routing-and-actions/routing-and-action-selection
5) https://learn.microsoft.com/en-us/aspnet/core/web-api/jsonpatch?view=aspnetcore-8.0
6) https://stackoverflow.com/questions/64210290/how-to-construct-the-payload-for-web-api-patch-method
7) https://learn.microsoft.com/en-us/aspnet/core/fundamentals/environments?view=aspnetcore-8.0
8) https://stackoverflow.com/questions/17096201/build-query-string-for-system-net-httpclient-get/26744471#26744471
9) https://stackoverflow.com/questions/178456/what-is-the-proper-way-to-rethrow-an-exception-in-c
10) https://learn.microsoft.com/en-us/dotnet/csharp/advanced-topics/reflection-and-attributes/generics-and-reflection
11) https://stackoverflow.com/questions/59361548/how-to-build-all-c-sharp-projects-in-vs-code-using-only-one-command-in-tasks-jso
12) https://stackoverflow.com/questions/36343223/create-c-sharp-sln-file-with-visual-studio-code
13) https://learn.microsoft.com/en-us/visualstudio/extensibility/internals/solution-dot-sln-file?view=vs-2022

