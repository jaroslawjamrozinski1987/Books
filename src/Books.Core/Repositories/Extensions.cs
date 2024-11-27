using Books.Core.Data.Repositories;
using Books.Core.Repositories.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Books.Core.Repositories
{
    public static class Extensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IBookRepository, BookRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            return services;
        }
    }
}
