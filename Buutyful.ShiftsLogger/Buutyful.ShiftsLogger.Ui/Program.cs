using Buutyful.ShiftsLogger.Domain.Contracts.Shift;
using Buutyful.ShiftsLogger.Domain.Contracts.WorkerContracts;
using Buutyful.ShiftsLogger.Ui;
using Spectre.Console;

var client = new ApiHttpClient();


AnsiConsole.MarkupLine("[yellow]api/workers docs[/]");
var table = new Table();

table.AddColumn("HttpVerb");
table.AddColumn("EndPoint");
table.AddColumn("Description");

table.AddRow("GET", "api/workers", "get all workers");
table.AddRow("GET", "api/workers/{id}", "get worker by id");
table.AddRow("POST", "api/workers", $"Create wroker: body payload of type {nameof(CreateWorkerRequest)}");
table.AddRow("PUT", "api/workers", $"Update wroker: body payload of type {nameof(UpsertWorkerRequest)}");
table.AddRow("DELETE", "api/workers/{id}", "Delete wroker by id");

AnsiConsole.Write(table);
Console.WriteLine("");
AnsiConsole.MarkupLine("[yellow]Usage example api/workers[/]");
var workers = await client.GetWorkersAsync();
foreach (var worker in workers) Console.WriteLine(worker.ToString());

AnsiConsole.MarkupLine("[yellow]press a key to continue[/]");
Console.ReadLine();
Console.Clear();

AnsiConsole.MarkupLine("[yellow]api/shifts docs[/]");
var table2 = new Table();

table2.AddColumn("HttpVerb");
table2.AddColumn("EndPoint");
table2.AddColumn("Description");
     
table2.AddRow("GET", "api/shifts", "get all shifts");
table2.AddRow("GET", "api/shifts/{id}", "get shift by id");
table2.AddRow("POST", "api/shifts", $"Create shift: body payload of type {nameof(CreateShiftRequest)}");
table2.AddRow("DELETE", "api/shifts/{id}", "Delete shift by id");

AnsiConsole.Write(table2);

AnsiConsole.MarkupLine("[yellow]Usage example api/shifts[/]");
var shifts = await client.GetShiftsAsync();
foreach (var shift in shifts) Console.WriteLine(shift.ToString());

AnsiConsole.MarkupLine("[yellow]presentetion over[/]");
Console.ReadLine();
