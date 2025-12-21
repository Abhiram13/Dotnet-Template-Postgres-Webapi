using UrlShortner.Entities;

namespace UrlShortner.Interfaces;

public interface IUserRepository
{
    Task CreateUserAsync(Users user);
    Task<bool> IsUserNameExistAsync(string userName);
}