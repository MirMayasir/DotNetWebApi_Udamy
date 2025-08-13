namespace UdamyCourse.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly ILogger<ExceptionHandlerMiddleware> logger;
        private readonly RequestDelegate next;

        public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger,
            RequestDelegate next)
        {
            this.logger = logger;
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid();
                logger.LogError(ex, $"{errorId} : {ex.Message}");
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                var error = new
                {
                    ErrorId = errorId,
                    Message = "Something went wrong! We are looking into it."
                };

                await context.Response.WriteAsJsonAsync(error);
            }
        }
    }
}
