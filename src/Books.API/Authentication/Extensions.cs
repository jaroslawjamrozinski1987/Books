using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Books.API.Authentication
{
    public static class Extensions
    {
        public static IServiceCollection AddAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
         .AddJwtBearer(options =>
         {
             options.Events = new JwtBearerEvents
             {
         OnMessageReceived = context =>
         {
             var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

             if (string.IsNullOrEmpty(token))
             {
                 context.NoResult();
             }

             return Task.CompletedTask;
         },
         OnChallenge = context =>
         {
             context.HandleResponse();
             context.Response.StatusCode = StatusCodes.Status401Unauthorized;
             context.Response.ContentType = "application/json";
             return context.Response.WriteAsync("{\"error\":\"Bearer token is required\"}");
         }
     };

     options.TokenValidationParameters = new TokenValidationParameters
     {
         ValidateIssuer = false,
         ValidateAudience = false,
         ValidateLifetime = false,
         ValidateIssuerSigningKey = false,
     };
 });

     return services;
}
    }
}
