using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

/// <summary>
/// Contains unit tests for the Sale entity class.
/// Tests cover validation scenarios and business rules.
/// </summary>
public class SaleTests
{
    /// <summary>
    /// Tests that validation passes when all sale properties are valid.
    /// </summary>
    [Fact(DisplayName = "Validation should pass for valid sale data")]
    public void Given_ValidSaleData_When_Validated_Then_ShouldReturnValid()
    {
        // Arrange
        var sale = UserSaleData.GenerateValidSale();

        // Act
        var result = sale.Validate();

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    /// <summary>
    /// Tests that validation fails when sale properties are invalid.
    /// </summary>
    [Fact(DisplayName = "Validation should fail for invalid sale data")]
    public void Given_InvalidSaleData_When_Validated_Then_ShouldReturnInvalid()
    {
        // Arrange
        var sale = new Sale
        {
            Customer = "", // Invalid: empty
            TotalAmount = -100, // Invalid: negative amount
            Items = new List<SaleItem> // Invalid: item with invalid data
            {
                new SaleItem
                {
                    ProductId = Guid.Empty, // Invalid: empty GUID
                    Quantity = 0, // Invalid: zero quantity
                    UnitPrice = -50 // Invalid: negative price
                }
            }
        };

        // Act
        var result = sale.Validate();

        // Assert
        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
    }

    /// <summary>
    /// Tests that a sale with no items is invalid.
    /// </summary>
    [Fact(DisplayName = "Validation should fail for sale with no items")]
    public void Given_SaleWithNoItems_When_Validated_Then_ShouldReturnInvalid()
    {
        // Arrange
        var sale = UserSaleData.GenerateValidSale();
        sale.Items = new List<SaleItem>(); // No items

        // Act
        var result = sale.Validate();

        // Assert
        Assert.False(result.IsValid);
        //Assert.Contains(result.Errors, error => error.Message.Contains("Items cannot be empty"));
        Assert.NotEmpty(result.Errors);
    }

    /// <summary>
    /// Tests that a sale with a negative total amount is invalid.
    /// </summary>
    [Fact(DisplayName = "Validation should fail for sale with negative total amount")]
    public void Given_SaleWithNegativeTotalAmount_When_Validated_Then_ShouldReturnInvalid()
    {
        // Arrange
        var sale = UserSaleData.GenerateValidSale();
        sale.TotalAmount = -100; // Negative total amount

        // Act
        var result = sale.Validate();

        // Assert
        Assert.False(result.IsValid);
        //Assert.Contains(result.Errors, error => error.Message.Contains("TotalAmount must be greater than zero"));
        Assert.NotEmpty(result.Errors);
    }

    /// <summary>
    /// Tests that a sale item with invalid data is flagged during validation.
    /// </summary>
    [Fact(DisplayName = "Validation should fail for sale with invalid item")]
    public void Given_SaleWithInvalidItem_When_Validated_Then_ShouldReturnInvalid()
    {
        // Arrange
        var sale = UserSaleData.GenerateValidSale();
        sale.Items[0].Quantity = 0; // Invalid: zero quantity

        // Act
        var result = sale.Validate();

        // Assert
        Assert.False(result.IsValid);
        //Assert.Contains(result.Errors, error => error.Message.Contains("Quantity must be greater than zero"));
        Assert.NotEmpty(result.Errors);
    }
}
