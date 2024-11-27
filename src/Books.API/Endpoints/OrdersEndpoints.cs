using Microsoft.AspNetCore.Mvc;
using Books.Core.Repositories.Abstractions;

namespace Books.API.Endpoints
{
    public static class OrdersEndpoints
    {
        public static void AddOrderEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/orders/{page}/{count}", async ([FromRoute] uint page, [FromRoute] uint count, [FromServices] IOrderRepository orderRepository, [FromServices] IHttpContextAccessor httpContextAccessor, CancellationToken cancellationToken = default) =>
            {
                var result = await orderRepository.GetOrders(page, count, cancellationToken);
                httpContextAccessor.HttpContext.Response.Headers.Add("X-Total-Count", result.Total.ToString());
                return Results.Ok(result.Data);
            });
        }
    }
}
