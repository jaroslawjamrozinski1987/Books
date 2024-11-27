using Books.Core.Auth;
using Books.Core.Repositories;
using Books.Core.Services;
using Books.Core.Mapper;
using Books.API.Endpoints;
using Books.Core.Data;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Books.API.Middlewares;
using Books.API.Swagger;

namespace Books.API;
public static class Extensions
{
    public static void AddApiDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwagger(configuration);

        services.AddRepositories();
        services.AddServices();
        services.AddDataProviders();
        services.AddMapping();
        services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddExceptions();
    }

    public static WebApplication UseApiDependencies(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.AddEndpoints();
        app.UseExceptions();

        return app;
    }


}
