namespace Global5.Core;

public static class Configuration
{
    public const int DefaultStatusCode = 200;
    
    public static string BackendUrl { get; set; } = "http://localhost:5288";
    public static string FrontendUrl { get; set; } = "http://localhost:5170";
}