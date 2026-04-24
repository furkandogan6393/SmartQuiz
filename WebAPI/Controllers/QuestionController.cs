using Core.Entities.Concrete;
using Core.Utilities.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebAPI.Services.Abstract;
using WebAPI.Services.Concrete;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        readonly IQuestionService _questionService;
        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(Question question)
        {
            var result = await _questionService.AddAsync(question);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(Question question)
        {
            var result = await _questionService.UpdateAsync(question);
            if(!result.Success)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _questionService.DeleteAsync(id);
            if(!result.Success)
                return BadRequest(result);
            return Ok(result);
        }


        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _questionService.GetAllAsync();
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);  
                  

        }

        [HttpGet("getallbyid")]
        public async Task<IActionResult> GetAllById(int id)
        {
            var result = await _questionService.GetByQuizIdAsync(id);
            if(!result.Success)
                return BadRequest(result);
            return Ok(result);
        }


    }
}
