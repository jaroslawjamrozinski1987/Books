using Books.API;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiDependencies(builder.Configuration);

var app = builder.Build();

app.UseApiDependencies();

app.Run();
