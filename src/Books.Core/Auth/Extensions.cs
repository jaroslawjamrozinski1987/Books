using Microsoft.Extensions.DependencyInjection;

namespace Books.Core.Auth;

public static class Extensions
{
    public static IServiceCollection AddAuth(this IServiceCollection services)
    {
        return services;
    }

}
