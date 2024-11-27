using Books.Core.Data.Models;

namespace Books.Core.Data.DataProviders.Abstractions;

internal interface IBookDataProvider
{
    Task<IList<Book>> GetBooks(uint page, uint pageSize, CancellationToken cancellationToken);
    Task<Book> GetBookById(uint bookId, CancellationToken cancellationToken);
    Task<uint> Count( CancellationToken cancellationToken);
    Task<uint> AddBook(Book book, CancellationToken cancellationToken);
    Task UpdateBook(Book book, CancellationToken cancellationToken);
}
