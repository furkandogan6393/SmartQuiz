using Microsoft.AspNetCore.Builder;

namespace Core.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            // Burada kendi oluşturduğun ExceptionMiddleware class'ını çağırman gerekir.
            // app.UseMiddleware<ExceptionMiddleware>(); 
        }
    }
}