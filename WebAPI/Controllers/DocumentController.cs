using Core.Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebAPI.Services.Abstract;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        readonly IDocumentService _documentService;
        public DocumentController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        [HttpGet("getbyid")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _documentService.GetByIdAsync(id);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _documentService.GetAllAsync();
            if(!result.Success)
                return BadRequest(result);
            return Ok(result);
        }


        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            var result = await _documentService.AddAsync(file, User);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpGet("getbyuserid")]
        public async Task<IActionResult> GetByUserId(string  userId)
        {
            var result = await _documentService.GetByUserIdAsync(userId);
            if(!result.Success)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _documentService.DeleteAsync(id);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpGet("getbytenantid")]
        public async Task<IActionResult> GetByTenantId()
        {
            var tenantId = User.Claims.FirstOrDefault(c => c.Type == "tenantId")?.Value;
            var result = await _documentService.GetByTenantIdAsync(tenantId);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }

    }
}
