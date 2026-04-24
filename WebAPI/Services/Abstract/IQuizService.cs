using Core.Entities.Concrete;
using Core.Utilities.Results;

namespace WebAPI.Services.Abstract
{
    public interface IQuizService
    {
        Task<IDataResult<List<Quiz>>> GetAllAsync();
        Task<IDataResult<Quiz>> GetByIdAsync(int id);
        Task<IDataResult<Quiz>> AddAsync(Quiz quiz);
        Task<IDataResult<Quiz>> UpdateAsync(Quiz quiz);
        Task<IDataResult<Quiz>> DeleteAsync(int id);
    }
}
