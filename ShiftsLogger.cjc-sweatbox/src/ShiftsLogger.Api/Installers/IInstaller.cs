namespace ShiftsLogger.Api.Installers;

/// <summary>
/// Interface for all DI container installer classes.
/// </summary>
public interface IInstaller
{
    #region Methods

    void InstallServices(WebApplicationBuilder builder);

    #endregion
}
