namespace ShiftsLogger.Api.Contracts.V1;

/// <summary>
/// The supported API routes for this application.
/// </summary>
public static class ApiRoutes
{
    #region Constants

    public const string Root = "api";

    public const string Version = "v1";

    public const string Base = @$"{Root}/{Version}";

    public static class Shifts
    {
        public const string Create = @$"{Base}/shifts";
        public const string Get = @$"{Base}/shifts";
        public const string GetById = @$"{Base}/shifts/{{shiftId}}";
        public const string Update = @$"{Base}/shifts/{{shiftId}}";
        public const string Delete = @$"{Base}/shifts/{{shiftId}}";
    }

    #endregion
}
