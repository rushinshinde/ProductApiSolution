using Application.DTOs.Common;
using Application.DTOs.Product;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers;

[ApiController]
[Authorize]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    // GET: api/v1/Products
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await _productService.GetAllAsync();

        return Ok(new ApiResponse<IEnumerable<ProductDto>>
        {
            Success = true,
            StatusCode = StatusCodes.Status200OK,
            Message = "Products retrieved successfully.",
            Data = products
        });
    }

    // GET: api/v1/Products/1
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await _productService.GetByIdAsync(id);
        return Ok(new ApiResponse<ProductDto>
    {
        Success = true,
        StatusCode = StatusCodes.Status200OK,
        Message = "Product retrieved successfully.",
        Data = product
    });
    }

    // POST: api/v1/Products
    [HttpPost]
    public async Task<IActionResult> Create(CreateProductDto dto)
    {
        var product = await _productService.CreateAsync(dto);

        return CreatedAtAction(
        nameof(GetById),
        new { version = "1.0", id = product.Id },
        new ApiResponse<ProductDto>
        {
            Success = true,
            StatusCode = StatusCodes.Status201Created,
            Message = "Product created successfully.",
            Data = product
        });
    }

    // PUT: api/v1/Products/1
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateProductDto dto)
    {
        await _productService.UpdateAsync(id, dto);

        return Ok(new ApiResponse<string>
        {
            Success = true,
            StatusCode = StatusCodes.Status200OK,
            Message = "Product updated successfully.",
            Data = null
        });
    }

    // DELETE: api/v1/Products/1
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _productService.DeleteAsync(id);

        return Ok(new ApiResponse<string>
        {
            Success = true,
            StatusCode = StatusCodes.Status200OK,
            Message = "Product deleted successfully.",
            Data = null
        });
    }
}