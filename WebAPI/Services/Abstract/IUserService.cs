using Core.Entities.Concrete;
using Core.Entities.Dto;
using Core.Utilities.Results;

namespace WebAPI.DataAccess.Abstract
{
    public interface IUserService
    {
        Task<IDataResult<List<OperationClaim>>> GetClaims(User user);
        Task<IDataResult<User>> Add(User user);
        Task<IDataResult<User>> GetByMail(string Email);
        Task<IDataResult<User>> RolesAdd(User user, int OperationClaimId);
        Task<IDataResult<List<UserDto>>> GetAllByTenantIdAsync(string tenantId);
    }
}