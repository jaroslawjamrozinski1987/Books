using Books.Core.Data.Models;
using Books.Core.DTO;

namespace Books.Core.Repositories.Abstractions;

internal interface IBookRepository
{
    Task<Results<BookDTO>> GetBooks(uint page, uint count, CancellationToken cancellationToken);
    Task<BookDTO> GetBookById(uint id, CancellationToken cancellationToken);
    Task AddBook(BookDTO book, CancellationToken cancellationToken);
    Task UpdateBook(BookDTO book, CancellationToken cancellationToken);
    Task<AddOrUpdateResult> AddOrUpdate(BookDTO book, CancellationToken cancellationToken);
    Task<uint> Count(CancellationToken cancellationToken);
}
