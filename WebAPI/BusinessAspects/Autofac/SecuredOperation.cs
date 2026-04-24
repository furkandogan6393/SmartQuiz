using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Core.Extensions;
// Buradaki namespace'i kendi WebAPI veya Business klasör yapına göre güncelle
namespace WebAPI.BusinessAspects.Autofac
{
    public class SecuredOperation : MethodInterception
    {
        private string[] _roles;
        private IHttpContextAccessor _httpContextAccessor;

        public SecuredOperation(string roles)
        {
            _roles = roles.Split(',');
            // ServiceTool, Core katmanındaki IoC içinde servisleri yakalamamızı sağlar
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        protected override void OnBefore(IInvocation invocation)
        {
            var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();
            foreach (var role in _roles)
            {
                if (roleClaims.Contains(role))
                {
                    return; // Yetki bulundu, metot çalışmaya devam edebilir.
                }
            }
            // Messages.AuthorizationDenied gelmiyorsa direkt string de yazabilirsin: "Yetkiniz yok"
            throw new Exception("Yetkiniz yok.");
        }
    }
}