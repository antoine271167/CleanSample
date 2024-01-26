namespace CleanSample.Domain.Tests;

public class ProductTests
{
    private readonly Fixture _fixture = new();

    [Fact]
    public void Ctr_WhenCalled_ProductShouldBeProperlyFilled()
    {
        // Arrange
        var productId = _fixture.Create<Guid>();
        var productName = _fixture.Create<string>();

        // Act
        var sut = new Product(productId, productName);

        // Assert
        sut.Id.Should().Be(productId);
        sut.Name.Should().Be(productName);
    }
}