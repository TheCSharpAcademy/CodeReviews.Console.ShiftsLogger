using Microsoft.OpenApi.Models;

namespace ShiftsLogger.Api.Installers;

/// <summary>
/// Register the required MVC services to the DI container.
/// </summary>
public class MvcInstaller : IInstaller
{
    #region Methods

    public void InstallServices(WebApplicationBuilder builder)
    {
        // -------------------------------------------------------------------------------------
        // MVC.
        // -------------------------------------------------------------------------------------

        builder.Services.AddControllers();

        // -------------------------------------------------------------------------------------
        // Swagger.
        // -------------------------------------------------------------------------------------

        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Shifts Logger",
                Version = "v1"
            });
        });
    }

    #endregion
}
