namespace Books.Core.Data.Models;

internal record OrderLine(uint BookId, uint Quantity)
{
    public OrderLine(OrderLine orderLine)
    {
        this.BookId = orderLine.BookId;
        this.Quantity = orderLine.Quantity;
    }
}