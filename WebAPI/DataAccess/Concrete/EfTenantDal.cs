using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using WebAPI.DataAccess.Abstract;

namespace WebAPI.DataAccess.Concrete
{
    public class EfTenantDal : EfEntityRepositoryBase<Tenant, UserDbContext>, ITenantDal
    {
        public EfTenantDal(UserDbContext context) : base(context)
        {
        }
    }
}