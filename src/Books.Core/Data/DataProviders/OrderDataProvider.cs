using Books.Core.Data.DataProviders.Abstractions;
using Books.Core.Data.Models;
using static System.Reflection.Metadata.BlobBuilder;

namespace Books.Core.Data.DataProviders;

internal class OrderDataProvider : IOrderDataProvider
{
    private static List<Order> orders = new List<Order>();
    private static SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1);
    static OrderDataProvider()
    {
        for (uint i = 0; i < 100; i++)
        {
            orders.Add(new Order(Guid.NewGuid(), new List<OrderLine>()
            {
                new OrderLine(i, i + 1)
            }));
        }
    }
    public async Task<uint> Count(CancellationToken cancellationToken)
    {
        await semaphoreSlim.WaitAsync(cancellationToken);
        try
        {
            return (uint)orders.Count;
        }
        finally
        {
            semaphoreSlim.Release();
        }
    }

    public async Task<IList<Order>> GetOrders(uint page, uint pageSize, CancellationToken cancellationToken)
    {
        await semaphoreSlim.WaitAsync(cancellationToken);
        try
        {
            return orders.Skip((int)((page - 1) * pageSize)).Take((int)pageSize).Select(a=>new Order(a)).ToList();
        }
        finally
        {
            semaphoreSlim.Release();
        }
    }
}
