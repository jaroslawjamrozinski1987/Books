namespace Books.Core.Exceptions;

internal class OrderAlreadyExistsException : EntityAlreadyExistsException<ulong>
{
    public OrderAlreadyExistsException(ulong id) : base(id, "order already exists")
    {
    }
}
