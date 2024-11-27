
namespace Books.Core.Exceptions
{
    internal class BookNotFoundException : EntityNotFoundException<ulong>
    {
        public BookNotFoundException(ulong id) : base(id, "book not found")
        {
        }
    }
}
