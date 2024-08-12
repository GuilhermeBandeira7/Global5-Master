using Global5.Core.Models;
using Global5.Core.Requests.MaterialRequest;
using Global5.Core.Responses;

namespace Global5.Core.Handlers;

public interface IMaterialHandler
{
    Task<Response<Material?>> CreateAsync(CreateMaterialRequest request);
    Task<Response<Material?>> DeleteAsync(DeleteMaterialRequest request);
    Task<Response<Material?>> GetAsync(GetMaterialRequest request);
    Task<Response<Material?>> EditAsync(EditMaterialRequest request);
    Task<Response<List<Material>?>> GetAllAsync(GetAllMaterialRequest request);
}