using CleanSample.Application.Products.Dto;
using CleanSample.Application.Products.Requests;
using CleanSample.Presentation.AppService.Controllers;
using MediatR;
using NSubstitute;

namespace CleanSample.Presentation.AppService.Tests.Controllers;

public class ProductControllerTests
{
    private readonly Fixture _fixture = new();
    private readonly IMediator _mediator = Substitute.For<IMediator>();

    [Fact]
    public async Task Get_ShouldCallMediator()
    {
        // Arrange
        var expected = _fixture.Build<ProductDto>()
            .FromFactory<string>(s => new ProductDto(_fixture.Create<Guid>(), s))
            .Create();

        _mediator.Send(Arg.Is<GetProductRequest>(request => request.ProductId == expected.Id))
            .Returns(expected);

        var sut = new ProductController(_mediator);

        // Act
        var actual = await sut.Get(expected.Id);

        // Assert
        actual.Should().Be(expected);
        await _mediator.Received(1).Send(Arg.Is<GetProductRequest>(request =>
            request.ProductId == expected.Id));
    }

    [Fact]
    public async Task Add_ShouldCallMediator()
    {
        // Arrange
        var expected = _fixture.Create<ProductDto>();

        var sut = new ProductController(_mediator);

        // Act
        await sut.Add(expected);

        // Assert
        await _mediator.Received(1).Send(Arg.Is<AddProductRequest>(request =>
            request.Product == expected));
    }
}