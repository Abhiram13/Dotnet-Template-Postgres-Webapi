using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using UrlShortner.Exceptions;
using UrlShortner.Models;

namespace UrlShortner.Middlwares;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _requestDelegate;
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;
    private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = null };

    public ExceptionHandlerMiddleware(RequestDelegate requestDelegate, ILogger<ExceptionHandlerMiddleware> logger)
    {
        _requestDelegate = requestDelegate;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _requestDelegate(httpContext);
        }
        catch (UnauthorizedException exception)
        {
            HttpRequest request = httpContext.Request;
            string requestUrl = $"{request.Scheme}://{request.Host}{request.Path}{request.QueryString}";

            _logger.LogError(exception, message: "Unhandled Exception at Request = {0}. Exception message = {2}", requestUrl, exception.Message);
            
            httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
            httpContext.Response.ContentType = "application/json";

            await httpContext.Response.WriteAsJsonAsync(options: _jsonOptions, value: new ApiResponse
            {
                StatusCode = System.Net.HttpStatusCode.Unauthorized,                
                Message = "Invalid credentials provided"
            });
        }
        catch (BadRequestException exception)
        {
            HttpRequest request = httpContext.Request;
            string requestUrl = $"{request.Scheme}://{request.Host}{request.Path}{request.QueryString}";

            _logger.LogError(exception, message: "Unhandled Exception at Request = {0}. Exception message = {2}", requestUrl, exception.Message);
            
            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            httpContext.Response.ContentType = "application/json";

            await httpContext.Response.WriteAsJsonAsync(options: _jsonOptions, value: new ApiResponse
            {
                StatusCode = System.Net.HttpStatusCode.BadRequest,                
                Message = "Invalid request provided"
            });
        }
        catch (Exception exception)
        {
            HttpRequest request = httpContext.Request;
            string requestUrl = $"{request.Scheme}://{request.Host}{request.Path}{request.QueryString}";

            _logger.LogError(exception, message: "Unhandled Exception at Request = {0}. Exception message = {2}", requestUrl, exception.Message);
            
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            httpContext.Response.ContentType = "application/json";

            await httpContext.Response.WriteAsJsonAsync(options: _jsonOptions, value: new ApiResponse
            {
                StatusCode = System.Net.HttpStatusCode.InternalServerError,                
                Message = "Unhandled exception occured. Please check logs for more details"
            });
        }
    }
}