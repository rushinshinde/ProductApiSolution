using Application.DTOs.Product;
using Domain.Entities;
using Application.DTOs.Common;

namespace Application.Interfaces.Services;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetAllAsync();

    Task<IEnumerable<ProductDto>> GetPagedAsync(PaginationParams paginationParams);

    Task<ProductDto> GetByIdAsync(int id);

    Task<ProductDto> CreateAsync(CreateProductDto dto);

    Task<bool> UpdateAsync(int id, UpdateProductDto dto);

    Task<bool> DeleteAsync(int id);
}