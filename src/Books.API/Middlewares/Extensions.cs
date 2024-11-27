namespace Books.API.Middlewares
{
    public static class Extensions
    {
        internal static IServiceCollection AddExceptions(this IServiceCollection services)
        {
            services.AddTransient<ExceptionHandlingMiddleware>();
            services.AddTransient<CheckBearerTokenMiddleware>();

            return services;
        }

        public static WebApplication UseExceptions(this WebApplication app)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseMiddleware<CheckBearerTokenMiddleware>();

            return app;
        }
    }
}
