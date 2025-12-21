using UrlShortner.Entities;
using UrlShortner.Models;

namespace UrlShortner.Interfaces;

public interface IUserRepository
{
    Task CreateUserAsync(Users user);
    Task<bool> IsUserNameExistAsync(string userName);
    Task<Users?> GetByUsername(string userName);
}