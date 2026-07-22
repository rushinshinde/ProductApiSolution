using Domain.Exceptions;
using Application.DTOs.Item;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using AutoMapper;
using Domain.Entities;

namespace Application.Services;

public class ItemService : IItemService
{
    private readonly IItemRepository _itemRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ItemService(
        IItemRepository itemRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _itemRepository = itemRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ItemDto>> GetAllAsync()
    {
        var items = await _itemRepository.GetAllAsync();

        return _mapper.Map<IEnumerable<ItemDto>>(items);
    }

    public async Task<ItemDto?> GetByIdAsync(int id)
    {
        var item = await _itemRepository.GetByIdAsync(id);

        if (item == null)
            return null;

        return _mapper.Map<ItemDto>(item);
    }

    public async Task<ItemDto> CreateAsync(CreateItemDto dto)
    {
        var item = _mapper.Map<Item>(dto);

        await _itemRepository.AddAsync(item);

        var result = await _unitOfWork.SaveChangesAsync();

        if (result <= 0)
            throw new BadRequestException("Unable to create item.");

        return _mapper.Map<ItemDto>(item);
    }

    public async Task<bool> UpdateAsync(int id, UpdateItemDto dto)
    {
        var item = await _itemRepository.GetByIdAsync(id);

        if (item == null)
            throw new NotFoundException($"Item with Id {id} was not found.");

        item.Quantity = dto.Quantity;

        _itemRepository.Update(item);

        var result = await _unitOfWork.SaveChangesAsync();

        if (result <= 0)
            throw new BadRequestException("Unable to update item.");

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var item = await _itemRepository.GetByIdAsync(id);

        if (item == null)
            throw new NotFoundException($"Item with Id {id} was not found.");

        _itemRepository.Delete(item);

        var result = await _unitOfWork.SaveChangesAsync();

        if (result <= 0)
            throw new BadRequestException("Unable to delete item.");

        return true;
    }
}