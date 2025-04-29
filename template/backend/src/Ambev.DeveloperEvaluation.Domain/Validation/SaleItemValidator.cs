using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

/// <summary>
/// Validator for the SaleItem entity, ensuring all business rules and constraints are met.
/// </summary>
public class SaleItemValidator : AbstractValidator<SaleItem>
{
    public SaleItemValidator()
    {
        // Validate SaleId
        RuleFor(item => item.SaleId)
            .NotEmpty().WithMessage("SaleId must not be empty.");

        // Validate ProductId
        RuleFor(item => item.ProductId)
            .NotEmpty().WithMessage("ProductId must not be empty.");

        // Validate Quantity
        RuleFor(item => item.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than zero.")
            .LessThanOrEqualTo(20).WithMessage("Quantity cannot exceed 20 items per product.");

        // Validate UnitPrice
        RuleFor(item => item.UnitPrice)
            .GreaterThan(0).WithMessage("UnitPrice must be greater than zero.");

        // Validate Discount
        RuleFor(item => item.Discount)
            .GreaterThanOrEqualTo(0).WithMessage("Discount must be greater than or equal to zero.")
            .LessThanOrEqualTo(item => item.UnitPrice * item.Quantity)
            .WithMessage("Discount cannot exceed the total price of the item.");

        //// Validate TotalAmount
        //RuleFor(item => item.TotalAmount)
        //    .GreaterThanOrEqualTo(0).WithMessage("TotalAmount must be greater than or equal to zero.")
        //    .Equal(item => (item.UnitPrice * item.Quantity) - item.Discount)
        //    .WithMessage("TotalAmount must equal (UnitPrice * Quantity) - Discount.");

        // Validate IsCancelled
        RuleFor(item => item.IsCancelled)
            .NotNull().WithMessage("IsCancelled must not be null.");
    }
}
