using Global5.Core.Models;
using Global5.Core.Requests;
using Global5.Core.Requests.FornecedorRequest;
using Global5.Core.Responses;

namespace Global5.Core.Handlers;

public interface IFornecedorHandler
{
    Task<Response<Fornecedor?>> CreateAsync(CreateFornecedorRequest request);
    Task<Response<Fornecedor?>> GetAsync(GetFornecedorRequest request);
    Task<Response<Fornecedor?>> DeleteAsync(DeleteFornecedorRequest request);
    Task<Response<Fornecedor?>> EditAsync(EditFornecedorRequest request);
    Task<Response<List<Fornecedor>?>> GetAllAsync(GetAllFornecedorRequest request);
}