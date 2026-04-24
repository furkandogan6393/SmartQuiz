using Core.Entities.Dto;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Services.Abstract;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AIController : ControllerBase
    {
        readonly IAIService _aiService;

        public AIController(IAIService aiService)
        {
            _aiService = aiService;
        }

        [HttpPost("generate")]
        public async Task<IActionResult> Generate(AIQuizRequest request)
        {
            var result = await _aiService.GenerateQuizAsync(request);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPost("generate-word")]
        public async Task<IActionResult> GenerateWord(AIQuizRequest request)
        {
            var result = await _aiService.GenerateQuizWordAsync(request);
            if (!result.Success)
                return BadRequest(result.Message);

            return File(result.Data,
                "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                "sinav.docx");
        }

        [HttpGet("prompt-templates")]
        public IActionResult GetPromptTemplates()
        {
            var templates = _aiService.GetPromptTemplates();
            return Ok(templates);
        }
    }
}