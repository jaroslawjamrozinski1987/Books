using AutoMapper;
using Books.Core.Data.DataProviders.Abstractions;
using Books.Core.Data.Models;
using Books.Core.DTO;
using Books.Core.Repositories.Abstractions;

namespace Books.Core.Data.Repositories;

internal class BookRepository : IBookRepository
{
    private static SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);
    private readonly IBookDataProvider _bookDataProvider;
    private readonly IMapper _mapper;
    public BookRepository(IBookDataProvider bookDataProvider, IMapper mapper)
    {
        _bookDataProvider = bookDataProvider;
        _mapper = mapper;
    }
    public Task AddBook(BookDTO book, CancellationToken cancellationToken)
    => _bookDataProvider.AddBook(_mapper.Map<Book>(book), cancellationToken);

    public async Task<AddOrUpdateResult> AddOrUpdate(BookDTO book, CancellationToken cancellationToken)
    {
        await semaphore.WaitAsync(cancellationToken);
        try
        {
            var currentItem = await GetBookById(book.Id, cancellationToken);

            if(currentItem is null)
            {
                await AddBook(book, cancellationToken);
                return AddOrUpdateResult.Added;
            }
            else
            {
                await UpdateBook(book, cancellationToken);
                return AddOrUpdateResult.Updated;
            }
        }
        finally
        {
            semaphore.Release();
        }
    }

    public Task<uint> Count(CancellationToken cancellationToken)
     => _bookDataProvider.Count(cancellationToken);

    public async Task<BookDTO> GetBookById(uint id, CancellationToken cancellationToken)
    => _mapper.Map<BookDTO>(await _bookDataProvider.GetBookById(id, cancellationToken));

    public async Task<Results<BookDTO>> GetBooks(uint page, uint count, CancellationToken cancellationToken)
    {
        await semaphore.WaitAsync(cancellationToken);
        try
        {
            var books = await _bookDataProvider.GetBooks(page, count, cancellationToken);
            var data = books.Select(_mapper.Map<BookDTO>).ToList();

            uint totalCount = 0;
            if (page == 1)
            {
                totalCount = await Count(cancellationToken);
            }

            return new Results<BookDTO>(data, totalCount);
        }finally
        {
            semaphore.Release();
        }
    }

    public async Task UpdateBook(BookDTO book, CancellationToken cancellationToken)
    {
        var bookToUpdate = new Book(_mapper.Map<Book>( book));
        await _bookDataProvider.UpdateBook(bookToUpdate, cancellationToken);
    }
}
