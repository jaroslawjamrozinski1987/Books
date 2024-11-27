
namespace Books.Core.Data.Models;

internal record Order(Guid OrderId, IReadOnlyCollection<OrderLine> OrderLines)
{
    public Order(Order order)
    {
        OrderId = order.OrderId;
        OrderLines = order.OrderLines.Select(a=>new OrderLine(a)).ToList();
    }
}