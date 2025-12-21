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

    public UserService(IUserRepository repository, PasswordHash passwordHash)
    {
        _repository = repository;
        _passwordHash = passwordHash;
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

    // public async Task<Users> RegisterUserAsync(Users user)
    // {
    //     DateTime date = DateTime.UtcNow;

    //     user.Password = _hasher.HashPassword(user, user.Password);
    //     // user.CreatedAt = date;c
    //     // user.UpdatedAt = date;

    //     await _usersDbSet.AddAsync(user);
    //     await _context.SaveChangesAsync();

    //     return user;
    // }

    // public async Task<string> LoginUserAsync(string username, string password)
    // {
    //     Users? user = await _usersDbSet.FirstOrDefaultAsync(u => u.UserName == username);

    //     if (user is null) throw new ObjectNotFoundException("User not found");

    //     PasswordVerificationResult result = _hasher.VerifyPassword(user: user, password: password);

    //     if (result != PasswordVerificationResult.Success) throw new UnauthorizedAccessException();

    //     string token = _jwt.GenerateToken(user: user);

    //     return token;
    // }

    // public async Task<List<Users>> GetAllUserAsync()
    // {
    //     return await _usersDbSet.ToListAsync();
    // }

    // public async Task<Users?> GetByIdAsync(int userId)
    // {
    //     return await _usersDbSet.FirstOrDefaultAsync(u => u.Id == userId);
    // }
}