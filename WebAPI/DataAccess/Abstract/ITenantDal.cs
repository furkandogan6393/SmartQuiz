using Core.DataAccess;
using Core.Entities.Concrete;

namespace WebAPI.DataAccess.Abstract
{
    public interface ITenantDal : IEntityRepository<Tenant>
    {
    }
}