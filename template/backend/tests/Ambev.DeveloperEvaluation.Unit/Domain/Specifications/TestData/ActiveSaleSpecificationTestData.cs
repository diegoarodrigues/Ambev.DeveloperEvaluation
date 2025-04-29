using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Specifications.TestData;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation for ActiveSaleSpecification tests
/// to ensure consistency across test cases.
/// </summary>
public static class ActiveSaleSpecificationTestData
{
    /// <summary>
    /// Configures the Faker to generate valid Sale entities.
    /// The generated sales will have valid:
    /// - SaleNumber (unique identifier)
    /// - Customer (randomized customer name)
    /// - TotalAmount (randomized positive value)
    /// - CreatedAt (current or past date)
    /// - IsCancelled (set to false by default)
    /// IsCancelled is the main test parameter and can be overridden.
    /// </summary>
    private static readonly Faker<Sale> saleFaker = new Faker<Sale>()
        .CustomInstantiator(f => new Sale
        {
            SaleNumber = f.Random.Guid().ToString(),
            Customer = f.Person.FullName,
            TotalAmount = f.Finance.Amount(10, 1000),
            CreatedAt = f.Date.Past(1),
            IsCancelled = false // Default to active sales
        });

    /// <summary>
    /// Generates a valid Sale entity with the specified cancellation status.
    /// </summary>
    /// <param name="isCancelled">The cancellation status to set for the generated sale.</param>
    /// <returns>A valid Sale entity with randomly generated data and specified cancellation status.</returns>
    public static Sale GenerateSale(bool isCancelled)
    {
        var sale = saleFaker.Generate();
        sale.IsCancelled = isCancelled;
        return sale;
    }
}
