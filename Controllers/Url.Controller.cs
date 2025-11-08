using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UrlShortner.Helper;
using UrlShortner.Models;

namespace UrlShortner.Controllers;

[ApiController]
[Route("url")]
public class UrlController : BaseApiController
{
    private readonly ILogger<UrlController> _logger;
    private readonly DbSet<Url> _urlDbSet;
    private readonly UrlDbContext _context;

    public UrlController(ILogger<UrlController> logger, UrlDbContext context)
    {
        _logger = logger;
        _urlDbSet = context.UrlDbSet;
        _context = context;
    }

    [HttpGet]
    public IActionResult Get()
    {
        _logger.LogInformation("Hello");
        return Ok("Hello. This is Postgres Template");
    }    
}