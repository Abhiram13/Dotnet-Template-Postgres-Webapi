using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using UrlShortner.Models;
using UrlShortner.Services;

namespace UrlShortner.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService service)
    {
        _userService = service;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUserAsync([FromBody] Users user)
    {
        Users result = await _userService.RegisterUserAsync(user);

        return Ok("User created successfully");
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginDto loginPayload)
    {
        string token = await _userService.LoginUserAsync(username: loginPayload.UserName, password: loginPayload.Password);

        return Ok(new { token });
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        Console.WriteLine(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        Console.WriteLine(User.FindFirst(ClaimTypes.Role)?.Value);
        Console.WriteLine(User.FindFirst(ClaimTypes.Expiration)?.Value);
        Console.WriteLine(User.FindFirst(ClaimTypes.Expired)?.Value);        

        foreach (Claim c in User.Claims)
        {
            Console.WriteLine("Key: {0}, Value: {1}", c.Type, c.Value);            
        }

        List<Users> users = await _userService.GetAllUserAsync();
        return Ok(users);
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUserByIdAsync(int userId)
    {
        Users? user = await _userService.GetByIdAsync(userId);

        if (user is null) return NotFound();

        return Ok(user);
    }
}