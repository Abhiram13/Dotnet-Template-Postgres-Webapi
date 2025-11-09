using Microsoft.AspNetCore.Identity;
using UrlShortner.Models;

namespace UrlShortner.Helper;

public class PasswordHash
{
    private readonly IPasswordHasher<Users> _hasher;

    public PasswordHash()
    {
        _hasher = new PasswordHasher<Users>();
    }

    public string HashPassword(Users user, string password)
    {
        return _hasher.HashPassword(user, password);
    }

    public PasswordVerificationResult VerifyPassword(Users user, string password)
    {
        PasswordVerificationResult result = _hasher.VerifyHashedPassword(user, user.Password, password);
        return result;
    }
}