using Core.Entities.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DataAccess.Abstract;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        readonly IAuthService _authService;
        readonly IUserService _userService;

        public UsersController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var userToLogin = await _authService.Login(loginDto);
            if (!userToLogin.Success)
            {
                return BadRequest("Başarısız Giriş");
            }

            var result = await _authService.CreateAccesToken(userToLogin.Data);
            if(result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest("İkiside Yanlış Mennn");
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var KayıtOlabilirmi = await _authService.KayıtOlabilirmi(registerDto.Email);
            if(!KayıtOlabilirmi.Success)
            {
                return BadRequest("Bu E posta zaten var");
            }

            var registerResult = await _authService.Register(registerDto, registerDto.Password);
            var result = await _authService.CreateAccesToken(registerResult.Data);
            if(result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest("Başarısız Kayıt");
        }

        [HttpGet("getallbytenantid")]
        public async Task<IActionResult> GetAllByTenantId()
        {
            var tenantId = User.Claims.FirstOrDefault(c => c.Type == "tenantId")?.Value;
            var result = await _userService.GetAllByTenantIdAsync(tenantId);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }
    }
}
