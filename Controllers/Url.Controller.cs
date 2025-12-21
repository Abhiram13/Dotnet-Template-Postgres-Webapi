using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UrlShortner.Helper;
using UrlShortner.Models;
using UrlShortner.Services;
using UrlShortner.Entities;

namespace UrlShortner.Controllers;

[ApiController]
[Route("api/urls")]
public class UrlController : BaseApiController
{
    private readonly ILogger<UrlController> _logger;
    private readonly UrlService _urlService;
    private readonly MetaDataService _metaService;

    public UrlController(ILogger<UrlController> logger, UrlService urlService, MetaDataService metaDataService)
    {
        _logger = logger;
        _urlService = urlService;
        _metaService = metaDataService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateShortUrlAsync([FromBody] AddUrlDto body)
    {
        await _urlService.CreateShortUrlAsync(body);
        return Ok(new ApiResponse
        {
            StatusCode = System.Net.HttpStatusCode.Created,
            Message = "Short Url is successfully created"
        });
    }

    [HttpGet("{shortCode}")]
    public async Task<IActionResult> GetShortCodeDetailsAsync([FromRoute] string shortCode)
    {
        ShortCodeDetails details = await _urlService.GetShortCodeDetailsAsync(shortCode);
        return Ok(new ApiResponse<ShortCodeDetails>
        {
            StatusCode = System.Net.HttpStatusCode.OK,
            Result = details
        });
    }

    // [HttpGet]
    // public async Task<IActionResult> GetAllUrlsAsync()
    // {
    //     List<Url> urls = await _urlService.GetAllUrlsAsync();
    //     return Ok(urls);
    // }

    // [HttpGet("{urlShortCode}")]
    // public async Task<IActionResult> GetShortCodeDetails([FromRoute] string urlShortCode)
    // {
    //     Url url = await _urlService.GetShortUrlByCodeAsync(shortCode: urlShortCode);
    //     UrlMetaData? existingMetaData = await _metaService.GetMetaDataByUrlIdAsync(urlId: url.Id);

    //     if (existingMetaData is null)
    //     {
    //         await _metaService.CreateMetaData(urlId: url.Id);
    //     }
    //     else
    //     {
    //         await _metaService.UpdateMetaData(urlId: url.Id);
    //     }

    //     return Redirect(url.OriginalUrl);
    // }
}