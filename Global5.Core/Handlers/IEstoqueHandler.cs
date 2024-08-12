using Global5.Core.Models;
using Global5.Core.Requests.EstoqueRequest;
using Global5.Core.Responses;

namespace Global5.Core.Handlers;

public interface IEstoqueHandler
{
    Task<Response<Estoque?>> MoveAsync(MoveStockRequest request);
    Task<Response<Estoque?>> RemoveAsync(RemoveMovimentationRequest request);
    Task<Response<Estoque?>> EditAsync(EditMovimentationRequest request);
    Task<Response<List<Estoque>?>> GetAsync(GetMovimentationRequest request);
}