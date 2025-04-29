using MediatR;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Application.Users.GetUser;
using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
//using Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct;
//using Ambev.DeveloperEvaluation.WebApi.Features.Products.DeleteProduct;
//using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetCategories;
//using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProductsByCategory;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products;

/// <summary>
/// Controller for managing product operations
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ProductsController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of ProductsController
    /// </summary>
    /// <param name="mediator">The mediator instance</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public ProductsController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// Retrieves a list of all products
    /// </summary>
    //[HttpGet]
    //[ProducesResponseType(typeof(ApiResponseWithData<PaginatedList<GetProductResponse>>), StatusCodes.Status200OK)]
    //public async Task<IActionResult> GetProducts([FromQuery] GetProductsRequest request, CancellationToken cancellationToken)
    //{
    //    var validator = new GetProductsRequestValidator();
    //    var validationResult = await validator.ValidateAsync(request, cancellationToken);

    //    if (!validationResult.IsValid)
    //        return BadRequest(validationResult.Errors);

    //    var response = await _mediator.Send(request, cancellationToken);

    //    return Ok(new ApiResponseWithData<PaginatedList<GetProductResponse>>
    //    {
    //        Success = true,
    //        Message = "Products retrieved successfully",
    //        Data = response
    //    });
    //}

    /// <summary>
    /// Retrieves a specific product by ID
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<GetProductResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProduct([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new GetProductRequest { Id = id };
        var validator = new GetProductRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);
        
        var command = _mapper.Map<GetProductCommand>(request.Id);
        var response = await _mediator.Send(command, cancellationToken);

        return Ok(new ApiResponseWithData<GetProductResponse>
        {
            Success = true,
            Message = "Product retrieved successfully",
            Data = _mapper.Map<GetProductResponse>(response)
        });
    }

    /// <summary>
    /// Adds a new product
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateProductResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request, CancellationToken cancellationToken)
    {
        var validator = new CreateProductRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<CreateProductCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);

        return Created(string.Empty, new ApiResponseWithData<CreateProductResponse>
        {
            Success = true,
            Message = "Product created successfully",
            Data = _mapper.Map<CreateProductResponse>(response)
        });
    }

    ///// <summary>
    ///// Updates a specific product
    ///// </summary>
    //[HttpPut("{id}")]
    //[ProducesResponseType(typeof(ApiResponseWithData<UpdateProductResponse>), StatusCodes.Status200OK)]
    //[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    //[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    //public async Task<IActionResult> UpdateProduct([FromRoute] Guid id, [FromBody] UpdateProductRequest request, CancellationToken cancellationToken)
    //{
    //    request.Id = id;

    //    var validator = new UpdateProductRequestValidator();
    //    var validationResult = await validator.ValidateAsync(request, cancellationToken);

    //    if (!validationResult.IsValid)
    //        return BadRequest(validationResult.Errors);

    //    var command = _mapper.Map<UpdateProductCommand>(request);
    //    var response = await _mediator.Send(command, cancellationToken);

    //    return Ok(new ApiResponseWithData<UpdateProductResponse>
    //    {
    //        Success = true,
    //        Message = "Product updated successfully",
    //        Data = _mapper.Map<UpdateProductResponse>(response)
    //    });
    //}

    ///// <summary>
    ///// Deletes a specific product
    ///// </summary>
    //[HttpDelete("{id}")]
    //[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    //[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    //public async Task<IActionResult> DeleteProduct([FromRoute] Guid id, CancellationToken cancellationToken)
    //{
    //    var request = new DeleteProductRequest { Id = id };
    //    var validator = new DeleteProductRequestValidator();
    //    var validationResult = await validator.ValidateAsync(request, cancellationToken);

    //    if (!validationResult.IsValid)
    //        return BadRequest(validationResult.Errors);

    //    await _mediator.Send(request, cancellationToken);

    //    return Ok(new ApiResponse
    //    {
    //        Success = true,
    //        Message = "Product deleted successfully"
    //    });
    //}

    ///// <summary>
    ///// Retrieves all product categories
    ///// </summary>
    //[HttpGet("categories")]
    //[ProducesResponseType(typeof(ApiResponseWithData<List<string>>), StatusCodes.Status200OK)]
    //public async Task<IActionResult> GetCategories(CancellationToken cancellationToken)
    //{
    //    var response = await _mediator.Send(new GetCategoriesRequest(), cancellationToken);

    //    return Ok(new ApiResponseWithData<List<string>>
    //    {
    //        Success = true,
    //        Message = "Categories retrieved successfully",
    //        Data = response
    //    });
    //}

    ///// <summary>
    ///// Retrieves products in a specific category
    ///// </summary>
    //[HttpGet("category/{category}")]
    //[ProducesResponseType(typeof(ApiResponseWithData<PaginatedList<GetProductResponse>>), StatusCodes.Status200OK)]
    //public async Task<IActionResult> GetProductsByCategory([FromRoute] string category, [FromQuery] GetProductsByCategoryRequest request, CancellationToken cancellationToken)
    //{
    //    request.Category = category;

    //    var validator = new GetProductsByCategoryRequestValidator();
    //    var validationResult = await validator.ValidateAsync(request, cancellationToken);

    //    if (!validationResult.IsValid)
    //        return BadRequest(validationResult.Errors);

    //    var response = await _mediator.Send(request, cancellationToken);

    //    return Ok(new ApiResponseWithData<PaginatedList<GetProductResponse>>
    //    {
    //        Success = true,
    //        Message = "Products retrieved successfully",
    //        Data = response
    //    });
    //}
}
