namespace HappyBookingCleanArchitectureServer.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            // Xử lý exception ở đây
            // Ví dụ: Ghi log, trả về một trang lỗi, gửi email thông báo, vv.
            // Sau đó, bạn có thể chuyển exception cho middleware tiếp theo nếu cần
            throw;
        }
    }
}

public static class ExceptionMiddlewareExtensions
{
    public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionMiddleware>();
    }
}

