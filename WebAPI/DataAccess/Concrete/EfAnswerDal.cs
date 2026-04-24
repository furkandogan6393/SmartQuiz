using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using WebAPI.DataAccess.Abstract;

namespace WebAPI.DataAccess.Concrete
{
    public class EfAnswerDal : EfEntityRepositoryBase<Answer, UserDbContext>, IAnswerDal
    {
        public EfAnswerDal(UserDbContext context) : base(context)
        {
        }

        public async Task<List<Answer>> GetAnswersByQuestionIdAsync(int questionId)
        {
            return await _context.Answers
            .Where(a => a.QuestionId == questionId)
            .ToListAsync(); 
        }

    }
}
