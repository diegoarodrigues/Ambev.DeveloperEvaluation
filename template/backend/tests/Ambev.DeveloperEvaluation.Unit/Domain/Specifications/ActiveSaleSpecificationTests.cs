using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Specifications;
using Ambev.DeveloperEvaluation.Unit.Domain.Specifications.TestData;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Specifications
{
    /// <summary>
    /// Contains unit tests for the ActiveSaleSpecification class.
    /// Tests cover validation of the IsCancelled property.
    /// </summary>
    public class ActiveSaleSpecificationTests
    {
        [Theory]
        [InlineData(false, true)] // Sale is not cancelled, should satisfy the specification
        [InlineData(true, false)] // Sale is cancelled, should not satisfy the specification
        public void IsSatisfiedBy_ShouldValidateSaleCancellationStatus(bool isCancelled, bool expectedResult)
        {
            // Arrange
            var sale = ActiveSaleSpecificationTestData.GenerateSale(isCancelled);
            var specification = new ActiveSaleSpecification();

            // Act
            var result = specification.IsSatisfiedBy(sale);

            // Assert
            result.Should().Be(expectedResult);
        }
    }
}
