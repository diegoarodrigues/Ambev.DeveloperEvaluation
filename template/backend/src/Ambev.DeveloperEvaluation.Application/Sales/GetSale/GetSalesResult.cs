using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using System.Numerics;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSales;

/// <summary>
/// Response model for GetSales operation
/// </summary>
public class GetSalesResult
{
    public List<GetSaleResult> Items { get; set; }

    public int TotalCount { get; set; }
}

