using Microsoft.Data.SqlClient;

namespace Global5.Api.Data;

public class DbConnection
{
    private string ConnString { get; } = "Server=localhost,1433;Database=Global5DB;User ID=sa;Password=1q2w3e4r@#$";
    
    public DbConnection()
    {
        using (var connection = new SqlConnection())
        {
            
        };
    }
}