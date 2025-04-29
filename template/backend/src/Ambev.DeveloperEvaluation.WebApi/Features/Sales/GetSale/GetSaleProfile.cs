using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSales;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSales;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;

/// <summary>
/// Profile for mapping GetSale feature requests to commands
/// </summary>
public class GetSaleProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetSale feature
    /// </summary>
    public GetSaleProfile()
    {
        // Map from Guid to GetSaleCommand
        CreateMap<Guid, Application.Sales.GetSale.GetSaleCommand>()
            .ConstructUsing(id => new Application.Sales.GetSale.GetSaleCommand(id));

        // Map from GetSaleRequest to GetSaleCommand
        CreateMap<GetSaleRequest, Application.Sales.GetSale.GetSaleCommand>()
            .ConstructUsing(request => new Application.Sales.GetSale.GetSaleCommand(request.Id));

        // Map from GetSaleResult to GetSaleResponse
        CreateMap<GetSaleResult, GetSaleResponse>();

        // Map from GetSaleItemResult to GetSaleItemResponse
        CreateMap<GetSaleItemResult, GetSaleItemResponse>();

        
        CreateMap<GetSalesRequest, GetSalesCommand>();

        CreateMap<GetSalesResult, GetSalesResponse>();
    }
}

