using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Profile for mapping between Sale entity and CreateSaleResponse
/// </summary>
public class CreateSaleProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateSale operation
    /// </summary>
    public CreateSaleProfile()
    {
        // Map from CreateSaleCommand to Sale entity
        CreateMap<CreateSaleCommand, Sale>().ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

        // Map from Sale entity to CreateSaleResult
        CreateMap<Sale, CreateSaleResult>().ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

        // Map from CreateSaleItemCommand to SaleItem entity
        CreateMap<CreateSaleItemCommand, SaleItem>();

        // Map from SaleItem entity to CreateSaleItemResult
        CreateMap<SaleItem, CreateSaleItemResult>();
    }
}
