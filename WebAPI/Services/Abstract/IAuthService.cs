using Core.Entities.Concrete;
using Core.Entities.Dto;
using Core.Utilities.Results;
using Core.Utilities.Security.JWT;
using System.IdentityModel.Tokens.Jwt;

namespace WebAPI.DataAccess.Abstract
{
    public interface IAuthService
    {
        Task<IDataResult<AccessToken>> CreateAccesToken(User user);
        Task<IDataResult<User>> KayıtOlabilirmi(string Email);
        Task<IDataResult<User>> Login(LoginDto loginDto);
        Task<IDataResult<User>> Register(RegisterDto registerDto, string password); //Bize User Türü Dönecek Demek.

    }
}