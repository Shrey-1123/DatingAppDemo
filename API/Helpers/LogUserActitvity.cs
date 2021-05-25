using System;
using System.Threading.Tasks;
using API.Extensions;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace API.Helpers
{
    // Implementing an Action filter to update user log and lat aaaactive property
    public class LogUserActitvity : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var resultContext = await next();

            if(!resultContext.HttpContext.User.Identity.IsAuthenticated)
            {
                return;
            }

             var userId = resultContext.HttpContext.User.GetUserId();
             var repo = resultContext.HttpContext.RequestServices.GetService<IUserRepository>();

             var user  = await repo.GetUserByIdAsync(userId);
             user.LastActive = DateTime.Now;
             await repo.SaveAllAsync();

             

        }
    }
}