using Global5.Core.Models;
using Global5.Core.Requests.RelatorioRequest;
using Global5.Core.Responses;

namespace Global5.Core.Handlers;

public interface IRelatorioHandler
{
    Task<Response<List<Relatorio>?>> GenerateAsync(GerarRelatorioRequest request);
}