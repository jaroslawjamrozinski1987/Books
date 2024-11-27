using Books.Core.Data.DataProviders.Abstractions;
using Books.Core.Data.Models;
using Books.Core.Exceptions;

namespace Books.Core.Data.DataProviders;

internal class BookDataProvider : IBookDataProvider
{
    private static List<Book> books = new List<Book>();
    private static SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1);

    static BookDataProvider()
    {
        semaphoreSlim.Wait();
        try
        {
            for (int i = 0; i < 100; i++)
            {
                books.Add(new Book((uint)i, $"Test${i}", 10.0m, 1, 1, new List<Author> { new Author($"firstname_{i}", $"lastname_{i}") }));
            }
        }
        finally
        {
            semaphoreSlim.Release();
        }
    }

    public async Task<uint> AddBook(Book book, CancellationToken cancellationToken)
    {
        await semaphoreSlim.WaitAsync(cancellationToken);
        try
        {
           var bookToFind = books.FirstOrDefault(a => a.BookId == book.BookId);

            if (bookToFind is not null)
            {
               throw new BookAlreadyExistsException(book.BookId);
            }

            var id = GetNextId();
            var newBook = new Book(id, book.Title ?? "", book.Price, book.Bookstand, book.Shelf, book.Authors);
            books.Add(newBook);
            return newBook.BookId;
        }
        finally
        {
            semaphoreSlim.Release();
        }
    }

    public async Task<uint> Count(CancellationToken cancellationToken)
    {
        await semaphoreSlim.WaitAsync(cancellationToken);
        try
        {
            return (uint)books.Count;
        }
        finally
        {
            semaphoreSlim.Release();
        }
    }

    public async Task<Book> GetBookById(uint bookId, CancellationToken cancellationToken)
    {
        await semaphoreSlim.WaitAsync(cancellationToken);
        try
        {
           var book = books.FirstOrDefault(a => a.BookId == bookId);

            if(book is null)
            {
                return null;
            }

            return new Book(book);
        }
        finally
        {
            semaphoreSlim.Release();
        }
    }

    public async Task<IList<Book>> GetBooks(uint page, uint pageSize, CancellationToken cancellationToken)
    {
        await semaphoreSlim.WaitAsync(cancellationToken);
        try
        {
            return books.Skip((int)((page - 1) * pageSize)).Take((int)pageSize).Select(a => new Book(a)).ToList();
        }
        finally
        {
            semaphoreSlim.Release();
        }
    }

    public async Task UpdateBook(Book book, CancellationToken cancellationToken)
    {
        await semaphoreSlim.WaitAsync(cancellationToken);
        try
        {
            var bookToUpdate = books.FirstOrDefault(a => a.BookId == book.BookId);

            if (bookToUpdate is null)
            {
                throw new BookNotFoundException(book.BookId);
            }

            var newBook = new Book(book.BookId, book.Title ?? "", book.Price, book.Bookstand, book.Shelf, book.Authors);

            var bookIndex = books.IndexOf(bookToUpdate);
            books[bookIndex] = newBook;
        }
        finally
        {
            semaphoreSlim.Release();
        }
    }

    private uint GetNextId()
    {
        if (!books.Any())
        {
            return 1;
        }

        var maxValue = books.Max(a => a.BookId);
        return maxValue + 1;
    }
}
