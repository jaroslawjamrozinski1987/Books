using Books.Core.Data.Models;

namespace Books.Core.Data.DataProviders.Abstractions;

internal interface IOrderDataProvider
{
    Task<IList<Order>> GetOrders(uint page, uint pageSize, CancellationToken cancellationToken);
    Task<uint> Count(CancellationToken cancellationToken);
}
