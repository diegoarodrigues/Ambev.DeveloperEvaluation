using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using AutoMapper;
using Bogus;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales;

/// <summary>
/// Contains unit tests for the CreateSaleHandler class.
/// Tests cover scenarios for creating sales with valid and invalid data.
/// </summary>
public class CreateSaleHandlerTests
{
    private readonly ISaleRepository _saleRepositoryMock;
    private readonly ISaleDomainService _saleDomainServiceMock;
    private readonly CreateSaleHandler _handler;
    private readonly IMapper _mapperMock;

    public CreateSaleHandlerTests()
    {
        _saleRepositoryMock = Substitute.For<ISaleRepository>();
        _saleDomainServiceMock = Substitute.For<ISaleDomainService>();
        _mapperMock = Substitute.For<IMapper>();
        _handler = new CreateSaleHandler(_saleRepositoryMock, _mapperMock, _saleDomainServiceMock);
    }

    /// <summary>
    /// Tests that a sale is successfully created when valid data is provided.
    /// </summary>
    [Fact(DisplayName = "Should create sale successfully with valid data")]
    public async Task Given_ValidCreateSaleCommand_When_Handled_Then_ShouldCreateSaleSuccessfully()
    {
        // Arrange
        var command = CreateSaleHandlerTestData.GenerateValidCommand();
        var createdSale = new Sale
        {
            Id = Guid.NewGuid(),
            SaleNumber = command.SaleNumber,
            TotalAmount = 100.0m,
            Items = command.Items.Select(i => new SaleItem
            {
                ProductId = i.ProductId,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice,
                Discount = 0,
                TotalAmount = i.Quantity * i.UnitPrice
            }).ToList()
        };

        _mapperMock.Map<Sale>(Arg.Any<CreateSaleCommand>())
            .Returns(createdSale);

        _mapperMock.Map<CreateSaleResult>(Arg.Any<Sale>())
            .Returns(new CreateSaleResult { Id = createdSale.Id, SaleNumber = createdSale.SaleNumber, TotalAmount = createdSale.TotalAmount});

        _saleRepositoryMock.CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(createdSale));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(createdSale.Id);
        result.SaleNumber.Should().Be(createdSale.SaleNumber);
        result.TotalAmount.Should().Be(createdSale.TotalAmount);
    }

    /// <summary>
    /// Tests that an exception is thrown when a sale with a duplicate sale number is created.
    /// </summary>
    [Fact(DisplayName = "Should throw exception for duplicate sale number")]
    public async Task Given_DuplicateSaleNumber_When_Handled_Then_ShouldThrowException()
    {
        // Arrange
        var command = CreateSaleHandlerTestData.GenerateValidCommand();
        var existingSale = new Sale { SaleNumber = command.SaleNumber };

        _saleRepositoryMock.GetBySaleNumberAsync(command.SaleNumber, Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(existingSale));

        // Act
        var act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage($"Sale with number {command.SaleNumber} already exists");
    }

    /// <summary>
    /// Tests that an exception is thrown when the command is invalid.
    /// </summary>
    [Fact(DisplayName = "Should throw validation exception for invalid command")]
    public async Task Given_InvalidCreateSaleCommand_When_Handled_Then_ShouldThrowValidationException()
    {
        // Arrange
        var command = new CreateSaleCommand
        {
            SaleNumber = string.Empty, // Invalid sale number
            Date = DateTime.UtcNow,
            Customer = string.Empty, // Invalid customer
            Branch = string.Empty, // Invalid branch
            Items = new List<CreateSaleItemCommand>() // No items
        };

        // Act
        var act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
    }
}
