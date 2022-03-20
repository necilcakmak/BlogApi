using Blog.Business.Abstract.RedisCache;
using Blog.Core.Results;
using Blog.Core.Utilities;
using Blog.Entities.Entities;
using Blog.Repository.EntityFramework.Context;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Blog.Api.Filters
{
    public class AuthorizeFilter : ActionFilterAttribute
    {
        string RolValue;
        public AuthorizeFilter()
        {

        }
        public AuthorizeFilter(string RolValue)
        {
            this.RolValue = RolValue;
        }
        public override async void OnActionExecuting(ActionExecutingContext context)
        {
            var key = context.HttpContext.Request.Headers.Authorization.ToString();
            var service = context.HttpContext.RequestServices;
            var _redisService = service.GetService<IRedisService>();

            var user = _redisService.Get<User>(key);
            if (user == null || user.RoleName != RolValue)
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.HttpContext.Response.ContentType = "application/json";
                await context.HttpContext.Response.WriteAsync(
                        new Result(false, "UnAuthorizedRequest").ToJson());
                return;
            }

            BlogDbContext.UserId = user.Id;
            base.OnActionExecuting(context);
        }
    }
}
