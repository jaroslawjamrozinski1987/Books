namespace Books.Core.Exceptions;

internal class OrderNotFoundException : EntityNotFoundException<ulong>
{
    public OrderNotFoundException(ulong id) : base(id, "order not found")
    {
    }
}
