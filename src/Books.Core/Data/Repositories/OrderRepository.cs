using AutoMapper;
using Books.Core.Data.DataProviders.Abstractions;
using Books.Core.DTO;
using Books.Core.Repositories.Abstractions;

namespace Books.Core.Data.Repositories;

internal class OrderRepository : IOrderRepository
{
    private static SemaphoreSlim readerWriterLockSlim = new SemaphoreSlim(1, 1);
    private readonly IOrderDataProvider _orderDataProvider;
    private readonly IMapper _mapper;
    public OrderRepository(IOrderDataProvider orderDataProvider, IMapper mapper)
    {
        _orderDataProvider = orderDataProvider;
        _mapper = mapper;
    }

    public Task<uint> Count(CancellationToken cancellationToken)
    => _orderDataProvider.Count(cancellationToken);

    public async Task<Results<OrderDTO>> GetOrders(uint page, uint count, CancellationToken cancellationToken)
    {
        await readerWriterLockSlim.WaitAsync(cancellationToken);
        try
        {
            var orders = await _orderDataProvider.GetOrders(page, count, cancellationToken);
            var mappedData = orders.Select(_mapper.Map<OrderDTO>).ToList();
            uint totalCount = 0;
            if (page == 1)
            {
                totalCount = await Count(cancellationToken);
            }
            return new Results<OrderDTO>(mappedData, totalCount);
        }
        finally
        {
            readerWriterLockSlim.Release();
        }        
    }
}
