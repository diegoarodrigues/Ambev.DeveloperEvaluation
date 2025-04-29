using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

/// <summary>
/// Validator for CreateSaleRequest that defines validation rules for sale creation.
/// </summary>
public class CreateSaleRequestValidator : AbstractValidator<CreateSaleRequest>
{
    /// <summary>
    /// Initializes a new instance of the CreateSaleRequestValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - SaleNumber: Required, length between 1 and 50 characters
    /// - Date: Must not be in the future
    /// - Customer: Required, length between 1 and 100 characters
    /// - Branch: Required, length between 1 and 100 characters
    /// - Items: Must contain at least one item
    /// - Each item must follow specific rules (validated using SaleItemValidator)
    /// </remarks>
    public CreateSaleRequestValidator()
    {
        RuleFor(sale => sale.SaleNumber)
            .NotEmpty().WithMessage("Sale number is required.")
            .Length(1, 50).WithMessage("Sale number must be between 1 and 50 characters.");

        RuleFor(sale => sale.Date)
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Sale date cannot be in the future.");

        RuleFor(sale => sale.Customer)
            .NotEmpty().WithMessage("Customer is required.")
            .Length(1, 100).WithMessage("Customer name must be between 1 and 100 characters.");

        RuleFor(sale => sale.Branch)
            .NotEmpty().WithMessage("Branch is required.")
            .Length(1, 100).WithMessage("Branch name must be between 1 and 100 characters.");

        RuleFor(sale => sale.Items)
            .NotEmpty().WithMessage("At least one sale item is required.")
            .ForEach(item => item.SetValidator(new CreateSaleItemRequestValidator()));
    }
}

/// <summary>
/// Validator for CreateSaleItemRequest that defines validation rules for sale items.
/// </summary>
public class CreateSaleItemRequestValidator : AbstractValidator<CreateSaleItemRequest>
{
    /// <summary>
    /// Initializes a new instance of the CreateSaleItemRequestValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - ProductId: Required
    /// - Quantity: Must be greater than 0 and less than or equal to 20
    /// - UnitPrice: Must be greater than 0
    /// </remarks>
    public CreateSaleItemRequestValidator()
    {
        RuleFor(item => item.ProductId)
            .NotEmpty().WithMessage("Product ID is required.");

        RuleFor(item => item.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than 0.")
            .LessThanOrEqualTo(20).WithMessage("Quantity cannot exceed 20.");

        RuleFor(item => item.UnitPrice)
            .GreaterThan(0).WithMessage("Unit price must be greater than 0.");
    }
}
