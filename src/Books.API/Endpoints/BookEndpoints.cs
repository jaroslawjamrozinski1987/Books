using Microsoft.AspNetCore.Mvc;
using Books.Core.Repositories.Abstractions;
using Books.Core.DTO;
namespace Books.API.Endpoints
{
    public static class BookEndpoints
    {
        const string BasePath = "/books";
        public static void AddBookEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/books/{page}/{count}", async ([FromRoute] uint page, [FromRoute] uint count, [FromServices] IBookRepository bookRepository, [FromServices] IHttpContextAccessor httpContextAccessor, CancellationToken cancellationToken = default) =>
            {
                var result = await bookRepository.GetBooks(page, count, cancellationToken);

                httpContextAccessor.HttpContext.Response.Headers.Add("X-Total-Count", result.Total.ToString());
                return Results.Ok(result.Data);
            });

            app.MapPost(BasePath, async ([FromBody] BookDTO book,  [FromServices] IBookRepository bookRepository, CancellationToken cancellationToken = default) =>
            {
                var result = await bookRepository.AddOrUpdate(book, cancellationToken);

                switch (result)
                {
                    case AddOrUpdateResult.Added:
                        return Results.StatusCode(StatusCodes.Status201Created);
                    case AddOrUpdateResult.Updated:
                        return Results.StatusCode(StatusCodes.Status204NoContent);
                    default:
                        return Results.StatusCode(StatusCodes.Status404NotFound);
                }
            });
        }
    }
}
