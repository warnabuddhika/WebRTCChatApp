using Common.EventBus.Local;
using Common.EventBus.Shared;
using Common.Security.Users;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Signalling.API.Extensions;
using Signalling.API.SignalR;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Users.API.Configurations;
using Users.Application;
using Users.Domain.AggregateRoots.Users;

string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

#pragma warning disable CA1305 // Specify IFormatProvider
builder.Host.UseSerilog((context, services, configuration) => configuration
                .ReadFrom.Configuration(context.Configuration)
                .ReadFrom.Services(services)
                .Enrich.FromLogContext()
                .WriteTo.Console());
#pragma warning restore CA1305 // Specify IFormatProvider



// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    })
    .ConfigureApi();

builder.Services.AddEndpointsApiExplorer().AddSwagger(builder.Configuration);
builder.Services.AddSwaggerGen();



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
builder.Services.ConfigureAuthService(builder.Configuration);
builder.Services.AddScoped<ICurrentUser, DummyCurrentUser>();
builder.Services.AddScoped<ILocalEventBus, LocalEventBus>();
builder.Services.AddScoped<ISharedEventBus, DummySharedEventBus>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddFluentValidationAutoValidation().AddValidatorsFromAssemblyContaining<IAssemblyEntry>();
builder.Services.AddSignalR();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.ConfigureSwaggerUI(builder.Configuration);
}

app.UseRouting();

app.UseCors(MyAllowSpecificOrigins);

app.UseApiExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseEndpoints(endpoints =>
{

	endpoints.MapHub<VitalityHub>("hubs/presence");
	endpoints.MapHub<WebRTCChatHub>("hubs/chathub");
	endpoints.MapFallbackToController("Index", "Fallback");//publish
});

app.Run();
