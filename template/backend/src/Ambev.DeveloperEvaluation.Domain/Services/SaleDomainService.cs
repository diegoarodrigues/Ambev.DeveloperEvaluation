using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Services;

public class SaleDomainService : ISaleDomainService
{
    public void ApplyDiscounts(Sale sale)
    {
        foreach (var item in sale.Items)
        {
            item.CalculateDiscount();
        }

        sale.TotalAmount = sale.Items.Sum(i => i.TotalAmount);
    }

    public void ValidateSale(Sale sale)
    {
        foreach (var item in sale.Items)
        {
            item.ValidateQuantity();
        }
    }
}
