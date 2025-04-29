using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation;

/// <summary>
/// Contains unit tests for the SaleValidator class.
/// Tests cover validation of all sale properties including customer, total amount,
/// items, and created date.
/// </summary>
public class SaleValidatorTests
{
    private readonly SaleValidator _validator;

    public SaleValidatorTests()
    {
        _validator = new SaleValidator();
    }

    /// <summary>
    /// Tests that validation passes when all sale properties are valid.
    /// </summary>
    [Fact(DisplayName = "Valid sale should pass all validation rules")]
    public void Given_ValidSale_When_Validated_Then_ShouldNotHaveErrors()
    {
        // Arrange
        var sale = UserSaleData.GenerateValidSale();

        // Act
        var result = _validator.TestValidate(sale);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    /// <summary>
    /// Tests that validation fails for an empty customer.
    /// </summary>
    [Fact(DisplayName = "Empty customer should fail validation")]
    public void Given_EmptyCustomer_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var sale = UserSaleData.GenerateValidSale();
        sale.Customer = "";

        // Act
        var result = _validator.TestValidate(sale);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Customer);
    }

    /// <summary>
    /// Tests that validation fails for a negative total amount.
    /// </summary>
    [Fact(DisplayName = "Negative total amount should fail validation")]
    public void Given_NegativeTotalAmount_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var sale = UserSaleData.GenerateValidSale();
        sale.TotalAmount = -100;

        // Act
        var result = _validator.TestValidate(sale);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.TotalAmount);
    }

    /// <summary>
    /// Tests that validation fails when no items are present in the sale.
    /// </summary>
    [Fact(DisplayName = "Sale with no items should fail validation")]
    public void Given_NoItems_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var sale = UserSaleData.GenerateValidSale();
        sale.Items = new List<SaleItem>();

        // Act
        var result = _validator.TestValidate(sale);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Items);
    }

    ///// <summary>
    ///// Tests that validation fails when the created date is in the future.
    ///// </summary>
    //[Fact(DisplayName = "Created date in the future should fail validation")]
    //public void Given_CreatedDateInFuture_When_Validated_Then_ShouldHaveError()
    //{
    //    // Arrange
    //    var sale = UserSaleData.GenerateValidSale();
    //    sale.CreatedAt = DateTime.UtcNow.AddDays(1);

    //    // Act
    //    var result = _validator.TestValidate(sale);

    //    // Assert
    //    result.ShouldHaveValidationErrorFor(x => x.CreatedAt);
    //}

    /// <summary>
    /// Tests that validation fails for invalid items in the sale.
    /// </summary>
    [Fact(DisplayName = "Invalid sale items should fail validation")]
    public void Given_InvalidItems_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var sale = UserSaleData.GenerateValidSale();
        sale.Items[0].Quantity = 0; // Invalid quantity

        // Act
        var result = _validator.TestValidate(sale);

        // Assert
        result.ShouldHaveValidationErrorFor("Items[0].Quantity");
    }
}
