using Buutyful.ShiftsLogger.Ui;

var client = new ApiHttpClient();
await Task.Delay(2000);
var workers = await client.GetWorkersAsync();
foreach (var worker in workers) Console.WriteLine(worker.ToString());

