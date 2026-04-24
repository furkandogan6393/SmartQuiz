using Core.Entities.Concrete;
using Core.Entities.Dto;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using System.Reflection.Metadata.Ecma335;
using WebAPI.BusinessAspects.Autofac;
using WebAPI.DataAccess.Abstract;

namespace WebAPI.DataAccess.Concrete
{
    public class AuthManager : IAuthService
    {
        readonly IUserService _userService;
        readonly ITokenHelper _tokenHelper;
        public AuthManager(IUserService userService, ITokenHelper tokenHelper) 
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }

        public async Task<IDataResult<AccessToken>> CreateAccesToken(User user)
        {
            var claims = await _userService.GetClaims(user);
            if(claims.Data == null)
            {
                return new ErrorDataResult<AccessToken>("Kullanıcı Yetkileri Bulunamadı");
            }
            var accessToken = _tokenHelper.CreateToken(user, claims.Data);
            return new SuccessDataResult<AccessToken>(accessToken, "Başarılı Alım");
        }

        public async Task<IDataResult<User>> KayıtOlabilirmi(string Email)
        {
            var result = await _userService.GetByMail(Email);
            if (result.Data!=null)
            {
                return new ErrorDataResult<User>("Böyle bir hesap zaten var");
            }
            return new SuccessDataResult<User>();
        }

        //[SecuredOperation("admin,user")]
        public async Task<IDataResult<User>> Login(LoginDto loginDto)
        {
            var userToCheck = await _userService.GetByMail(loginDto.Email);
            if(userToCheck.Data == null)
            {
                return new ErrorDataResult<User>("E-posta Hatalı");
            }

            if (!HashingHelper.VerifyPasswordHash(loginDto.Password, userToCheck.Data.PasswordHash, userToCheck.Data.PasswordSalt))
            {
                return new ErrorDataResult<User>(userToCheck.Data,"Şifre Hatalı");
            }

            return new SuccessDataResult<User>(userToCheck.Data, "Giriş Başarılı");

        }

        [SecuredOperation("admin,superadmin")]
        public async Task<IDataResult<User>> Register(RegisterDto registerDto, string password)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            var user = new User
            {
                UserId = Guid.NewGuid().ToString(),
                Email = registerDto.Email,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                TenantId = registerDto.TenantId
            };
            await _userService.Add(user);
            await _userService.RolesAdd(user, registerDto.OperationClaimId);
            return new SuccessDataResult<User>(user, "Kayıt Başarılı");
        }
    }
}