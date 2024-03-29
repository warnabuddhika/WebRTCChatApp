﻿namespace Messaging.API.Helpers;

using Common.Domain.Repositories;
using Messaging.API.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;

public class LogUserActivity : IAsyncActionFilter
{
	public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
	{
		var resultContext = await next();

		if (!resultContext.HttpContext.User.Identity.IsAuthenticated)
		{
			return;
		}

		//var userId = resultContext.HttpContext.User.GetUserId();
		//var repo = resultContext.HttpContext.RequestServices.GetService<IUnitOfWork>();
		////GetService: Microsoft.Extensions.DependencyInjection
		//var user = await repo.UserRepository.GetUserByIdAsync(userId);
		//user.LastActive = DateTime.Now;
		//await repo.Complete();//add this: services.AddScoped<LogUserActivity>(); [ServiceFilter(typeof(LogUserActivity))] dat truoc controller base
	}
}


