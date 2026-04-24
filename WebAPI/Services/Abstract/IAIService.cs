using Core.Entities.Dto;
using Core.Utilities.Results;

namespace WebAPI.Services.Abstract
{
    public interface IAIService
    {
        Task<IDataResult<string>> GenerateQuizAsync(AIQuizRequest request);
        Task<IDataResult<byte[]>> GenerateQuizWordAsync(AIQuizRequest request);
        List<string> GetPromptTemplates();
    }
}
