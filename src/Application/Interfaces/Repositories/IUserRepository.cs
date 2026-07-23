using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface IUserRepository
{
    Task<User?> GetByUsernameAsync(string username);
    
    Task<User?> GetByRefreshTokenAsync(string refreshToken);

    Task AddAsync(User user);
}