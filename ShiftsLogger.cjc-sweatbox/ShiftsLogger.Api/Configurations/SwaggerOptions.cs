namespace ShiftsLogger.Api.Configurations;

/// <summary>
/// Class to hold the required application options for swagger.
/// </summary>
public class SwaggerOptions
{
    #region Properties

    public string JsonRoute { get; set; }

    public string Description { get; set; }

    public string UiEndpoint { get; set; }

    #endregion
}
