using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;
using System;
using TRIP.Platform.Service.Core.Models.ViewModel;

namespace TRIP.Platform.Service.API.Middleware
{
	public class ExceptionMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<ExceptionMiddleware> _logger;
		public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
		{
			_logger = logger;
			_next = next;
		}
		public async Task InvokeAsync(HttpContext httpContext)
		{
			try
			{
				await _next(httpContext);
			}
			catch (AccessViolationException avEx)
			{
				await HandleExceptionAsync(httpContext, avEx);
			}
			catch (Exception ex)
			{
				await HandleExceptionAsync(httpContext, ex);
			}
		}
		private async Task HandleExceptionAsync(HttpContext context, Exception exception)
		{
			context.Response.ContentType = "application/json";
			context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
			var refId = Guid.NewGuid();
			var message = $"Internal Server Error. Please contact admin for more information. Ref# {refId}";
			if (exception.GetType() == typeof(AccessViolationException))
			{
				message = $"Access violation error. Please contact admin for more information. Ref# {refId}";
			}
			_logger.LogError($"Something went wrong - Ref# {refId}: {exception}");
			await context.Response.WriteAsync(new ErrorDetails()
			{
				StatusCode = context.Response.StatusCode,
				Message = message
			}.ToString());
		}
	}
}
