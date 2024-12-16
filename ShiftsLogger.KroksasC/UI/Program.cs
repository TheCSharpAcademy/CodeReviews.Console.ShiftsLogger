using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using UI.Models;
using System;
using UI.Controllers;
using UI.UI;

namespace UI
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            await ShiftLoggerMenu.Run();
        }
    }
}
