using Application.DTOs.Auth;
using Application.DTOs.Common;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    // POST: api/v1/Auth/Register
    [HttpPost("Register")]
    public async Task<IActionResult> Register(RegisterRequestDto request)
    {
        await _authService.RegisterAsync(request);

        return Ok(new ApiResponse<string>
        {
            Success = true,
            StatusCode = StatusCodes.Status200OK,
            Message = "User registered successfully.",
            Data = null
        });
    }

    // POST: api/v1/Auth/Login
    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginRequestDto request)
    {
        var result = await _authService.LoginAsync(request);

        return Ok(new ApiResponse<LoginResponseDto>
        {
            Success = true,
            StatusCode = StatusCodes.Status200OK,
            Message = "Login successful.",
            Data = result
        });
    }

    // POST: api/v1/Auth/RefreshToken
    [HttpPost("RefreshToken")]
    public async Task<IActionResult> RefreshToken(RefreshTokenRequestDto request)
    {
        var result = await _authService.RefreshTokenAsync(request);

        return Ok(new ApiResponse<LoginResponseDto>
        {
            Success = true,
            StatusCode = StatusCodes.Status200OK,
            Message = "Token refreshed successfully.",
            Data = result
        });
    }
}