using Microsoft.AspNetCore.Diagnostics;
using QuebecAdventures.Domain.Exceptions;

namespace QuebecAdventures.API.Middleware
{
	public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
		: IExceptionHandler
	{
		public async ValueTask<bool> TryHandleAsync(
			HttpContext httpContext,
			Exception exception,
			CancellationToken cancellationToken)
		{
			logger.LogError(exception, "Unhandled exception occurred: {Message}", exception.Message);

			var (statusCode, message) = exception switch
			{
				NotFoundException notFound => (StatusCodes.Status404NotFound, notFound.Message),
				ValidationException validation => (StatusCodes.Status400BadRequest, validation.Message),
				DomainException domain => (StatusCodes.Status400BadRequest, domain.Message),
				_ => (StatusCodes.Status500InternalServerError, "An unexpected error occurred")
			};

			var response = new
			{
				error = message,
				statusCode,
				timestamp = DateTime.UtcNow,
				path = httpContext.Request.Path
			};

			httpContext.Response.StatusCode = statusCode;
			httpContext.Response.ContentType = "application/json";

			await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);
			return true;
		}
	}
}
