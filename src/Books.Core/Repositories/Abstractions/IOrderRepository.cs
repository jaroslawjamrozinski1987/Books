using Books.Core.DTO;

namespace Books.Core.Repositories.Abstractions;

internal interface IOrderRepository
{
    Task<Results<OrderDTO>> GetOrders(uint page, uint count, CancellationToken cancellationToken);
    Task<uint> Count(CancellationToken cancellationToken);
}
