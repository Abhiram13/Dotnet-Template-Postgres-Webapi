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
    private readonly MetaDataService _metaService;
    private readonly UrlService _urlService;

    public InitController(ILogger<InitController> logger, MetaDataService metaDataService, UrlService urlService)
    {
        _logger = logger;
        _metaService = metaDataService;
        _urlService = urlService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        _logger.LogInformation("Hello");
        return Ok("Hello. This is Postgres Template");
    }

    [HttpGet("{urlShortCode}")]
    public async Task<IActionResult> GetShortCodeDetails([FromRoute] string urlShortCode)
    {
        Url url = await _urlService.GetShortUrlByCodeAsync(shortCode: urlShortCode);
        UrlMetaData? existingMetaData = await _metaService.GetMetaDataByUrlIdAsync(urlId: url.Id);

        if (existingMetaData is null)
        {
            await _metaService.CreateMetaData(urlId: url.Id);
        }
        else
        {
            await _metaService.UpdateMetaData(urlId: url.Id);
        }

        return Redirect(url.OriginalUrl);
    }
}