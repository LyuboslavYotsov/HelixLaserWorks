using HelixLaserWorks.Middlewares;

namespace HelixLaserWorks.Extensions
{
	public static class IApplicationBuilderExtensions
	{
		public static IApplicationBuilder UseErrorHandler(this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<ErrorHandlerMiddleware>();
		}
	}
}
