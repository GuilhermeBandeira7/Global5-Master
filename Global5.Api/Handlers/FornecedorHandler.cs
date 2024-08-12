using System.Data;
using Dapper;
using Global5.Core.Handlers;
using Global5.Core.Models;
using Global5.Core.Requests;
using Global5.Core.Requests.FornecedorRequest;
using Global5.Core.Responses;
using Microsoft.Data.SqlClient;

namespace Global5.Api.Handlers;

public class FornecedorHandler : IFornecedorHandler
{
    private const string ConnectionString = "Server=localhost,1433;Database=Global5DB;" +
                                            "User ID=sa;Password=1q2w3e4r@#$;TrustServerCertificate=true";
    public async Task<Response<Fornecedor?>> CreateAsync(CreateFornecedorRequest request)
    {
        await using var connection = new SqlConnection(ConnectionString);
        var parameters = new DynamicParameters();  
        parameters.Add("CNPJ", request.CNPJ);  
        parameters.Add("RazaoSocial", request.RazaoSocial);

        try
        {
            var fornecedor = await connection.QuerySingleOrDefaultAsync<Fornecedor>("sp_insert_fornecedores",
                parameters, commandType: CommandType.StoredProcedure);
                
            return new Response<Fornecedor?>(fornecedor, 201, "Fornecedor criado com sucesso!");
        }
        catch
        {
            return new Response<Fornecedor?>(null, 500, "Não foi possível criar fornecedor.");
        }
    }

    public async Task<Response<Fornecedor?>> GetAsync(GetFornecedorRequest request)
    {
        await using var connection = new SqlConnection(ConnectionString);
        var parameters = new DynamicParameters();
        parameters.Add("TableName", "Fornecedor"); 
        parameters.Add("Id", request.Id); 
        
        try
        {
            var fornecedor =  connection.QuerySingleOrDefault<Fornecedor>("sp_get_by_id",
                parameters, commandType: CommandType.StoredProcedure);
                
            return new Response<Fornecedor?>(fornecedor, 201);
        }
        catch
        {
            return new Response<Fornecedor?>(null, 500, "Não foi possível consultar fornecedor.");
        }
    }

    public async Task<Response<Fornecedor?>> DeleteAsync(DeleteFornecedorRequest request)
    {
        await using var connection = new SqlConnection(ConnectionString);
        var parameters = new DynamicParameters();
        parameters.Add("TableName", "Fornecedor"); 
        parameters.Add("Id", request.Id); 
        
        try
        {
            connection.QuerySingleOrDefault<Fornecedor>("sp_delete",
                parameters, commandType: CommandType.StoredProcedure);
                
            return new Response<Fornecedor?>(null,201, "Fornecedor excluído com sucesso.");
        }
        catch
        {
            return new Response<Fornecedor?>(null, 500, "Não foi possível excluir fornecedor.");
        }
    }

    public async Task<Response<Fornecedor?>> EditAsync(EditFornecedorRequest request)
    {
        await using var connection = new SqlConnection(ConnectionString);
        var parameters = new DynamicParameters();
        parameters.Add("TableName", "Fornecedor"); 
        parameters.Add("ColumnName", request.ColumnName);
        parameters.Add("Id", request.Id); 
        parameters.Add("NewValue" , request.ColumnName == request.Cnpj ? request.Cnpj : request.RazaoSocial);
        
        try
        {
            connection.QuerySingleOrDefault<Fornecedor>("sp_update",
                parameters, commandType: CommandType.StoredProcedure);
                
            return new Response<Fornecedor?>(null,201, "Fornecedor editado com sucesso.");
        }
        catch
        {
            return new Response<Fornecedor?>(null, 500, "Não foi possível editar fornecedor.");
        }
    }

    public async Task<Response<List<Fornecedor>?>> GetAllAsync(GetAllFornecedorRequest request)
    {
        await using var connection = new SqlConnection(ConnectionString);
        var parameters = new DynamicParameters();
        parameters.Add("TableName", "Fornecedor"); 
        
        try
        {
            var fornecedores = await connection.QueryAsync<Fornecedor>("sp_get",
                parameters, commandType: CommandType.StoredProcedure);
                
            return new Response<List<Fornecedor>?>(fornecedores.ToList(),201);
        }
        catch
        {
            return new Response<List<Fornecedor>?>(null, 500, "Não foi possível buscar fornecedores.");
        }
    }
}