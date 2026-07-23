using Application.DTOs.Common;
using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync();

    Task<IEnumerable<Product>> GetPagedAsync(PaginationParams paginationParams);

    Task<Product?> GetByIdAsync(int id);

    Task AddAsync(Product product);

    void Update(Product product);

    void Delete(Product product);
}