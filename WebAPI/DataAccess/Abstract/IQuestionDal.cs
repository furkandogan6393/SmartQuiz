using Core.DataAccess;
using Core.Entities.Concrete;

namespace WebAPI.DataAccess.Abstract
{
    public interface IQuestionDal:IEntityRepository<Question>
    {
        Task<List<Question>> GetQuestionsByQuizIdAsync(int  quizId);
    }
}
