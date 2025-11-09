using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UrlShortner.Helper;
using UrlShortner.Models;
using UrlShortner.Services;

namespace UrlShortner.Controllers;

[ApiController]
[Route("api/urls")]
public class UrlController : ControllerBase
{
    private readonly ILogger<UrlController> _logger;
    private readonly UrlService _urlService;

    public UrlController(ILogger<UrlController> logger, UrlService urlService)
    {
        _logger = logger;
        _urlService = urlService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUrlsAsync()
    {
        List<Url> urls = await _urlService.GetAllUrlsAsync();

        return Ok(urls);
    }
}