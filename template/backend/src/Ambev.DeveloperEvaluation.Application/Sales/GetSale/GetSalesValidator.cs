using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSales;

/// <summary>
/// Validator for GetSalesCommand
/// </summary>
public class GetSalesValidator : AbstractValidator<GetSalesCommand>
{
    /// <summary>
    /// Initializes validation rules for GetSalesCommand
    /// </summary>
    public GetSalesValidator()
    {
        //// Validate StartDate and EndDate
        //RuleFor(x => x.Page)
        //    .LessThanOrEqualTo(x => x.PageSize)
        //    .When(x => x.PageSize >= 0)
        //    .WithMessage("StartDate must be less than or equal to EndDate.");
    }
}

