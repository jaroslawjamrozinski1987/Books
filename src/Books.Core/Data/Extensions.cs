using Books.Core.Data.DataProviders;
using Books.Core.Data.DataProviders.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Books.Core.Data
{
    public static class Extensions
    {
        public static IServiceCollection AddDataProviders(this IServiceCollection services)
        {
            services.AddTransient<IBookDataProvider, BookDataProvider>();
            services.AddTransient<IOrderDataProvider, OrderDataProvider>();

            return services;
        }
    }
}
