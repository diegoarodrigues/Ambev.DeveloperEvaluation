using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

/// <summary>
/// Validator for Product entity
/// </summary>
public class ProductValidator : AbstractValidator<Product>
{
    /// <summary>
    /// Initializes validation rules for Product
    /// </summary>
    public ProductValidator()
    {
        RuleFor(product => product.Title)
            .NotEmpty().WithMessage("Product title is required.")
            .MinimumLength(3).WithMessage("Product title must be at least 3 characters long.")
            .MaximumLength(100).WithMessage("Product title cannot be longer than 100 characters.");

        RuleFor(product => product.Price)
            .GreaterThan(0).WithMessage("Product price must be greater than zero.");

        RuleFor(product => product.Description)
            .MaximumLength(500).WithMessage("Product description cannot be longer than 500 characters.");

        RuleFor(product => product.Category)
            .NotEmpty().WithMessage("Product category is required.")
            .MaximumLength(50).WithMessage("Product category cannot be longer than 50 characters.");

        RuleFor(product => product.Image)
            .NotEmpty().WithMessage("Product image URL is required.")
            .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
            .WithMessage("Product image must be a valid URL.");

        RuleFor(product => product.Rating.Rate)
            .InclusiveBetween(0, 5).WithMessage("Product rating must be between 0 and 5.");

        RuleFor(product => product.Rating.Count)
            .GreaterThanOrEqualTo(0).WithMessage("Product rating count cannot be negative.");
    }
}
