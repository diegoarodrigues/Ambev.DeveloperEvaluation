using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Services;

/// <summary>
/// Interface for SaleDomainService to encapsulate business rules for sales.
/// </summary>
public interface ISaleDomainService
{
    /// <summary>
    /// Applies discounts to the items in the sale.
    /// </summary>
    /// <param name="sale">The sale entity.</param>
    void ApplyDiscounts(Sale sale);

    /// <summary>
    /// Validates the sale and its items.
    /// </summary>
    /// <param name="sale">The sale entity.</param>
    void ValidateSale(Sale sale);
}
