<h1>Shifts Logger</h1>
<subtitle>The C# Academy</subtitle>
<subtitle>Thomas Henley</subtitle>


<h2>Usage</h2>

<h4>To start the API:</h4>

1. Navigate to the ShiftsLoggerAPI project root and `Update-Database`.
2. Enter `dotnet run` to start the API. By default, the server should open an HTTPS socket on port `7101`.
3. You can now see the swagger spec at <a>https://localhost:7101/swagger/index.html</a>.

<h4>To start the Shifts Manager client:</h4>

1. Navigate to the `ShiftsLoggerCLI` project root and enter `dotnet run`.
2. By default, the client will attempt to reach the API on localhost port `7101`. The port can be changed in `appsettings.json`.
3. Follow the prompts to use the command line interface.


<h2>Notes</h2>
<p>The solution is split into several projects:</p>

- `ShiftsLoggerAPI`: Contains the server code to run the API.
- `ShiftsLoggerCLI`: Contains the command line client tool for employees to log their shifts.
- `ShiftsLoggerModels`: Contains shared model types to be used by the API, CLI, and any other interfaces that may be developed.
- `ShiftsLoggerClientLibrary`: Contains `IShiftApiClient` and types that implement it, to be used in the CLI and any other interfaces that may be developed. These types manage the actual requests to the REST API. I separated this because I was considering writing multiple versions of the client with different REST frameworks (Flurl, RestEase), but ended up deciding that I'm happy just with the native HttpClient.
