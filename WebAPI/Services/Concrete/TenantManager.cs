using Core.Entities.Concrete;
using Core.Utilities.Results;
using WebAPI.DataAccess.Abstract;
using WebAPI.Services.Abstract;

namespace WebAPI.Services.Concrete
{
    public class TenantManager : ITenantService
    {
        readonly ITenantDal _tenantDal;

        public TenantManager(ITenantDal tenantDal)
        {
            _tenantDal = tenantDal;
        }

        public async Task<IDataResult<Tenant>> AddAsync(Tenant tenant)
        {
            if (tenant == null)
                return new ErrorDataResult<Tenant>("Tenant boş olamaz");
            tenant.CreatedAt = DateTime.UtcNow;
            tenant.IsActive = true;
            await _tenantDal.Add(tenant);
            return new SuccessDataResult<Tenant>(tenant, "Kurum Eklendi");
        }

        public async Task<IDataResult<Tenant>> DeleteAsync(int id)
        {
            var tenant = await _tenantDal.Get(t => t.Id == id);
            if (tenant == null)
                return new ErrorDataResult<Tenant>("Kurum Bulunamadı");
            await _tenantDal.Delete(tenant);
            return new SuccessDataResult<Tenant>(tenant, "Kurum Silindi");
        }

        public async Task<IDataResult<List<Tenant>>> GetAllAsync()
        {
            var tenants = await _tenantDal.GetAll();
            return new SuccessDataResult<List<Tenant>>(tenants, "Kurumlar Getirildi");
        }

        public async Task<IDataResult<Tenant>> GetByIdAsync(int id)
        {
            var tenant = await _tenantDal.Get(t => t.Id == id);
            if (tenant == null)
                return new ErrorDataResult<Tenant>("Kurum Bulunamadı");
            return new SuccessDataResult<Tenant>(tenant, "Kurum Getirildi");
        }

        public async Task<IDataResult<Tenant>> UpdateAsync(Tenant tenant)
        {
            var existing = await _tenantDal.Get(t => t.Id == tenant.Id);
            if (existing == null)
                return new ErrorDataResult<Tenant>("Kurum Bulunamadı");
            await _tenantDal.Update(tenant);
            return new SuccessDataResult<Tenant>(tenant, "Kurum Güncellendi");
        }
    }
}