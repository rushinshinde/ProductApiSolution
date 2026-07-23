using Application.DTOs.Auth;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Entities;

namespace Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AuthService(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task RegisterAsync(RegisterRequestDto request)
    {
        var existingUser = await _userRepository.GetByUsernameAsync(request.Username);

        if (existingUser != null)
            throw new Exception("Username already exists.");

        var user = new User
        {
            Username = request.Username,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
        };

        await _userRepository.AddAsync(user);

        await _unitOfWork.SaveChangesAsync();
    }

    public Task<LoginResponseDto> LoginAsync(LoginRequestDto request)
    {
        throw new NotImplementedException();
    }
}