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

    public UserService(UrlDbContext context, PasswordHash hasher)
    {
        _context = context;
        _usersDbSet = context.UsersDbSet;
        _hasher = hasher;
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
}