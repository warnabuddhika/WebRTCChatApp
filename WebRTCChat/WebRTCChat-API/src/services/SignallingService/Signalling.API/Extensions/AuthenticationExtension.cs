﻿namespace Signalling.API.Extensions;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

public static class AuthenticationExtension
{
	public static IServiceCollection ConfigureAuthService(this IServiceCollection services,
			IConfiguration configuration)
	{
		services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
		   .AddJwtBearer(options =>
		   {
			   options.Authority = configuration.GetValue<string>("IdentityServer:Url");
			   options.RequireHttpsMetadata = configuration.GetValue<bool>("IdentityServer:RequireHTTPS");

			   options.TokenValidationParameters = new TokenValidationParameters
			   {
				   ValidateAudience = false
			   };
		   });

		services.AddAuthorization(options =>
		{
			options.AddPolicy("Signalling.ApiScope", policy =>
			{
				policy.RequireAuthenticatedUser();
				policy.RequireClaim("scope", "UserManagement.scope");
			});
		});

		return services;
	}
}
