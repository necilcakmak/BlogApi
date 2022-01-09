using Blog.Core.Results;
using Blog.Core.Utilities;
using Blog.Repository.EntityFramework.Context;

namespace Blog.APi.Middlewares
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ITokenHelper _tokenHelper;
        public AuthMiddleware(RequestDelegate next, ITokenHelper tokenHelper)
        {
            _tokenHelper = tokenHelper;
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                var token = context.Request.Headers["Authorization"].ToString();
                if (!string.IsNullOrEmpty(token))
                {
                    if (_tokenHelper.ValidateToken(token))
                    {
                        BlogDbContext.UserId = _tokenHelper.TokenToUserId(token);
                    }
                    else
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync(
                                new Result(false, "TokenNotValid").ToJson());
                        return;
                    }
                }
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(
                    new Result(false, "ServerException: " + ex.Message).ToJson());
                return;
            }
        }
    }
}
