namespace HelixLaserWorks.Middlewares
{
	public class ErrorHandlerMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger _logger;

		public ErrorHandlerMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
		{
			_next = next;
			_logger = loggerFactory.CreateLogger<ErrorHandlerMiddleware>();
		}

		public async Task Invoke(HttpContext context)
		{
			try
			{
				await this._next(context);

				if (context.Response.StatusCode == 404)
				{
					context.Response.Redirect("/pagenotfound");
				}

				if (context.Response.StatusCode == 500)
				{
					context.Response.Redirect("/error");
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Unexpected exception was thrown.");

				throw;
			}

		}

	}
}
