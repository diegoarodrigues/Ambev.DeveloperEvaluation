using MediatR;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProducts;

/// <summary>
/// Request model for retrieving a list of products with pagination and ordering
/// </summary>
public class GetProductsRequest : IRequest<PaginatedList<GetProductResponse>>
{
    /// <summary>
    /// The page number for pagination (default: 1)
    /// </summary>
    public int Page { get; set; } = 1;

    /// <summary>
    /// The number of items per page for pagination (default: 10)
    /// </summary>
    public int Size { get; set; } = 10;

    /// <summary>
    /// The ordering of results (e.g., "price desc, title asc")
    /// </summary>
    public string? Order { get; set; }
}
