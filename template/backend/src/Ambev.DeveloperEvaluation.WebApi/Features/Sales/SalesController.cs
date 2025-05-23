﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSales;
using Ambev.DeveloperEvaluation.Application.Sales.GetSales;
//using Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;
//using Ambev.DeveloperEvaluation.WebApi.Features.Sales.DeleteSale;
//using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSalesByCustomer;
//using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
//using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
//using Ambev.DeveloperEvaluation.Application.Sales.GetSalesByCustomer;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales;

/// <summary>
/// Controller for managing sales operations
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class SalesController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public SalesController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// Retrieves a list of all sales
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponseWithData<List<GetSaleResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetSales([FromQuery] GetSalesRequest request, CancellationToken cancellationToken)
    {
        var validator = new GetSalesValidator();
        //var validationResult = await validator.ValidateAsync(request, cancellationToken);

        //if (!validationResult.IsValid)
        //    return BadRequest(validationResult.Errors);

        var query = _mapper.Map<GetSalesCommand>(request);
        var result = await _mediator.Send(query, cancellationToken);

        // Map the result to GetSaleResponse
        var response = _mapper.Map<List<GetSaleResponse>>(result.Items);

        return Ok(new PaginatedResponse<GetSaleResponse>
        {
            Success = true,
            Message = "Sales retrieved successfully",
            Data = response,
            CurrentPage = request.Page,
            TotalPages = (int)Math.Ceiling((double)result.TotalCount / request.PageSize),
            TotalCount = result.TotalCount
        }); ; 
    }

    /// <summary>
    /// Creates a new sale
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateSaleResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateSale([FromBody] CreateSaleRequest request, CancellationToken cancellationToken)
    {
        var validator = new CreateSaleRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<CreateSaleCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);

        return Created(string.Empty, new ApiResponseWithData<CreateSaleResponse>
        {
            Success = true,
            Message = "Sale created successfully",
            Data = _mapper.Map<CreateSaleResponse>(response)
        });
    }

    /// <summary>
    /// Retrieves a sale by its ID
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<GetSaleResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetSale([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new GetSaleRequest { Id = id };
        var validator = new GetSaleRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<GetSaleCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);

        if (response == null)
            return NotFound("Sale not found");

        return Ok(new ApiResponseWithData<GetSaleResponse>
        {
            Success = true,
            Message = "Sale retrieved successfully",
            Data = _mapper.Map<GetSaleResponse>(response)
        });
    }


    ///// <summary>
    ///// Updates a specific sale
    ///// </summary>
    //[HttpPut("{id}")]
    //[ProducesResponseType(typeof(ApiResponseWithData<UpdateSaleResponse>), StatusCodes.Status200OK)]
    //public async Task<IActionResult> UpdateSale([FromRoute] Guid id, [FromBody] UpdateSaleRequest request, CancellationToken cancellationToken)
    //{
    //    var validator = new UpdateSaleRequestValidator();
    //    var validationResult = await validator.ValidateAsync(request, cancellationToken);

    //    if (!validationResult.IsValid)
    //        return BadRequest(validationResult.Errors);

    //    var command = _mapper.Map<UpdateSaleCommand>(request);
    //    command.Id = id;

    //    var response = await _mediator.Send(command, cancellationToken);

    //    return Ok(new ApiResponseWithData<UpdateSaleResponse>
    //    {
    //        Success = true,
    //        Message = "Sale updated successfully",
    //        Data = response
    //    });
    //}

    ///// <summary>
    ///// Deletes a sale by its ID
    ///// </summary>
    //[HttpDelete("{id}")]
    //[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    //[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    //[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    //public async Task<IActionResult> DeleteSale([FromRoute] Guid id, CancellationToken cancellationToken)
    //{
    //    var request = new DeleteSaleRequest { Id = id };
    //    var validator = new DeleteSaleRequestValidator();
    //    var validationResult = await validator.ValidateAsync(request, cancellationToken);

    //    if (!validationResult.IsValid)
    //        return BadRequest(validationResult.Errors);

    //    var command = _mapper.Map<DeleteSaleCommand>(request.Id);
    //    await _mediator.Send(command, cancellationToken);

    //    return Ok(new ApiResponse
    //    {
    //        Success = true,
    //        Message = "Sale deleted successfully"
    //    });
    //}

    ///// <summary>
    ///// Retrieves all sales for a specific customer
    ///// </summary>
    //[HttpGet("customer/{customerId}")]
    //[ProducesResponseType(typeof(ApiResponseWithData<PaginatedList<GetSaleResponse>>), StatusCodes.Status200OK)]
    //public async Task<IActionResult> GetSalesByCustomer([FromRoute] Guid customerId, [FromQuery] GetSalesByCustomerRequest request, CancellationToken cancellationToken)
    //{
    //    request.CustomerId = customerId;

    //    var validator = new GetSalesByCustomerRequestValidator();
    //    var validationResult = await validator.ValidateAsync(request, cancellationToken);

    //    if (!validationResult.IsValid)
    //        return BadRequest(validationResult.Errors);

    //    var query = _mapper.Map<GetSalesByCustomerQuery>(request);
    //    var response = await _mediator.Send(query, cancellationToken);

    //    return Ok(new ApiResponseWithData<PaginatedList<GetSaleResponse>>
    //    {
    //        Success = true,
    //        Message = "Sales for customer retrieved successfully",
    //        Data = response
    //    });
    //}
}
