using Microsoft.AspNetCore.Mvc;
using UrlShortner.Models;
using UrlShortner.Services;

namespace UrlShortner.Controllers;

[ApiController]
[Route("urls")]
public class UnAuthUrlController : ControllerBase
{
    private readonly ILogger<UnAuthUrlController> _logger;
    private readonly UrlService _urlService;    

    public UnAuthUrlController(ILogger<UnAuthUrlController> logger, UrlService urlService)
    {
        _logger = logger;
        _urlService = urlService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateShortUrlAsync([FromBody] AddUrlDto body)
    {
        Url url = await _urlService.CreateShortUrlAsync(body);
        return Ok(url);
    }

    [HttpGet("{shortCode}")]
    public async Task<IActionResult> GetMetaData([FromRoute] string shortCode)
    {
        return Ok("Returns Metadata of Short code URL");
    }
}