
namespace Books.Core.DTO;

internal record BookDTO(uint Id, string Title, decimal Price, uint Bookstand, uint Shelf, IReadOnlyCollection<AuthorDTO> Authors)
{
    private BookDTO() : this(default, default, default, default, default, default) { }
}
