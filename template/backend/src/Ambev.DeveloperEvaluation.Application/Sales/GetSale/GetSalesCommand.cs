using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSales;

/// <summary>
/// Command for retrieving a list of sales.
/// </summary>
public class GetSalesCommand : IRequest<GetSalesResult>
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
