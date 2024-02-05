using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using UserManagement.API.Configurations;
using UserManagement.Domain.Entities;
using UserManagement.Infrastructure;

namespace UserManagement.API.Extensions;

public static class IdentityServiceExtensions
    {
	//public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
	//{
	//	services.AddIdentityCore<AppUser>(opt =>
	//	{
	//		opt.Password.RequireNonAlphanumeric = false;
	//		opt.Password.RequireUppercase = false;
	//		opt.Password.RequireDigit = false;
	//		opt.Password.RequireLowercase = false;
	//	})
	//	.AddRoles<AppRole>()
	//	.AddRoleManager<RoleManager<AppRole>>()
	//	.AddSignInManager<SignInManager<AppUser>>()
	//	.AddRoleValidator<RoleValidator<AppRole>>()
	//	.AddEntityFrameworkStores<UserDbContext>();



	//	services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(op =>
	//	{
	//		op.TokenValidationParameters = new TokenValidationParameters
	//		{
	//			ValidateIssuerSigningKey = true,
	//			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"])),
	//			ValidateIssuer = false,
	//			ValidateAudience = false
	//		};

	//		op.Events = new JwtBearerEvents
	//		{
	//			OnMessageReceived = context =>
	//			{
	//				var accessToken = context.Request.Query["access_token"];

	//				var path = context.HttpContext.Request.Path;
	//				if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/hubs"))
	//				{
	//					context.Token = accessToken;
	//				}

	//				return Task.CompletedTask;
	//			}
	//		};
	//	});

	//	return services;
	//}

	public static void AddIdentity(this IServiceCollection services,
		   IConfiguration configuration)
	{
		services.AddIdentity<AppUser, AppRole>(o =>
		{
			o.SignIn.RequireConfirmedEmail = true;
			o.User.RequireUniqueEmail = false;
			o.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
			o.Lockout.AllowedForNewUsers = true;
			o.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(365);
			o.Lockout.MaxFailedAccessAttempts = 5;
		})
			.AddEntityFrameworkStores<UserDbContext>()
			.AddDefaultTokenProviders()
			.AddTokenProvider<DataProtectorTokenProvider<AppUser>>("email");
		    //.AddClaimsPrincipalFactory<CustomUserClaimsPrincipalFactory>();

		services.Configure<DataProtectionTokenProviderOptions>(o =>
			o.TokenLifespan = TimeSpan.FromHours(24)
		);

		services.AddIdentityServer()
			.AddDeveloperSigningCredential()
			.AddOperationalStore(options =>
			{
				options.ConfigureDbContext = builder => builder.UseSqlServer(configuration.GetConnectionString("UserDB"));
				options.EnableTokenCleanup = true;
				options.TokenCleanupInterval = 30;
			})
			.AddInMemoryIdentityResources(Config.GetIdentityResources())
			.AddInMemoryApiScopes(Config.GetApiScopes())
			.AddInMemoryApiResources(Config.GetApiResources())
			.AddInMemoryClients(configuration.GetSection("IdentityServer:Clients"))
			.AddAspNetIdentity<AppUser>();

		services.AddAuthentication("Bearer")
		   .AddJwtBearer("Bearer", options =>
		   {
			   options.Authority = "https://localhost:7122"; // IdentityServer4 URL
			   options.Audience = "api1"; // Audience of the API
		   });
	}
}
