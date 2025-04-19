namespace Ui;

using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        WorkerService workerService = new();
        workerService.ConnectApi();

        ShiftService shiftService = new(await workerService.GetWorkerByIdAsync(2));
        shiftService.ConnectApi();

        var output = await shiftService.GetAllShiftsByWorkerIdAsync();
        DisplayTable.Shift(output);


        // var options = new RestClientOptions("http://localhost:5295/api/");
        // var client = new RestClient(options);
        // var request = new RestRequest("Workers/")
        //     .AddJsonBody
        //     (
        //         JsonSerializer.Serialize
        //         (
        //             new Worker()
        //             {
        //                 EmployeeName = "test",
        //                 EmployeeId = 99,
        //             }
        //         )
        //     );

        // var response = await client.PostAsync(request);

        // Console.Write(response.Content);
    }
}
