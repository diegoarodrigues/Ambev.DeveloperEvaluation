using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSales;

/// <summary>
/// Represents the request model for retrieving a list of sales.
/// </summary>
public class GetSalesRequest
{
    public string? Search { get; set; }

    /// <summary>
    /// The page number for pagination.
    /// </summary>
    public int Page { get; set; } = 1;

    /// <summary>
    /// The number of items per page for pagination.
    /// </summary>
    public int PageSize { get; set; } = 10;
}
