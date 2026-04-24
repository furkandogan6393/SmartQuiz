using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using WebAPI.DataAccess.Abstract;

namespace WebAPI.DataAccess.Concrete
{
    public class EfQuestionDal : EfEntityRepositoryBase<Question, UserDbContext>, IQuestionDal
    {
        public EfQuestionDal(UserDbContext context) : base(context)
        {
        }

        public async Task<List<Question>> GetQuestionsByQuizIdAsync(int quizId)
        {
            return await _context.Questions
                .Where(q => q.QuizId == quizId).ToListAsync();
        }
    }
}
