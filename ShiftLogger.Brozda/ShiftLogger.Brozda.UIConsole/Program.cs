using Microsoft.Extensions.DependencyInjection;
using RestSharp;
using ShiftLogger.Brozda.Models;
using ShiftLogger.Brozda.UIConsole.Interfaces.MenuActionHandlers;
using ShiftLogger.Brozda.UIConsole.Interfaces.Services;
using ShiftLogger.Brozda.UIConsole.Menu;
using ShiftLogger.Brozda.UIConsole.MenuActionHandlers;
using ShiftLogger.Brozda.UIConsole.Services;

namespace ShiftLogger.Brozda.UIConsole
{
    internal class Program
    {
        private static async Task Main()
        {
            string baseUrl = "http://localhost:5181";

            var services = new ServiceCollection();

            SetServices(services, baseUrl);

            var sp = services.BuildServiceProvider();

            var mainMenu = sp.GetRequiredService<MainMenu>();

            await mainMenu.ProcessMenu();

            Console.ReadLine();
        }

        public static void SetServices(IServiceCollection services, string baseUrl)
        {
            //client
            services.AddSingleton(new RestClient(baseUrl));

            //services
            services.AddScoped<IApiService<ShiftTypeDto>>(
                sp => new ApiService<ShiftTypeDto>(sp.GetRequiredService<RestClient>(), "/api/shifttypes"
                ));
            services.AddScoped<IApiService<WorkerDto>>(
                sp => new ApiService<WorkerDto>(sp.GetRequiredService<RestClient>(), "/api/workers"
                ));
            services.AddScoped<IAssignedShiftService>(
                sp => new AssignedShiftService(sp.GetRequiredService<RestClient>(), "/api/assignedshifts"
                ));

            //menu action handlers
            services.AddSingleton<IShiftTypesActionHandler>(
                sp => new ShiftTypesActionHandler(sp.GetRequiredService<IApiService<ShiftTypeDto>>()
                ));
            services.AddSingleton<IWorkerActionHandler>(
                sp => new WorkerActionHandler(sp.GetRequiredService<IApiService<WorkerDto>>()
                ));
            services.AddSingleton<IAssignedShiftsActionHandler>(
                sp => new AssignedShiftsActionHandler(sp.GetRequiredService<IAssignedShiftService>()
                ));

            //main menu
            services.AddSingleton(
                sp => new MainMenu(
                    sp.GetRequiredService<IShiftTypesActionHandler>(),
                    sp.GetRequiredService<IWorkerActionHandler>(),
                    sp.GetRequiredService<IAssignedShiftsActionHandler>()
                    ));
        }
    }
}