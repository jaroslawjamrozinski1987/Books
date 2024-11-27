using Books.Core.Data.DataProviders;
using Books.Core.Data.Models;
using Books.Core.Exceptions;
using Xunit;

namespace Books.Core.Unit.Tests;

public class BookDataProviderTests
{
    private readonly BookDataProvider _bookDataProvider;

    public BookDataProviderTests()
    {
        _bookDataProvider = new BookDataProvider();
    }

    [Fact]
    public async Task AddBook_ShouldAddBook_WhenBookIsNew()
    {
        // Arrange
        var newBook = new Book(101, "New Book", 20.0m, 2, 3, new List<Author> { new Author("John", "Doe") });

        // Act
        var addedBookId = await _bookDataProvider.AddBook(newBook, CancellationToken.None);

        // Assert
        Assert.Equal(100u, addedBookId);

        var addedBook = await _bookDataProvider.GetBookById(addedBookId, CancellationToken.None);
        Assert.NotNull(addedBook);
        Assert.Equal("New Book", addedBook.Title);
    }

    [Fact]
    public async Task AddBook_ShouldThrowException_WhenBookAlreadyExists()
    {
        // Arrange
        var existingBook = new Book(1, "Existing Book", 15.0m, 1, 1, new List<Author> { new Author("Jane", "Doe") });

        // Act & Assert
        await Assert.ThrowsAsync<BookAlreadyExistsException>(() =>
            _bookDataProvider.AddBook(existingBook, CancellationToken.None));
    }

    [Fact]
    public async Task GetBookById_ShouldReturnBook_WhenBookExists()
    {
        // Arrange
        var bookId = 1;

        // Act
        var book = await _bookDataProvider.GetBookById((uint)bookId, CancellationToken.None);

        // Assert
        Assert.NotNull(book);
        Assert.Equal((uint)bookId, book.BookId);
    }

    [Fact]
    public async Task GetBookById_ShouldReturnNull_WhenBookDoesNotExist()
    {
        // Arrange
        var nonExistentBookId = 999;

        // Act
        var book = await _bookDataProvider.GetBookById((uint)nonExistentBookId, CancellationToken.None);

        // Assert
        Assert.Null(book);
    }

    [Fact]
    public async Task UpdateBook_ShouldUpdateBook_WhenBookExists()
    {
        // Arrange
        var updatedBook = new Book(1, "Updated Title", 12.5m, 3, 2, new List<Author> { new Author("Updated", "Author") });

        // Act
        await _bookDataProvider.UpdateBook(updatedBook, CancellationToken.None);

        // Assert
        var book = await _bookDataProvider.GetBookById(1, CancellationToken.None);
        Assert.NotNull(book);
        Assert.Equal("Updated Title", book.Title);
        Assert.Equal(12.5m, book.Price);
    }

    [Fact]
    public async Task UpdateBook_ShouldThrowException_WhenBookDoesNotExist()
    {
        // Arrange
        var nonExistentBook = new Book(999, "Non-Existent Book", 20.0m, 1, 1, new List<Author> { new Author("Non", "Existent") });

        // Act & Assert
        await Assert.ThrowsAsync<BookNotFoundException>(() =>
            _bookDataProvider.UpdateBook(nonExistentBook, CancellationToken.None));
    }

    [Fact]
    public async Task GetBooks_ShouldReturnPaginatedBooks()
    {
        // Arrange
        var page = 2u;
        var pageSize = 10u;

        // Act
        var books = await _bookDataProvider.GetBooks(page, pageSize, CancellationToken.None);

        // Assert
        Assert.Equal(10, books.Count);
        Assert.Equal((uint)10, books.First().BookId);
    }

    [Fact]
    public async Task Count_ShouldReturnTotalBookCount()
    {
        // Act
        var count = await _bookDataProvider.Count(CancellationToken.None);

        // Assert
        Assert.Equal(100u, count);
    }
}
