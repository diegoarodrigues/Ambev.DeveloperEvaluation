using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;
using NSubstitute.ReceivedExtensions;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

/// <summary>
/// Provides methods for generating test data for User and Sale entities.
/// This class centralizes test data generation for scenarios involving user sales.
/// </summary>
public static class UserSaleData
{
    /// <summary>
    /// Configures the Faker to generate valid Sale entities.
    /// The generated sales will have valid:
    /// - SaleId (unique identifier)
    /// - UserId (linked to a user)
    /// - TotalAmount (randomized value)
    /// - CreatedAt (current or past date)
    /// - Items (list of sale items)
    /// </summary>
    private static readonly Faker<Sale> SaleFaker = new Faker<Sale>()
        .RuleFor(s => s.Id, f => Guid.NewGuid())
        .RuleFor(s => s.Customer, f => f.Person.FullName)
        .RuleFor(s => s.TotalAmount, f => f.Finance.Amount(10, 1000))
        .RuleFor(s => s.CreatedAt, f => f.Date.Past(1))
        .RuleFor(s => s.Items, f => SaleItemFaker.Generate(f.Random.Int(1, 5)))
        .RuleFor(s => s.SaleNumber, f => "Diego costumer test")
        .RuleFor(s => s.Branch, f => "branch test test");

    /// <summary>
    /// Configures the Faker to generate valid SaleItem entities.
    /// The generated sale items will have valid:
    /// - ProductName (randomized product names)
    /// - Quantity (randomized quantity)
    /// - UnitPrice (randomized price per unit)
    /// </summary>
    private static readonly Faker<SaleItem> SaleItemFaker = new Faker<SaleItem>()
        .RuleFor(i => i.ProductId, f => Guid.NewGuid())
        .RuleFor(i => i.Quantity, f => f.Random.Int(1, 10))
        .RuleFor(i => i.UnitPrice, f => f.Finance.Amount(1, 100))
        .RuleFor(i => i.SaleId, f => Guid.NewGuid());

    /// <summary>
    /// Generates a valid Sale entity with randomized data.
    /// The generated sale will have all properties populated with valid values.
    /// </summary>
    /// <returns>A valid Sale entity with randomly generated data.</returns>
    public static Sale GenerateValidSale()
    {
        return SaleFaker.Generate();
    }

    /// <summary>
    /// Generates a list of valid Sale entities.
    /// Useful for testing scenarios involving multiple sales.
    /// </summary>
    /// <param name="count">The number of sales to generate.</param>
    /// <returns>A list of valid Sale entities.</returns>
    public static List<Sale> GenerateValidSales(int count)
    {
        return SaleFaker.Generate(count);
    }

    /// <summary>
    /// Generates a valid SaleItem entity with randomized data.
    /// The generated sale item will have all properties populated with valid values.
    /// </summary>
    /// <returns>A valid SaleItem entity with randomly generated data.</returns>
    public static SaleItem GenerateValidSaleItem()
    {
        return SaleItemFaker.Generate();
    }

    /// <summary>
    /// Generates a list of valid SaleItem entities.
    /// Useful for testing scenarios involving multiple sale items.
    /// </summary>
    /// <param name="count">The number of sale items to generate.</param>
    /// <returns>A list of valid SaleItem entities.</returns>
    public static List<SaleItem> GenerateValidSaleItems(int count)
    {
        return SaleItemFaker.Generate(count);
    }
}
