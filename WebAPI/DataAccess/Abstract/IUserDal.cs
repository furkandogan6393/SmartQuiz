using Core.DataAccess;
using Core.Entities.Concrete;
using Core.Entities.Dto;

namespace WebAPI.DataAccess.Abstract
{
    public interface IUserDal : IEntityRepository<User>
    {
        Task<User> GetByMail(string mail);
        Task<List<OperationClaim>> GetClaims(User user);
        Task AddRoles(User user,int OperationClaimId);
        Task<List<UserDto>> GetAllByTenantIdAsync(string tenantId);
    }
}