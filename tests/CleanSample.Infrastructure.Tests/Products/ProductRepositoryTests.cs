using CleanSample.Domain;
using CleanSample.Infrastructure.CloudStorage;
using CleanSample.Infrastructure.Products;
using NSubstitute;

namespace CleanSample.Infrastructure.Tests.Products;

public class ProductRepositoryTests
{
    private readonly ICloudStorageClient<Product, Guid> _client = Substitute.For<ICloudStorageClient<Product, Guid>>();
    private readonly Fixture _fixture = new();

    [Fact]
    public async Task GetProductAsync_ShouldCallClient()
    {
        // Arrange
        var expected = _fixture.Create<Product>();
        _client.GetEntityAsync(expected.Id, Arg.Any<CancellationToken>()).Returns(expected);

        var sut = new ProductRepository(_client);

        // Act
        var actual = await sut.GetProductAsync(expected.Id, CancellationToken.None);

        // Assert
        actual.Should().Be(expected);
        await _client.Received(1).GetEntityAsync(Arg.Is<Guid>(productId => productId == expected.Id),
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task AddProductAsync_ShouldCallClient()
    {
        // Arrange
        var expected = _fixture.Create<Product>();
        var sut = new ProductRepository(_client);

        // Act
        await sut.AddProductAsync(expected, CancellationToken.None);

        // Assert
        await _client.Received(1).AddEntityAsync(Arg.Is<Product>(product =>
            product.Id == expected.Id &&
            product.Name == expected.Name), Arg.Any<CancellationToken>());
    }
}