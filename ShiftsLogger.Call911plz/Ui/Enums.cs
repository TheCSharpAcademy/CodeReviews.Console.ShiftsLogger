
public static class MenuEnums
{
    public enum Main { MANAGESHIFT, MANAGEWORKER, EXIT }
    public enum Shift { CREATESHIFT, READSHIFT, UPDATESHIFT, DELETESHIFT, BACK }
    public enum Worker { CREATEWORKER, READWORKER, UPDATEWORKER, DELETEWORKER, BACK }
}

public static class Errors
{
    public static Dictionary<int, string> Codes = new()
    {
        {404, "Not found"},
        {400, "Syntax Error"},
        {401, "Not Authorized"},
        {403, "Forbidden"},
        {405, "Method Not Allowed"},
        {500, "Internal Error"},
        {501, "Not Implemented"},
        {502, "Bad Gateway"},
        {503, "Service Unavaliable"},
    };
}