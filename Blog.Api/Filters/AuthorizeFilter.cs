using Blog.Business.Abstract.RedisCache;
using Blog.Core.Results;
using Blog.Core.Utilities;
using Blog.Entities.Entities;
using Blog.Repository.EntityFramework.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
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

        public override async void OnActionExecuting(ActionExecutingContext context)
        {
            var key = context.HttpContext.Request.Headers.Authorization.ToString();
            if (string.IsNullOrEmpty(key))
            {
                context.Result = new UnauthorizedObjectResult(new Result(false, "UnAuthorizedRequest"));
                return;
            }
            var service = context.HttpContext.RequestServices;
            var _redisService = service.GetService<IRedisService>();

            var redisKey = key.TokenToRedisId().ToString();
            var user = _redisService.Get<User>(redisKey);
            if (user == null || user.RoleName != "Admin" && user.RoleName != RolValue)
            {
                context.Result = new UnauthorizedObjectResult(new Result(false, "UnAuthorizedRequest"));
                return;
            }

            BlogDbContext.UserId = user.Id;
            base.OnActionExecuting(context);
        }
    }
}
