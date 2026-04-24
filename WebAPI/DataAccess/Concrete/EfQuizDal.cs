using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using WebAPI.DataAccess.Abstract;

namespace WebAPI.DataAccess.Concrete
{
    public class EfQuizDal : EfEntityRepositoryBase<Quiz, UserDbContext>, IQuizDal
    {
        public EfQuizDal( UserDbContext context) : base(context)
        {

        }

    }
}
