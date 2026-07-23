using Application.DTOs.Common;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;

    public async Task<IEnumerable<Product>> GetPagedAsync(PaginationParams paginationParams)
    {
        return await _context.Products
            .AsNoTracking()
            .OrderBy(p => p.Id)
            .Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
            .Take(paginationParams.PageSize)
            .ToListAsync();
    }
    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _context.Products
            .AsNoTracking()
            .Include(p => p.Items)
            .ToListAsync();
    }

   public async Task<Product?> GetByIdAsync(int id)
    {
        return await _context.Products
            .AsNoTracking()
            .Include(p => p.Items)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task AddAsync(Product product)
    {
        await _context.Products.AddAsync(product);
    }

    public void Update(Product product)
    {
        _context.Products.Update(product);
    }

    public void Delete(Product product)
    {
        _context.Products.Remove(product);
    }
}