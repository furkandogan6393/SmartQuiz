using Core.Entities.Concrete;
using Core.Utilities.Results;

namespace WebAPI.Services.Abstract
{
    public interface IAnswerService
    {
            Task<IDataResult<Answer>> AddAsync(Answer answer);
            Task<IDataResult<List<Answer>>> GetAllAsync();
            Task<IDataResult<List<Answer>>> GetByQuestionIdAsync(int questionId);
            Task<IDataResult<Answer>> UpdateAsync(Answer answer);
            Task<IDataResult<Answer>> DeleteAsync(int id);
    }

}
