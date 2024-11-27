
namespace Books.Core.Data.Models;

internal record Book(uint BookId, string Title, decimal Price, uint Bookstand, uint Shelf, IReadOnlyCollection<Author> Authors)
{
    public Book(Book book)
    {
        this.Bookstand = book.Bookstand;
        this.BookId = book.BookId;
        this.Title = book.Title;
        this.Price = book.Price;
        this.Shelf = book.Shelf;
        this.Authors = book.Authors.Select(a => new Author(a)).ToList();
    }

    private Book() : this(default, default, default, default, default, default) { }
}
