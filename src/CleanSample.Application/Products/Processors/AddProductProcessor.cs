using CleanSample.Application.Products.Commands;
using CleanSample.Application.Products.Requests;
using FluentValidation;
using MediatR;

namespace CleanSample.Application.Products.Processors;

public class AddProductProcessor(ISender mediator, IValidator<AddProductRequest> validator)
    : IRequestHandler<AddProductRequest>
{
    public Task Handle(AddProductRequest request, CancellationToken cancellationToken)
    {
        var validationResult = validator.Validate(request);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        return mediator.Send(new AddProductCommand(request.Product), cancellationToken);
    }
}