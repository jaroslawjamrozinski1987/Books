using Books.Core.Exceptions;
using System.Net;

namespace Books.API.Middlewares
{
    public sealed class ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger) : IMiddleware
    {
        private readonly ILogger<ExceptionHandlingMiddleware> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        private const string SomethingWentWrong = "Something went wrong";

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await WriteErrorToResponse(context, ex);
            }
        }

        private async Task WriteErrorToResponse(HttpContext context, Exception exception)
        {
            if (exception is CustomException humanReadable)
            {
                context.Response.StatusCode = (int) HttpStatusCode.NotFound;
                await context.Response.WriteAsJsonAsync(new ErrorPayload(humanReadable.Message));
                return;
            }

            _logger.LogError(exception, exception.Message);
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(new ErrorPayload(SomethingWentWrong));
        }
    }

    internal record ErrorPayload(string ErrorMessage);
}
