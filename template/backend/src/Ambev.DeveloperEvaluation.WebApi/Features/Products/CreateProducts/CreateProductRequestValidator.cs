using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;

/// <summary>
/// Validator for CreateProductRequest that defines validation rules for product creation.
/// </summary>
public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
{
    /// <summary>
    /// Initializes a new instance of the CreateProductRequestValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Title: Required, length between 3 and 100 characters
    /// - Price: Must be greater than 0
    /// - Description: Optional, but cannot exceed 500 characters
    /// - Category: Required, length between 3 and 50 characters
    /// - Image: Must be a valid URL
    /// </remarks>
    public CreateProductRequestValidator()
    {
        RuleFor(product => product.Title).NotEmpty().WithMessage("Title is required.").Length(3, 100).WithMessage("Title must be between 3 and 100 characters.");
        RuleFor(product => product.Price).GreaterThan(0).WithMessage("Price must be greater than 0.");
        RuleFor(product => product.Description).MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");
        RuleFor(product => product.Category).NotEmpty().WithMessage("Category is required.").Length(3, 50).WithMessage("Category must be between 3 and 50 characters.");
        RuleFor(product => product.Image).NotEmpty().WithMessage("Image URL is required.").Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _)).WithMessage("Image must be a valid URL.");
    }
}
