using System.Data.Entity.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UrlShortner.Entities;
using UrlShortner.Helper;
using UrlShortner.Interfaces;
using UrlShortner.Models;

namespace UrlShortner.Services;

public class UserService
{
    private readonly IUserRepository _repository;
    private readonly PasswordHash _passwordHash;
    private readonly JwtService _jwtService;

    public UserService(IUserRepository repository, PasswordHash passwordHash, JwtService jwtService)
    {
        _repository = repository;
        _passwordHash = passwordHash;
        _jwtService = jwtService;
    }

    public async Task CreateUserAsync(CreateUserDto payload)
    {
        bool isUserNameExists = await _repository.IsUserNameExistAsync(payload.Username);
        
        if (isUserNameExists)
        {
            throw new BadHttpRequestException("User name already exists");
        }

        DateOnly date = DateOnly.FromDateTime(DateTime.UtcNow);
        Users user = new Users
        {
            CreatedAt = date,
            UpdatedAt = date,
            UserName = payload.Username,
            Name = payload.Name,
            Role = payload.Role
        };

        user.Password = _passwordHash.HashPassword(user, payload.Password);
        await _repository.CreateUserAsync(user);
    }

    public async Task<LoginUserResponseDto> LoginUserAsync(LoginUserRequestDto payload)
    {
        Users? user = await _repository.GetByUsername(payload.Username);

        if (user is null)
        {
            Console.WriteLine("username is invalid");
            throw new UnauthorizedAccessException("Invalid Credentials provided");
        }

        PasswordVerificationResult passwordResult = _passwordHash.VerifyPassword(user, payload.Password);

        if (passwordResult == PasswordVerificationResult.Failed)
        {
            Console.WriteLine("password is invalid");
            throw new UnauthorizedAccessException("Invalid Credentials provided");
        }

        string token = _jwtService.GenerateToken(user);

        return new LoginUserResponseDto { Token = token, Username = user.UserName };
    }
}