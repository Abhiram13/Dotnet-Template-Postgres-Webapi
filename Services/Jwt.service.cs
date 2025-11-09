using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using UrlShortner.Models;

namespace UrlShortner.Services;

public class JwtService
{
    private readonly IConfiguration _configuration;
    private readonly JwtSecurityTokenHandler _securityTokenHandler;

    public JwtService(IConfiguration configuration)
    {
        _configuration = configuration;
        _securityTokenHandler = new JwtSecurityTokenHandler();
    }

    public string GenerateToken(Users user)
    {
        Claim[] claims = new Claim[]
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.Role.ToString()),
            new Claim(ClaimTypes.Hash, Guid.NewGuid().ToString())
        };

        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        DateTime expires = DateTime.UtcNow.AddHours(2);
        JwtSecurityToken token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Issuer"],
            claims: claims,
            expires: expires,
            signingCredentials: credentials
        );

        return _securityTokenHandler.WriteToken(token);
    }
}