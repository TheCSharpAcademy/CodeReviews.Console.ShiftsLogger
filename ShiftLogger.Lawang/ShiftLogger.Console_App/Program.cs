using ShiftLogger.Console_App;

var userInput = new UserInput();

string url = "http://localhost:5008/api/shifts";
var shiftService = new ShiftService(url);
var app = new Application(userInput, shiftService);
await app.Build();