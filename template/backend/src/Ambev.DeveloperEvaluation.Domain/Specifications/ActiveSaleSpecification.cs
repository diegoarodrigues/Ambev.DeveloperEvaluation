using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Specifications;

/// <summary>
/// Specification to determine if a Sale is active.
/// A Sale is considered active if it is not cancelled.
/// </summary>
public class ActiveSaleSpecification : ISpecification<Sale>
{
    /// <summary>
    /// Determines whether the specified Sale satisfies the active condition.
    /// </summary>
    /// <param name="sale">The Sale entity to evaluate.</param>
    /// <returns>True if the Sale is active (not cancelled); otherwise, false.</returns>
    public bool IsSatisfiedBy(Sale sale)
    {
        return !sale.IsCancelled;
    }
}
