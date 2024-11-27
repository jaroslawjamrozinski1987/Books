using AutoMapper;
using Books.Core.Data.DataProviders.Abstractions;
using Books.Core.Data.Models;
using Books.Core.Data.Repositories;
using Books.Core.DTO;
using Moq;
using Shouldly;
using Xunit;

namespace Books.Core.Unit.Tests
{
    public class BookRepositoryTests
    {
        private readonly Mock<IMapper> _mapperMock;

        public BookRepositoryTests()
        {            
            _mapperMock = new Mock<IMapper>();
        }

        [Fact]
        public async Task AddBook_Should_Call_DataProvider_With_Mapped_Book()
        {
            // Arrange
            var bookDataProviderMock = new Mock<IBookDataProvider>();
            var repository = new BookRepository(bookDataProviderMock.Object, _mapperMock.Object);
            var bookDto = new BookDTO (1, "t", 1, 1, 1, new List<AuthorDTO>() { new AuthorDTO("a","b")});
            var book = new Book(1, "t", 2, 3, 4, new List<Author>() { new Author("a", "b") });
            var cancellationToken = CancellationToken.None;

            _mapperMock.Setup(m => m.Map<Book>(bookDto)).Returns(book);

            // Act
            await repository.AddBook(bookDto, cancellationToken);

            // Assert
            bookDataProviderMock.Verify(dp => dp.AddBook(book, cancellationToken), Times.Once);
        }

        [Fact]
        public async Task AddOrUpdate_Should_Add_Book_If_Not_Found()
        {
            // Arrange
            var bookDataProviderMock = new Mock<IBookDataProvider>();
            var repository = new BookRepository(bookDataProviderMock.Object, _mapperMock.Object);
            var bookDto = new BookDTO(1, "t", 1, 1, 1, new List<AuthorDTO>() { new AuthorDTO("a", "b") });
            var book = new Book(1, "t", 2, 3, 4, new List<Author>() { new Author("a", "b") });
            var cancellationToken = CancellationToken.None;

            bookDataProviderMock.Setup(dp => dp.GetBookById(bookDto.Id, cancellationToken))
                                 .ReturnsAsync((Book)null);

            _mapperMock.Setup(m => m.Map<Book>(bookDto)).Returns(book);

            // Act
            var result = await repository.AddOrUpdate(bookDto, cancellationToken);

            // Assert
            result.ShouldBe(AddOrUpdateResult.Added);
            bookDataProviderMock.Verify(dp => dp.AddBook(book, cancellationToken), Times.Once);
        }

        [Fact]
        public async Task AddOrUpdate_Should_Update_Book_If_Found()
        {
            // Arrange
            var bookDataProviderMock = new Mock<IBookDataProvider>();
            var repository = new BookRepository(bookDataProviderMock.Object, _mapperMock.Object);
            var bookDto = new BookDTO(1, "t", 1, 1, 1, new List<AuthorDTO>() { new AuthorDTO("a", "b") });
            var book = new Book(1, "t", 2, 3, 4, new List<Author>() { new Author("a", "b") });
            var cancellationToken = CancellationToken.None;

            bookDataProviderMock.Setup(dp => dp.GetBookById(bookDto.Id, cancellationToken))
                                 .ReturnsAsync((Book)book);

            _mapperMock.Setup(m => m.Map<Book>(bookDto)).Returns(book);
            _mapperMock.Setup(m => m.Map<BookDTO>(It.IsAny<Book>())).Returns(bookDto);
            // Act
            var result = await repository.AddOrUpdate(bookDto, cancellationToken);

            // Assert
            result.ShouldBe(AddOrUpdateResult.Updated);
            bookDataProviderMock.Verify(dp => dp.UpdateBook(It.Is<Book>(a=>a.BookId == book.BookId), cancellationToken), Times.Once);
        }
    }
}
