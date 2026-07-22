using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface IItemRepository
{
    Task<IEnumerable<Item>> GetAllAsync();

    Task<Item?> GetByIdAsync(int id);

    Task AddAsync(Item item);

    void Update(Item item);

    void Delete(Item item);
}