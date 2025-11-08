using Microsoft.AspNetCore.Mvc;
using UrlShortner.Models;
using UrlShortner.Services;

namespace UrlShortner.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : BaseController
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
}