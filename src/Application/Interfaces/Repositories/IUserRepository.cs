using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface IUserRepository
{
    Task<User?> GetByUsernameAsync(string username);

    Task AddAsync(User user);
}