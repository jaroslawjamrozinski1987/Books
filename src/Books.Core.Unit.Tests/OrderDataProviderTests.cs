using Books.Core.Data.DataProviders;
using Shouldly;
using Xunit;

namespace Books.Core.Unit.Tests
{
    public class OrderDataProviderTests
    {
        private readonly OrderDataProvider _orderDataProvider;

        public OrderDataProviderTests()
        {
            _orderDataProvider = new OrderDataProvider();
        }

        [Fact]
        public async Task Count_Should_Return_Total_Number_Of_Orders()
        {
            // Arrange
            var cancellationToken = CancellationToken.None;

            // Act
            var count = await _orderDataProvider.Count(cancellationToken);

            // Assert
            Assert.Equal<ulong>(100, count);
        }

        [Fact]
        public async Task GetOrders_Should_Return_Correct_Page_Of_Orders()
        {
            // Arrange
            uint page = 1;
            uint pageSize = 10;
            var cancellationToken = CancellationToken.None;

            // Act
            var orders = await _orderDataProvider.GetOrders(page, pageSize, cancellationToken);

            // Assert

            Assert.Equal(10, orders.Count);
        }

        [Fact]
        public async Task GetOrders_Should_Handle_Empty_Page_Gracefully()
        {
            // Arrange
            uint page = 11; // Beyond the number of orders (100 / 10 = 10 pages)
            uint pageSize = 10;
            var cancellationToken = CancellationToken.None;

            // Act
            var orders = await _orderDataProvider.GetOrders(page, pageSize, cancellationToken);

            // Assert
            Assert.Empty(orders);
        }
    }
}
