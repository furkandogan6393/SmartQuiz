using Core.Entities.Concrete;
using Core.Utilities.Results;

namespace WebAPI.Services.Abstract
{
    public interface IQuestionService
    {
        Task<IDataResult<Question>> AddAsync(Question question);
        Task<IDataResult<List<Question>>> GetAllAsync();
        Task<IDataResult<List<Question>>> GetByQuizIdAsync(int quizId);
        Task<IDataResult<Question>> UpdateAsync(Question question);
        Task<IDataResult<Question>> DeleteAsync(int id);
    }
}
