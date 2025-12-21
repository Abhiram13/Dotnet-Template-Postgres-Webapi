using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UrlShortner.Helper;
using UrlShortner.Models;
using UrlShortner.Services;

namespace UrlShortner.Controllers;

[ApiController]
[Route("")]
public class InitController : BaseApiController
{
    private readonly ILogger<InitController> _logger;

    public InitController(ILogger<InitController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Hello. This is a Url Shortnet project");
    }    
}