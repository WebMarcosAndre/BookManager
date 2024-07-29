namespace BookManager.API
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.Data.SqlClient;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using System;
    using System.Net;
    using System.Threading.Tasks;

    public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger<ExceptionMiddleware> _logger = logger;

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Error DataBase");
                await HandleExceptionAsync(httpContext, ex);
            }
            catch (NullReferenceException ex)
            {
                _logger.LogError(ex, "Error Null Reference");
                await HandleExceptionAsync(httpContext, ex);

            }
            catch (Exception ex)
            {
                GenerateLogError(ex, httpContext);
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var result = new
            {
                Message = "Internal Server Error. Please try again later.",
            };

            return context.Response.WriteAsync(JsonConvert.SerializeObject(result));
        }

        private void GenerateLogError(Exception exception, HttpContext httpContext)
        {
            var serializedRequest = GetValueFromContext(httpContext);

            _logger.LogError(exception, "Error request: {@serializedRequest}", serializedRequest);
        }

        private string GetValueFromContext(HttpContext httpContext)
        {
            var parameterInfos = new List<string>();
            parameterInfos.AddRange(httpContext?.Request.Query.Keys.Select(key => $"{key}={httpContext.Request.Query[key]}"));
            parameterInfos.AddRange(httpContext.Request.RouteValues.Keys.Select(key => $"{key}={httpContext.Request.Query[key]}"));

            return JsonConvert.SerializeObject(parameterInfos);
        }
    }
}
