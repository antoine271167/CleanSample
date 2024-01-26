using CleanSample.Application.Products.Commands;
using CleanSample.Application.Products.Dto;
using CleanSample.Application.Products.Processors;
using CleanSample.Application.Products.Requests;
using CleanSample.Application.Products.Validators;
using FluentValidation;
using MediatR;
using NSubstitute;

namespace CleanSample.Application.Tests.Products.Processors;

public class AddProductProcessorTests
{
    private readonly Fixture _fixture = new();
    private readonly IMediator _mediator = Substitute.For<IMediator>();
    private readonly AddProductRequestValidator _validator = new();

    [Fact]
    public async Task Handle_ShouldCallMediator()
    {
        // Arrange
        var request = _fixture.Create<AddProductRequest>();

        var sut = new AddProductProcessor(_mediator, _validator);

        // Act
        await sut.Handle(request, CancellationToken.None);

        // Assert
        await _mediator.Received(1).Send(Arg.Is<AddProductCommand>(command =>
            command.Product == request.Product));
    }

    [Fact]
    public async Task Handle_WithInvalidInput_ShouldFail()
    {
        // Arrange
        var request = _fixture.Build<AddProductRequest>()
            .FromFactory<Guid>(id => new AddProductRequest(new ProductDto(id, string.Empty)))
            .Create();

        var sut = new AddProductProcessor(_mediator, _validator);

        // Act
        var actual = () => sut.Handle(request, CancellationToken.None);

        // Assert
        await actual.Should().ThrowAsync<ValidationException>();
    }
}