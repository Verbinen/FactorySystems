using Microsoft.EntityFrameworkCore;
using System.Net.Mime;
using WebApi.Models;

namespace WebApi.Handlers
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleException(ex, context);
            }
        }

        private async Task HandleException(Exception ex, HttpContext context)
        {
            ErrorResponse response = new();

            switch (ex)
            {
                case ApiException apiException:
                    response.StatusCode = apiException.StatusCode;
                    response.ErrorMessage = apiException.Message;
                    break;

                case DbUpdateConcurrencyException:
                    response.StatusCode = StatusCodes.Status500InternalServerError;
                    response.ErrorMessage = "Concurrency conflict updating system";
                    break;

                default:
                    response.StatusCode = StatusCodes.Status500InternalServerError;
                    response.ErrorMessage = "An internal error has ocurred.";
                    _logger.LogError(ex, "Unknown error");
                    break;
            }

            context.Response.StatusCode = response.StatusCode;
            context.Response.ContentType = MediaTypeNames.Application.Json;
            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
