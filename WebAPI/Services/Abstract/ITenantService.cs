using Core.Entities.Concrete;
using Core.Utilities.Results;

namespace WebAPI.Services.Abstract
{
    public interface ITenantService
    {
        Task<IDataResult<Tenant>> AddAsync(Tenant tenant);
        Task<IDataResult<Tenant>> DeleteAsync(int id);
        Task<IDataResult<List<Tenant>>> GetAllAsync();
        Task<IDataResult<Tenant>> GetByIdAsync(int id);
        Task<IDataResult<Tenant>> UpdateAsync(Tenant tenant);
    }
}