using System.Data.Entity.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UrlShortner.Helper;
using UrlShortner.Models;

namespace UrlShortner.Services;

public class UserService
{
    private readonly UrlDbContext _context;
    private readonly DbSet<Users> _usersDbSet;
    private readonly PasswordHash _hasher;
    private readonly Jwt _jwt;

    public UserService(UrlDbContext context, PasswordHash hasher, Jwt jwt)
    {
        _context = context;
        _usersDbSet = context.UsersDbSet;
        _hasher = hasher;
        _jwt = jwt;
    }

    public async Task<Users> RegisterUserAsync(Users user)
    {
        DateTime date = DateTime.UtcNow;

        user.Password = _hasher.HashPassword(user, user.Password);
        user.CreatedAt = date;
        user.UpdatedAt = date;

        await _usersDbSet.AddAsync(user);
        await _context.SaveChangesAsync();

        return user;
    }

    public async Task<string> LoginUserAsync(string username, string password)
    {
        Users? user = await _usersDbSet.FirstOrDefaultAsync(u => u.UserName == username);

        if (user is null) throw new ObjectNotFoundException("User not found");

        PasswordVerificationResult result = _hasher.VerifyPassword(user: user, password: password);

        if (result != PasswordVerificationResult.Success) throw new UnauthorizedAccessException();

        string token = _jwt.GenerateToken(user: user);

        return token;
    }
}