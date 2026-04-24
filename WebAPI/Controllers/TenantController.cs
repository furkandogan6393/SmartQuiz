using Core.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Services.Abstract;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TenantController : ControllerBase
    {
        readonly ITenantService _tenantService;

        public TenantController(ITenantService tenantService)
        {
            _tenantService = tenantService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(Tenant tenant)
        {
            var result = await _tenantService.AddAsync(tenant);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _tenantService.GetAllAsync();
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _tenantService.GetByIdAsync(id);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(Tenant tenant)
        {
            var result = await _tenantService.UpdateAsync(tenant);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _tenantService.DeleteAsync(id);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }
    }
}