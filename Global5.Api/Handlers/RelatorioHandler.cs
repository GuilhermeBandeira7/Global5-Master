using System.Data;
using Dapper;
using Global5.Core.Handlers;
using Global5.Core.Models;
using Global5.Core.Requests.RelatorioRequest;
using Global5.Core.Responses;
using Microsoft.Data.SqlClient;

namespace Global5.Api.Handlers;

public class RelatorioHandler : IRelatorioHandler
{
    private const string ConnectionString = "Server=localhost,1433;Database=Global5DB;" +
                                            "User ID=sa;Password=1q2w3e4r@#$;TrustServerCertificate=true";
    
    public async Task<Response<List<Relatorio>?>> GenerateAsync(GerarRelatorioRequest request)
    {
        await using var connection = new SqlConnection(ConnectionString);
        var parameters = new DynamicParameters();  
        parameters.Add("fornecedorId", request.FornecedorId);
        parameters.Add("materialId", request.MaterialId);
        
        try
        {
            var relatorio = await connection.QueryAsync<Relatorio>("sp_relatorio",
                parameters, commandType: CommandType.StoredProcedure);
                
            return new Response<List<Relatorio>?>(relatorio.ToList(), 201, "Relatório gerado com sucesso!");
        }
        catch
        {
            return new Response<List<Relatorio>?>(null, 500, "Não foi possível gerar relatório.");
        }
    }
}