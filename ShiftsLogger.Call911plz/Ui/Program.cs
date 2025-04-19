namespace Ui;

using System.Threading.Tasks;
using RestSharp;
using System.Text.Json;
using Spectre.Console;

class Program
{
    static async Task Main(string[] args)
    {
        WorkerService workerService = new();
        workerService.ConnectApi();

        var output = await workerService.DeleteWorkerAsync
        (
            new Worker()
            {
                EmployeeName = "aids",
                EmployeeId = 1000,
            }
        );
        //DisplayTable.Worker([output]);

        Console.WriteLine(output);


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
