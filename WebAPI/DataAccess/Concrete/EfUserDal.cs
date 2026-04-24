using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using Core.Entities.Dto;
using Microsoft.EntityFrameworkCore;
using WebAPI.DataAccess.Abstract;

namespace WebAPI.DataAccess.Concrete
{
    public class EfUserDal : EfEntityRepositoryBase<User, UserDbContext>, IUserDal
    {

        public EfUserDal(UserDbContext context) : base(context)
        {

        }

        public async Task AddRoles(User user, int OperationClaimId)
        {
            var userRole = new UserOperationClaim
            {
                UserId = user.UserId,
                OperationClaimId = OperationClaimId
            };
            await _context.UserOperationClaims.AddAsync(userRole);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetByMail(string Email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == Email);
        }

        public async Task<List<OperationClaim>> GetClaims(User user)
        {
            var claims = _context.UserOperationClaims
            .Where(uoc => uoc.UserId == user.UserId)
            .Join(_context.OperationClaims,
            uoc => uoc.OperationClaimId,
            oc => oc.Id,
            (uoc, oc) => oc);
            return await claims.ToListAsync();

        }

        public async Task<List<UserDto>> GetAllByTenantIdAsync(string tenantId)
        {
            return await _context.Users
                .Where(u => u.TenantId == tenantId)
                .Select(u => new UserDto
                {
                    UserId = u.UserId,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    TenantId = u.TenantId
                })
                .ToListAsync();
        }

    }
}
    