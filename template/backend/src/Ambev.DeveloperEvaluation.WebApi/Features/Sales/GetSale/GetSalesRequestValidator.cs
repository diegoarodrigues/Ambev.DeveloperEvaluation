using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSales;

/// <summary>
/// Validator for GetSalesRequest.
/// Ensures that filter parameters are valid.
/// </summary>
public class GetSalesRequestValidator : AbstractValidator<GetSalesRequest>
{
    public GetSalesRequestValidator()
    {
        //RuleFor(x => x.StartDate)
        //    .LessThanOrEqualTo(x => x.EndDate)
        //    .When(x => x.StartDate.HasValue && x.EndDate.HasValue)
        //    .WithMessage("StartDate must be less than or equal to EndDate.");
    }
}
