
namespace Books.Core.Exceptions;

internal class BookAlreadyExistsException : EntityAlreadyExistsException<ulong>
{
    public BookAlreadyExistsException(ulong id) : base(id, "book already exists")
    {
    }
}
