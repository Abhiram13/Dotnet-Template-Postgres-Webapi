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
    private readonly UrlService _urlService;

    public InitController(ILogger<InitController> logger, UrlService urlService)
    {
        _logger = logger;
        _urlService = urlService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new ApiResponse
        {
            StatusCode = System.Net.HttpStatusCode.OK,
            Message = "Welcome, this is URL Shortner project"
        });
    }

    [HttpGet("{shortCode}")]
    public async Task<IActionResult> GetLongUrl([FromRoute] string shortCode)
    {
        string? longUrl = await _urlService.GetLongUrlAsync(shortCode);

        if (string.IsNullOrEmpty(longUrl))
        {
            return NotFound(new ApiResponse
            {
                StatusCode = System.Net.HttpStatusCode.NotFound,
                Message = "Url not found"
            });
        }

        return Redirect(longUrl);
    }
}