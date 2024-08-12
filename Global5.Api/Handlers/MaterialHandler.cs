using System.Data;
using Dapper;
using Global5.Core.Handlers;
using Global5.Core.Models;
using Global5.Core.Requests;
using Global5.Core.Requests.FornecedorRequest;
using Global5.Core.Requests.MaterialRequest;
using Global5.Core.Responses;
using Microsoft.Data.SqlClient;

namespace Global5.Api.Handlers;

public class MaterialHandler : IMaterialHandler
{
    private const string ConnectionString = "Server=localhost,1433;Database=Global5DB;" +
                                            "User ID=sa;Password=1q2w3e4r@#$;TrustServerCertificate=true";
    
    public async Task<Response<Material?>> CreateAsync(CreateMaterialRequest request)
    {
        await using var connection = new SqlConnection(ConnectionString);
        var parameters = new DynamicParameters();  
        parameters.Add("Codigo", request.Codigo);
        parameters.Add("Nome", request.Nome);
        parameters.Add("UnidadeMedida", request.UnidadeDeMedia);
        
        try
        {
            var material = await connection.QuerySingleOrDefaultAsync<Material>("sp_insert_materiais",
                parameters, commandType: CommandType.StoredProcedure);
                
            return new Response<Material?>(material, 201, "Material criado com sucesso!");
        }
        catch
        {
            return new Response<Material?>(null, 500, "Não foi possível criar material.");
        }
    }

    public async Task<Response<Material?>> GetAsync(GetMaterialRequest request)
    {
        await using var connection = new SqlConnection(ConnectionString);
        var parameters = new DynamicParameters();
        parameters.Add("TableName", "Material"); 
        parameters.Add("Id", request.Id); 
        
        try
        {
            var material =  connection.QuerySingleOrDefault<Material>("sp_get_by_id",
                parameters, commandType: CommandType.StoredProcedure);
                
            return new Response<Material?>(material, 201);
        }
        catch
        {
            return new Response<Material?>(null, 500, "Não foi possível consultar material.");
        }
    }

    public async Task<Response<Material?>> DeleteAsync(DeleteMaterialRequest request)
    {
        await using var connection = new SqlConnection(ConnectionString);
        var parameters = new DynamicParameters();
        parameters.Add("TableName", "Material"); 
        parameters.Add("Id", request.Id); 
        
        try
        {
            connection.QuerySingleOrDefault<Material>("sp_delete",
                parameters, commandType: CommandType.StoredProcedure);
                
            return new Response<Material?>(null,201, "Material excluído com sucesso.");
        }
        catch
        {
            return new Response<Material?>(null, 500, "Não foi possível excluir material.");
        }
    }

    public async Task<Response<Material?>> EditAsync(EditMaterialRequest request)
    {
        await using var connection = new SqlConnection(ConnectionString);
        var parameters = new DynamicParameters();
        parameters.Add("TableName", "Material"); 
        parameters.Add("ColumnName", request.ColumnName);
        parameters.Add("Id", request.Id); 
        if(request.ColumnName == request.Codigo)
            parameters.Add("NewValue", request.Codigo);
        else if(request.ColumnName == request.Nome)
            parameters.Add("NewValue", request.Nome);
        else
            parameters.Add("NewValue", request.UnidadeDeMedia);
        
        try
        {
            var material = connection.QuerySingleOrDefault<Material>("sp_update",
                parameters, commandType: CommandType.StoredProcedure);
                
            return new Response<Material?>(material,201, "Material editado com sucesso.");
        }
        catch
        {
            return new Response<Material?>(null, 500, "Não foi possível editar material.");
        }
    }
    

    public async Task<Response<List<Material>?>> GetAllAsync(GetAllMaterialRequest request)
    {
        await using var connection = new SqlConnection(ConnectionString);
        var parameters = new DynamicParameters();
        parameters.Add("TableName", "Material"); 
        
        try
        {
            var material = await connection.QueryAsync<Material>("sp_get",
                parameters, commandType: CommandType.StoredProcedure);
                
            return new Response<List<Material>?>(material.ToList(),201);
        }
        catch
        {
            return new Response<List<Material>?>(null, 500, "Não foi possível buscar materiais.");
        }
    }
}