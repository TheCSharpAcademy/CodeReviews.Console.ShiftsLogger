namespace ShiftsLogger.Api.Installers;

public static class InstallerExtensions
{
    #region Methods

    public static void InstallServicesInAssembly(this WebApplicationBuilder builder)
    {
        var installers = typeof(Program).Assembly.ExportedTypes.
            Where(x => typeof(IInstaller).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract).
            Select(Activator.CreateInstance).
            Cast<IInstaller>().
            ToList();

        installers.ForEach(installer => installer.InstallServices(builder));
    }

    #endregion
}
