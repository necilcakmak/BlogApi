using Blog.Business.Abstract.RedisCache;
using Blog.Core.Results;
using Blog.Core.Utilities;
using Blog.Entities.Entities;
using Blog.Repository.EntityFramework.Context;

namespace Blog.APi.Middlewares
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IRedisService _redisService;
        public AuthMiddleware(RequestDelegate next, IRedisService redisService)
        {
            _redisService = redisService;
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                //var token = context.Request.Headers["Authorization"].ToString();
                //if (!string.IsNullOrEmpty(token))
                //{
                //    if (await _redisService.InCache(token))
                //    {
                //        var user = await _redisService.GetAsync<User>(token);
                //        BlogDbContext.UserId = user.Id;
                //    }
                //    else
                //    {
                //        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                //        context.Response.ContentType = "application/json";
                //        await context.Response.WriteAsync(
                //                new Result(false, "TokenNotValid").ToJson());
                //        return;
                //    }
                //}
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
