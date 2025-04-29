using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;

/// <summary>
/// API response model for GetSale operation
/// </summary>
public class GetSalesResponse
{
    /// <summary>
    /// The page number for pagination.
    /// </summary>
    public int Page { get; set; } = 1;

    /// <summary>
    /// The number of items per page for pagination.
    /// </summary>
    public int PageSize { get; set; } = 10;

    public List<GetSaleResponse> Items { get; set; }
}