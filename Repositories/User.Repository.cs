using Microsoft.EntityFrameworkCore;
using UrlShortner.Entities;
using UrlShortner.Interfaces;

namespace UrlShortner.Repository;

public class UserRepository : IUserRepository
{
    private readonly DbSet<Users> _userDbSet;
    private readonly UrlDbContext _context;

    public UserRepository(UrlDbContext context)
    {
        _context = context;
        _userDbSet = context.UsersDbSet;
    }

    public async Task CreateUserAsync(Users user)
    {
        await _userDbSet.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> IsUserNameExistAsync(string userName)
    {
        int count = await _userDbSet.CountAsync(u => u.UserName.ToLower() == userName.ToLower());
        return count > 0;
    }
}