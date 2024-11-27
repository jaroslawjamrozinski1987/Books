using Microsoft.Extensions.DependencyInjection;

namespace Books.Core.Mapper
{
    public static class Extensions
    {
        public static IServiceCollection AddMapping(this IServiceCollection services) => services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    }
}
