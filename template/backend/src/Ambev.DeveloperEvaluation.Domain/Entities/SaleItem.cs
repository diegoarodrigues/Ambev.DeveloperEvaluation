using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents an item in a sale with details and business rules validation.
/// This entity follows domain-driven design principles.
/// </summary>
public class SaleItem : BaseEntity
{
    /// <summary>
    /// Gets or sets the ID of the sale this item belongs to.
    /// </summary>
    public Guid SaleId { get; set; }

    /// <summary>
    /// Gets or sets the ID of the product being sold.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Gets or sets the quantity of the product being sold.
    /// Must be greater than zero and follow business rules for discounts.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets or sets the unit price of the product.
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Gets or sets the discount applied to the item.
    /// </summary>
    public decimal Discount { get; set; }

    /// <summary>
    /// Gets or sets the total amount for the item.
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Gets or sets whether the item is cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }

    /// <summary>
    /// Initializes a new instance of the SaleItem class.
    /// </summary>
    public SaleItem()
    {
        TotalAmount = 0;
        Discount = 0;
    }

    /// <summary>
    /// Calculates the discount and total amount based on the quantity.
    /// </summary>
    public void CalculateDiscount()
    {
        if (Quantity > 20)
        {
            throw new InvalidOperationException("Cannot sell more than 20 items of the same product.");
        }

        if (Quantity >= 10 && Quantity <= 20)
        {
            Discount = UnitPrice * Quantity * 0.20m; // 20% de desconto
        }
        else if (Quantity >= 4 && Quantity < 10)
        {
            Discount = UnitPrice * Quantity * 0.10m; // 10% de desconto
        }
        else
        {
            Discount = 0; // Sem desconto
        }

        TotalAmount = (UnitPrice * Quantity) - Discount;
    }

    /// <summary>
    /// Validates the quantity of the item.
    /// </summary>
    public void ValidateQuantity()
    {
        if (Quantity <= 0)
        {
            throw new InvalidOperationException("Quantity must be greater than zero.");
        }

        if (Quantity > 20)
        {
            throw new InvalidOperationException("Cannot sell more than 20 items of the same product.");
        }
    }

    /// <summary>
    /// Performs validation of the sale item entity using the SaleItemValidator rules.
    /// </summary>
    /// <returns>
    /// A <see cref="ValidationResultDetail"/> containing:
    /// - IsValid: Indicates whether all validation rules passed
    /// - Errors: Collection of validation errors if any rules failed
    /// </returns>
    public ValidationResultDetail Validate()
    {
        var validator = new SaleItemValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }

    /// <summary>
    /// Cancels the sale item.
    /// Changes the item's status to cancelled.
    /// </summary>
    public void Cancel()
    {
        IsCancelled = true;
    }
}
