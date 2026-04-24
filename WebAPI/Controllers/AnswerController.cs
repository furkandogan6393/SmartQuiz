using Core.Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Services.Abstract;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswerController : ControllerBase
    {
        readonly IAnswerService _answerService;
        public AnswerController(IAnswerService answerService)
        {
            _answerService = answerService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(Answer answer)
        {
            var result = await _answerService.AddAsync(answer);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(Answer answer)
        {
            var result = await _answerService.UpdateAsync(answer);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _answerService.DeleteAsync(id);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }


        [HttpGet("getall")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _answerService.GetAllAsync();
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpGet("getallbyid")]
        public async Task<IActionResult> GetAllById(int id)
        {
            var result = await _answerService.GetByQuestionIdAsync(id);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }


    }
}
