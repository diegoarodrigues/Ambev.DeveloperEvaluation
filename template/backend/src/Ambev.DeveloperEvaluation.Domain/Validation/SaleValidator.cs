using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

/// <summary>
/// Validator for the Sale entity, ensuring all business rules and constraints are met.
/// </summary>
public class SaleValidator : AbstractValidator<Sale>
{
    public SaleValidator()
    {
        // Validate SaleNumber
        RuleFor(sale => sale.SaleNumber)
            .NotEmpty().WithMessage("Sale number must not be empty.")
            .MaximumLength(50).WithMessage("Sale number cannot exceed 50 characters.");

        // Validate Date
        RuleFor(sale => sale.Date)
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Sale date cannot be in the future.");

        // Validate Customer
        RuleFor(sale => sale.Customer)
            .NotEmpty().WithMessage("Customer must not be empty.")
            .MaximumLength(100).WithMessage("Customer name cannot exceed 100 characters.");

        // Validate Branch
        RuleFor(sale => sale.Branch)
            .NotEmpty().WithMessage("Branch must not be empty.")
            .MaximumLength(100).WithMessage("Branch name cannot exceed 100 characters.");

        // Validate TotalAmount
        RuleFor(sale => sale.TotalAmount)
            .GreaterThanOrEqualTo(0).WithMessage("Total amount must be greater than or equal to zero.");

        // Validate Items
        RuleFor(sale => sale.Items)
            .NotEmpty().WithMessage("Sale must have at least one item.");

        // Validate each SaleItem using SaleItemValidator
        RuleForEach(sale => sale.Items)
            .SetValidator(new SaleItemValidator());

        //// Validate UpdatedAt
        //RuleFor(sale => sale.UpdatedAt)
        //    .LessThanOrEqualTo(new DateTime()).WithMessage("Date must be less than or equal to date now.");

        // Validate IsCancelled
        //RuleFor(sale => sale.IsCancelled)
        //    .NotNull().WithMessage("IsCancelled must not be null.");
    }
}
