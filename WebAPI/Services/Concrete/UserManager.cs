using Core.Entities.Concrete;
using Core.Entities.Dto;
using Core.Utilities.Results;
using WebAPI.DataAccess.Abstract;

namespace WebAPI.DataAccess.Concrete
{
    public class UserManager : IUserService
    {
        readonly IUserDal _userDal;
        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }
        public async Task<IDataResult<User>> Add(User user)
        {
            await _userDal.Add(user);
            return new SuccessDataResult<User>(user,"Başarılı");
        }

        public async Task<IDataResult<User>> GetByMail(string Email)
        {
            var mail = await _userDal.GetByMail(Email);
            return new SuccessDataResult<User>(mail, "Mail Getirildi Başarılı");
        }

        public async Task<IDataResult<List<OperationClaim>>> GetClaims(User user)
        {
            var claims = await _userDal.GetClaims(user);
            return new SuccessDataResult<List<OperationClaim>>(claims, "Yetkiler Getirildi");
        }

        public async Task<IDataResult<User>> RolesAdd(User user, int OperationClaimId)
        {
            await _userDal.AddRoles(user, OperationClaimId);
            return new SuccessDataResult<User>(user, "Başarıyla Eklendi");
        }

        public async Task<IDataResult<List<UserDto>>> GetAllByTenantIdAsync(string tenantId)
        {
            var users = await _userDal.GetAllByTenantIdAsync(tenantId);
            return new SuccessDataResult<List<UserDto>>(users, "Kullanıcılar Getirildi");
        }
    }   
}   