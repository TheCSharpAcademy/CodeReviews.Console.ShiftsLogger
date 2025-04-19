using System.Net;
public static class MenuEnums
{
    public enum Main { MANAGESHIFT, MANAGEWORKER, EXIT }
    public enum Shift { CREATESHIFT, READSHIFT, UPDATESHIFT, DELETESHIFT, BACK }
    public enum Worker { CREATEWORKER, READWORKER, UPDATEWORKER, DELETEWORKER, BACK }
}

public static class Errors
{
    public static Dictionary<HttpStatusCode, string> Codes = new()
    {
        {HttpStatusCode.NotFound, "Not found"},
        {HttpStatusCode.BadRequest, "Syntax Error"},
        {HttpStatusCode.Forbidden, "Forbidden"},
        {HttpStatusCode.MethodNotAllowed, "Method Not Allowed"},
        {HttpStatusCode.InternalServerError, "Internal Error"},
        {HttpStatusCode.NotImplemented, "Not Implemented"},
        {HttpStatusCode.BadGateway, "Bad Gateway"},
        {HttpStatusCode.ServiceUnavailable, "Service Unavaliable"},
    };
}