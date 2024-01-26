using AutoMapper;
using CleanSample.Application.Products.Dto;
using CleanSample.Domain;

namespace CleanSample.Application.Products.Mapping;

public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        this.CreateMap<Product, ProductDto>().ReverseMap();
    }
}