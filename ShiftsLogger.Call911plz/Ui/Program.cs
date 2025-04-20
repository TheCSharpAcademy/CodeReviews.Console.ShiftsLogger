namespace Ui;

using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        // WorkerService workerService = new();
        // workerService.ConnectApi();

        // ShiftService shiftService = new(await workerService.GetWorkerByIdAsync(3));
        // shiftService.ConnectApi();

        // var output = await shiftService.UpdateShiftAsync(
        //     8,
        //     new ShiftDto()
        //     {
        //         StartDateTime = DateTime.Now,
        //         EndDateTime = DateTime.Now,
        //     }
        // );
        // DisplayTable.Shift([output]);

        ShiftDto worker = GetData.GetShift();

        Console.WriteLine(worker);

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
