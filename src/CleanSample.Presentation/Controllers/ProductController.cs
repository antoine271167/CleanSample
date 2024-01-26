using CleanSample.Application.Products.Dto;
using CleanSample.Application.Products.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanSample.Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController(ISender mediator) : ControllerBase
{
    [HttpGet]
    public Task<ProductDto> Get(Guid id) =>
        mediator.Send(new GetProductRequest(id));

    [HttpPost]
    public Task Add(ProductDto product) =>
        mediator.Send(new AddProductRequest(product));
}