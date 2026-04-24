using Core.DataAccess;
using Core.Entities.Concrete;

namespace WebAPI.DataAccess.Abstract
{
    public interface IAnswerDal : IEntityRepository<Answer>
    {
        Task<List<Answer>> GetAnswersByQuestionIdAsync(int questionId);
    }
}
