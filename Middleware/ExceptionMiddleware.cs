using System;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MyWebApi.Dtos;

namespace MyWebApi.Middleware
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


        public class ForbiddenAccessException : Exception
        {
            public ForbiddenAccessException() { }

            public ForbiddenAccessException(string message) : base(message) { }

            public ForbiddenAccessException(string message, Exception inner) : base(message, inner) { }
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (UnauthorizedAccessException ex)
            {

                _logger.LogError(ex, "Unauthorized access");
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                var result = new ResultDTO
                {
                    Success = false,
                    Message = "Bạn chưa xác thực, vui lòng đăng nhập.",
                    StatusCode = 401
                };
                var json = JsonSerializer.Serialize(result);
                await context.Response.WriteAsync(json);
            }
            catch (ForbiddenAccessException ex)
            {

                _logger.LogError(ex, "Forbidden access");
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                var result = new ResultDTO
                {
                    Success = false,
                    Message = "Bạn không có quyền truy cập tài nguyên này.",
                    StatusCode = 403
                };
                var json = JsonSerializer.Serialize(result);
                await context.Response.WriteAsync(json);
            }
            catch (ArgumentException ex)
            {

                _logger.LogError(ex, "Bad Request");
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                var result = new ResultDTO
                {
                    Success = false,
                    Message = "Dữ liệu đầu vào không hợp lệ.",
                    StatusCode = 400
                };
                var json = JsonSerializer.Serialize(result);
                await context.Response.WriteAsync(json);
            }
            catch (KeyNotFoundException ex)
            {

                _logger.LogError(ex, "Not Found");
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                var result = new ResultDTO
                {
                    Success = false,
                    Message = "Tài nguyên không tìm thấy.",
                    StatusCode = 404
                };
                var json = JsonSerializer.Serialize(result);
                await context.Response.WriteAsync(json);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Internal Server Error");
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                var result = new ResultDTO
                {
                    Success = false,
                    Message = "Đã xảy ra lỗi hệ thống, vui lòng thử lại sau.",
                    StatusCode = 500
                };
                var json = JsonSerializer.Serialize(result);
                await context.Response.WriteAsync(json);
            }
        }
    }
}
