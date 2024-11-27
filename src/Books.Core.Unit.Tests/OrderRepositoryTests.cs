using AutoMapper;
using Books.Core.Data.DataProviders.Abstractions;
using Books.Core.Data.Models;
using Books.Core.Data.Repositories;
using Books.Core.DTO;
using Books.Core.Repositories.Abstractions;
using Moq;
using Xunit;

namespace Books.Core.Unit.Tests;

public class OrderRepositoryTests
{
    private readonly Mock<IOrderDataProvider> _orderDataProviderMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly IOrderRepository _orderRepository;

    public OrderRepositoryTests()
    {
        _orderDataProviderMock = new Mock<IOrderDataProvider>();
        _mapperMock = new Mock<IMapper>();
        _orderRepository = new OrderRepository(_orderDataProviderMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Count_ShouldReturnTotalOrdersCount()
    {
        // Arrange
        var expectedCount = 100u;
        _orderDataProviderMock
            .Setup(provider => provider.Count(It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedCount);

        // Act
        var actualCount = await _orderRepository.Count(CancellationToken.None);

        // Assert
        Assert.Equal(expectedCount, actualCount);
        _orderDataProviderMock.Verify(provider => provider.Count(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetOrders_ShouldReturnMappedResults_WhenPageIsOne()
    {
        // Arrange
        var page = 1u;
        var count = 10u;
        var totalOrdersCount = 100u;
        var orders = new List<Order>
    {
        new Order (Guid.NewGuid(), new List<OrderLine>()
        {
            new OrderLine(1, 1)
        }),
        new Order (Guid.NewGuid(), new List<OrderLine>()
        {
            new OrderLine(1, 1)
        }),
    };
        var expectedMappedOrders = orders.Select(a => new Order(a)).ToList();

        _orderDataProviderMock
            .Setup(provider => provider.GetOrders(page, count, It.IsAny<CancellationToken>()))
            .ReturnsAsync(orders);

        _orderDataProviderMock
            .Setup(provider => provider.Count(It.IsAny<CancellationToken>()))
            .ReturnsAsync(totalOrdersCount);

        _mapperMock
            .Setup(mapper => mapper.Map<OrderDTO>(It.IsAny<Order>()))
            .Returns((Order source) => new OrderDTO(source.OrderId, source.OrderLines.Select(a=>new OrderLineDTO(a.BookId, a.Quantity)).ToList()));

        // Act
        var results = await _orderRepository.GetOrders(page, count, CancellationToken.None);

        // Assert
        Assert.NotNull(results);
        Assert.Equal(expectedMappedOrders.Count, results.Data.Count);
        Assert.Equal(totalOrdersCount, results.Total);
        _orderDataProviderMock.Verify(provider => provider.GetOrders(page, count, It.IsAny<CancellationToken>()), Times.Once);
        _orderDataProviderMock.Verify(provider => provider.Count(It.IsAny<CancellationToken>()), Times.Once);
        _mapperMock.Verify(mapper => mapper.Map<OrderDTO>(It.IsAny<Order>()), Times.Exactly(orders.Count));
    }

}
