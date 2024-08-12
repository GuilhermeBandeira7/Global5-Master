using System.Data;
using Dapper;
using Global5.Core.Handlers;
using Global5.Core.Models;
using Global5.Core.Requests.EstoqueRequest;
using Global5.Core.Responses;
using Microsoft.Data.SqlClient;

namespace Global5.Api.Handlers;

public class EstoqueHandler : IEstoqueHandler
{
    private const string ConnectionString = "Server=localhost,1433;Database=Global5DB;" +
                                            "User ID=sa;Password=1q2w3e4r@#$;TrustServerCertificate=true";
    
    public async Task<Response<List<Estoque>?>> GetAsync(GetMovimentationRequest request)
    {
        await using var connection = new SqlConnection(ConnectionString);
        var parameters = new DynamicParameters();
        parameters.Add("TableName", "Estoque"); 
        
        try
        {
            var estoque = await connection.QueryAsync<Estoque>("sp_get",
                parameters, commandType: CommandType.StoredProcedure);
                
            return new Response<List<Estoque>?>(estoque.ToList(),201);
        }
        catch
        {
            return new Response<List<Estoque>?>(null, 500, "Não foi possível buscar fornecedores.");
        }
    }
    public async Task<Response<Estoque?>> MoveAsync(MoveStockRequest request)
    {
        await using var connection = new SqlConnection(ConnectionString);
        var parameters = new DynamicParameters();
        parameters.Add("dateTime", request.DateTime);
        parameters.Add("fornecedorId", request.FornecedorId);
        parameters.Add("materialId", request.MaterialId);
        parameters.Add("quantidade", request.Quantidade);
        parameters.Add("valorTotal", request.ValorTotal);
        parameters.Add("tipoOperacao", request.TipoOperacao);

        try
        {
              await connection.QueryAsync<Fornecedor>("sp_insert_estoque",
                parameters, commandType: CommandType.StoredProcedure);
                
            return new Response<Estoque?>(null, 201, "Estoque movimentado com sucesso!");
        }
        catch
        {
            return new Response<Estoque?>(null, 500, "Não foi possível movimentar estoque.");
        }
    }

    public async Task<Response<Estoque?>> RemoveAsync(RemoveMovimentationRequest request)
    {
        await using var connection = new SqlConnection(ConnectionString);
        var parameters = new DynamicParameters();  
        parameters.Add("TableName", "Estoque");
        parameters.Add("Id", request.Id);

        try
        {
            connection.QuerySingleOrDefault<Fornecedor>("sp_delete",
                parameters, commandType: CommandType.StoredProcedure);
                
            return new Response<Estoque?>(null,201, "Movimentação excluída com sucesso.");
        }
        catch
        {
            return new Response<Estoque?>(null, 500, "Não foi possível excluir movimentação.");
        }
    }

    public async Task<Response<Estoque?>> EditAsync(EditMovimentationRequest request)
    {
        await using var connection = new SqlConnection(ConnectionString);
        var parameters = new DynamicParameters();
        parameters.Add("TableName", "Estoque"); 
        parameters.Add("ColumnName", request.ColumnName);
        parameters.Add("Id", request.Id); 
        parameters.Add("NewValue" , request.ColumnName == request.TipoOperacao ? request.TipoOperacao : request.Quantidade);
        
        try
        {
            connection.QuerySingleOrDefault<Fornecedor>("sp_update",
                parameters, commandType: CommandType.StoredProcedure);
                
            return new Response<Estoque?>(null,201, "Estoque editado com sucesso.");
        }
        catch
        {
            return new Response<Estoque?>(null, 500, "Não foi possível editar estoque.");
        }
    }

}