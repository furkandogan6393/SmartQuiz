using Core.Entities.Concrete;
using Core.Utilities.Results;
using System.Security.Claims;
using Document = Core.Entities.Concrete.Document;
namespace WebAPI.Services.Abstract
{
    public interface IDocumentService
    {
        Task<IDataResult<Document>> AddAsync(IFormFile file, ClaimsPrincipal user);
        Task<IDataResult<Document>> DeleteAsync(int id);
        Task<IDataResult<List<Document>>> GetAllAsync();
        Task<IDataResult<List<Document>>> GetByUserIdAsync (string userId);
        Task<IDataResult<Document>> GetByIdAsync(int id);
        Task<IDataResult<List<Document>>> GetByTenantIdAsync(string tenantId);
    }
}
