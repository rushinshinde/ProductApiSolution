using Application.Exceptions;
using Application.DTOs.Product;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using AutoMapper;
using Domain.Entities;
using Application.DTOs.Common;

namespace Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProductService(
        IProductRepository productRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductDto>> GetAllAsync()
    {
        var products = await _productRepository.GetAllAsync();

        return _mapper.Map<IEnumerable<ProductDto>>(products);
    }

   public async Task<ProductDto> GetByIdAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product == null)
            throw new NotFoundException($"Product with Id {id} was not found.");

        return _mapper.Map<ProductDto>(product);
    }

    public async Task<IEnumerable<ProductDto>> GetPagedAsync(PaginationParams paginationParams)
    {
        var products = await _productRepository.GetPagedAsync(paginationParams);

        return _mapper.Map<IEnumerable<ProductDto>>(products);
    }
    public async Task<ProductDto> CreateAsync(CreateProductDto dto)
    {
        var product = _mapper.Map<Product>(dto);

        product.CreatedOn = DateTime.UtcNow;

        await _productRepository.AddAsync(product);

        var result = await _unitOfWork.SaveChangesAsync();

        if (result <= 0)
            throw new BadRequestException("Unable to create product.");

        return _mapper.Map<ProductDto>(product);
    }

    public async Task<bool> UpdateAsync(int id, UpdateProductDto dto)
    {
        var product = await _productRepository.GetByIdAsync(id);

       if (product == null)
          throw new NotFoundException($"Product with Id {id} was not found.");

        product.ProductName = dto.ProductName;
        product.ModifiedBy = dto.ModifiedBy;
        product.ModifiedOn = DateTime.UtcNow;

        _productRepository.Update(product);

        var result = await _unitOfWork.SaveChangesAsync();

        if (result <= 0)
            throw new BadRequestException("Unable to update product.");

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product == null)
           throw new NotFoundException($"Product with Id {id} was not found.");

        _productRepository.Delete(product);

        var result = await _unitOfWork.SaveChangesAsync();

        if (result <= 0)
            throw new BadRequestException("Unable to delete product.");

        return true;
    }
}