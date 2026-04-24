using Core.Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebAPI.Services.Abstract;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        readonly IQuizService _quizService;
        public QuizController(IQuizService quizService)
        {
            _quizService = quizService;
        }

        [HttpPost("add")]        
        public async Task<IActionResult> Add(Quiz quiz)
        {
            var result = await _quizService.AddAsync(quiz);
            if(!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(Quiz quiz)
        {
            var result = await _quizService.UpdateAsync(quiz);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _quizService.DeleteAsync(id);
            if(!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _quizService.GetAllAsync();
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuizById(int id)
        {
            var result = await _quizService.GetByIdAsync(id);
            if(!result.Success)
                return BadRequest(result.Message);
            return Ok(result.Data);
        }
        
    }
}
