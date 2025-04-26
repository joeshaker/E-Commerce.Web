using System.Net;
using DomainLayer.Execptions;
using Shared.ErrorModels;

namespace E_Commerce.Web.CustomeExceptionMiddleWare
{
    public class CustomeExceptionHandlerMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomeExceptionHandlerMiddleWare> _logger;

        public CustomeExceptionHandlerMiddleWare(RequestDelegate Next , ILogger<CustomeExceptionHandlerMiddleWare> Logger)
        {
            _next = Next;
            _logger = Logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next.Invoke(httpContext);
                await HandelNotFoundEndPointAsync(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the request.");

                //httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await HandleExceptionsAsync(httpContext, ex);
            }
        }

        private static async Task HandleExceptionsAsync(HttpContext httpContext, Exception ex)
        {
            httpContext.Response.StatusCode = ex switch
            {
                NotFoundException _ => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError,
            };
            httpContext.Response.ContentType = "application/json";

            var errorResponse = new ErrorToReturn
            {
                StatusCode = httpContext.Response.StatusCode,
                ErrorMessage = ex.Message
            };

            await httpContext.Response.WriteAsJsonAsync(errorResponse);
        }

        private static async Task HandelNotFoundEndPointAsync(HttpContext httpContext)
        {
            if (httpContext.Response.StatusCode == StatusCodes.Status404NotFound)
            {
                var Error = new ErrorToReturn
                {
                    StatusCode = httpContext.Response.StatusCode,
                    ErrorMessage = $"End Point {httpContext.Request.Path} is not found"
                };

                await httpContext.Response.WriteAsJsonAsync(Error);
            }
        }
    }
}
