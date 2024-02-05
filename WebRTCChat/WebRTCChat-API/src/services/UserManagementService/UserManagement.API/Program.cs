using Common.EventBus.Local;
using Common.EventBus.Shared;
using Common.Security.Users;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Text.Json;
using System.Text.Json.Serialization;
using UserManagement.API.Extensions;
using UserManagement.API.Helpers;
using UserManagement.Application.Interfaces;
using UserManagement.Application.Mapper;
using UserManagement.Application.Services;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Interfaces;
using UserManagement.Infrastructure;
using UserManagement.Infrastructure.Extensions;
using UserManagement.Infrastructure.Interfaces;
using UserManagement.Infrastructure.Repositories;
using Users.API.Configurations;
using Users.API.Extensions;
using Users.Application;
using UserManagement.Application.Features.Users.Commands.LoginUser;

string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

#pragma warning disable CA1305 // Specify IFormatProvider
builder.Host.UseSerilog((context, services, configuration) => configuration
                .ReadFrom.Configuration(context.Configuration)
                .ReadFrom.Services(services)
                .Enrich.FromLogContext()
                .WriteTo.Console());
#pragma warning restore CA1305 // Specify IFormatProvider


builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    })
    .ConfigureApi();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer().AddSwagger(builder.Configuration);
builder.Services.AddSwaggerGen();

// Add JWT authentication 
//https://learn.microsoft.com/en-us/azure/active-directory/develop/web-api-quickstart?pivots=devlang-aspnet-core
//builder.Services.AddAuthentication()
//    .AddJwtBearer(x =>
//    {
//        x.Authority = "https://dev.Okta.com/oauth2/default";
//        x.Audience = "api://default";
//    });
//builder.Services.AddIdentity<AppUser, AppRole>()
//		.AddEntityFrameworkStores<UserDbContext>()
//		.AddDefaultTokenProviders();

	builder.Services.AddCors(options =>
	{
		options.AddPolicy(name: MyAllowSpecificOrigins,
						  builder =>
						  {
							  builder.WithOrigins("https://localhost:4200")
											  .AllowAnyHeader()
											  .AllowAnyMethod()
											  .AllowCredentials();
						  });
	});
	builder.Services.AddIdentity(builder.Configuration);

	builder.Services.AddScoped<ICurrentUser, DummyCurrentUser>();
	builder.Services.AddScoped<ILocalEventBus, LocalEventBus>();
	builder.Services.AddScoped<ISharedEventBus, DummySharedEventBus>();
	builder.Services.AddScoped<ITokenService, TokenService>();
	builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
	builder.Services.AddScoped<LogUserActivity>();
	builder.Services.AddAutoMapper(typeof(MapperProfile).Assembly);
	builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<LoginUserCommandHandler>());
	builder.Services.AddFluentValidationAutoValidation().AddValidatorsFromAssemblyContaining<IAssemblyEntry>();
	builder.Services.AddScoped<IUserRepository, UserRepository>();

	builder.Services.AddDbContext<UserDbContext>(options =>
	{    
		options.UseSqlServer(builder.Configuration.GetConnectionString("UserDB"));

	});

	
	
	var app = builder.Build();
try
		{
			using var scope = app.Services.CreateScope();
			var services = scope.ServiceProvider;

			var userManager = services.GetRequiredService<UserManager<AppUser>>();
			var roleManager = services.GetRequiredService<RoleManager<AppRole>>();
			
			await Seed.SeedUsers(userManager, roleManager);
		}
		catch (Exception ex)
		{
			var logger = app.Services.GetRequiredService<ILogger<Program>>();
			logger.LogError(ex, "An error occurred during migration");
		}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
	{
		app.UseSwagger();
		app.ConfigureSwaggerUI(builder.Configuration);
	}
	app.UseApiExceptionHandler();

	app.UseHttpsRedirection();
	app.UseRouting();
	app.UseCors(MyAllowSpecificOrigins);
	//app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());



	app.UseAuthentication();

	app.UseAuthorization();

	app.MapControllers();

	app.MapUserEndpoints();

	app.Run();

