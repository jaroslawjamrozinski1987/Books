namespace Books.Core.DTO;

internal record OrderDTO(Guid OrderId, IReadOnlyCollection<OrderLineDTO> OrderLines)
{
    private OrderDTO() : this(default, default) { }
}
