using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData;

/// <summary>
/// Provides methods for generating test data for CreateSaleHandler using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class CreateSaleHandlerTestData
{
    /// <summary>
    /// Configures the Faker to generate valid Sale entities.
    /// The generated sales will have valid:
    /// - SaleNumber (random alphanumeric string)
    /// - Date (current or past date)
    /// - Customer (random name)
    /// - Branch (random branch name)
    /// - Items (list of valid sale items)
    /// </summary>
    private static readonly Faker<CreateSaleCommand> createSaleHandlerFaker = new Faker<CreateSaleCommand>()
        .RuleFor(s => s.SaleNumber, f => f.Random.AlphaNumeric(10))
        .RuleFor(s => s.Date, f => f.Date.Past())
        .RuleFor(s => s.Customer, f => f.Person.FullName)
        .RuleFor(s => s.Branch, f => f.Company.CompanyName())
        .RuleFor(s => s.Items, f => CreateSaleItemTestData.GenerateValidItems());

    /// <summary>
    /// Generates a valid Sale entity with randomized data.
    /// The generated sale will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Sale entity with randomly generated data.</returns>
    public static CreateSaleCommand GenerateValidCommand()
    {
        return createSaleHandlerFaker.Generate();
    }
}

/// <summary>
/// Provides methods for generating test data for sale items.
/// </summary>
public static class CreateSaleItemTestData
{
    /// <summary>
    /// Configures the Faker to generate valid SaleItem entities.
    /// The generated sale items will have valid:
    /// - ProductId (random GUID)
    /// - Quantity (random number between 1 and 20)
    /// - UnitPrice (random decimal value)
    /// </summary>
    private static readonly Faker<CreateSaleItemCommand> createSaleItemFaker = new Faker<CreateSaleItemCommand>()
        .RuleFor(i => i.ProductId, f => f.Random.Guid())
        .RuleFor(i => i.Quantity, f => f.Random.Int(1, 20))
        .RuleFor(i => i.UnitPrice, f => f.Finance.Amount(1, 100));

    /// <summary>
    /// Generates a list of valid SaleItem entities with randomized data.
    /// </summary>
    /// <param name="count">The number of items to generate.</param>
    /// <returns>A list of valid SaleItem entities.</returns>
    public static List<CreateSaleItemCommand> GenerateValidItems(int count = 3)
    {
        return createSaleItemFaker.Generate(count);
    }
}
