using UI.DAL;
using UI.Models;

namespace UI;

internal class Program
{
    private static HttpClient sharedClient = new()
    {
        BaseAddress = new Uri("https://localhost:7012")
    };

    static async Task Main(string[] args)
    {
        var repo = new ShiftDataAccess(sharedClient);
    }
}