using CleanSample.Application.Products.Dto;
using CleanSample.Application.Products.Requests;
using CleanSample.Presentation.FunctionApp.Authorization;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using FromBodyAttribute = Microsoft.Azure.Functions.Worker.Http.FromBodyAttribute;

namespace CleanSample.Presentation.FunctionApp.Functions.Api;

public class ProductFunction(ILogger<ProductFunction> logger, ISender mediator)
{
    [Authorize(AppRoles = [AppRoles.ProductsRead])]
    [Function(nameof(GetProduct))]
    public async Task<IActionResult> GetProduct(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "getproduct/{productId}")]
        HttpRequest req, Guid productId)
    {
        try
        {
            var productDto = await mediator.Send(new GetProductRequest(productId));
            return productDto == null ? new NotFoundResult() : new OkObjectResult(productDto);
        }
        catch (Exception ex) when (ex is ValidationException or ArgumentException)
        {
            logger.LogError(ex, ex.Message);
            return new BadRequestObjectResult(new { message = ex.Message });
        }
    }

    [Authorize(AppRoles = [AppRoles.ProductsChange])]
    [Function(nameof(AddProduct))]
    public async Task<IActionResult> AddProduct(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "addproduct")]
        HttpRequest req, [FromBody] ProductDto productDto)
    {
        try
        {
            await mediator.Send(new AddProductRequest(productDto));
            return new OkResult();
        }
        catch (Exception ex) when (ex is ValidationException or ArgumentException)
        {
            logger.LogError(ex, ex.Message);
            return new BadRequestObjectResult(new { message = ex.Message });
        }
    }
}