using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

/// <summary>
/// Profile for mapping between Application and API CreateSale responses
/// </summary>
public class CreateSaleProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateSale feature
    /// </summary>
    public CreateSaleProfile()
    {
        // Map from CreateSaleRequest to CreateSaleCommand
        CreateMap<CreateSaleRequest, CreateSaleCommand>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

        // Map from CreateSaleResult to CreateSaleResponse
        CreateMap<CreateSaleResult, CreateSaleResponse>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

        // Map from CreateSaleItemRequest to CreateSaleItemCommand
        CreateMap<CreateSaleItemRequest, CreateSaleItemCommand>();

        // Map from CreateSaleItemResult to CreateSaleItemResponse
        CreateMap<CreateSaleItemResult, CreateSaleItemResponse>();
    }
}

