using Blog.Business.Abstract.RedisCache;
using Blog.Core.Results;
using Blog.Core.Utilities;
using Blog.Entities.Entities;
using Blog.Repository.EntityFramework.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NuGet.Common;
using System.Net;

namespace Blog.Api.Filters
{
    public class AuthorizeFilter : ActionFilterAttribute
    {
        string RolValue;
        public AuthorizeFilter()
        {
            RolValue = "User";
        }
        public AuthorizeFilter(string RolValue)
        {
            this.RolValue = RolValue;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var token = context.HttpContext.Request.Cookies["authToken"] ?? context.HttpContext.Request.Headers.Authorization.ToString();
            ;
            if (string.IsNullOrEmpty(token))
            {
                context.Result = new UnauthorizedObjectResult(new Result(false, "UnAuthorizedRequest"));
                return;
            }

            var _redisService = context.HttpContext.RequestServices.GetService<IRedisService>();

            var redisKey = token.TokenToRedisId().ToString();
            var user = _redisService.Get<User>(redisKey);
            if (user == null || user.RoleName != "Admin" && user.RoleName != RolValue)
            {
                context.Result = new UnauthorizedObjectResult(new Result(false, "UnAuthorizedRequest"));
                return;
            }

            BlogDbContext.UserId = user.Id;
            await next();
        }

    }
}
