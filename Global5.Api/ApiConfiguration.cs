namespace Global5.Api;

public class ApiConfiguration
{
    public const string UserId = "user@global5.io";
    public static string ConnectionString { get; set; } = string.Empty;
    public static string CorsPolicyName = "wasm";
}