namespace Books.API.Endpoints
{
    public static class Extensions
    {
        public static void AddEndpoints(this IEndpointRouteBuilder app)
        {
            var groupItem = app.MapGroup("api");
            BookEndpoints.AddBookEndpoints(groupItem);
            OrdersEndpoints.AddOrderEndpoints(groupItem);
        }
    }
}
