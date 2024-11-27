using Books.Core.Services.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Books.Core.Services
{
    public static class Extensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddCache();
            return services;
        }

        private static IServiceCollection AddCache(this IServiceCollection services)
        {
            services.AddTransient<ICacheService, CacheService>();
            return services;
        }
    }
}
