using Common.EventBus.Local;
using Common.EventBus.Shared;
using Common.Security.Users;
using FluentValidation;
using FluentValidation.AspNetCore;
using Messages.API.Extensions;
using Messaging.API.Extensions;
using Messaging.Application.Features.Rooms.Commands.CreateRoom;
using Messaging.Domain.Entities;
using Messaging.Domain.Interfaces;
using Messaging.Infrastructure;
using Messaging.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Rooms.API.Extensions;
using Serilog;
using System.Text.Json;
using System.Text.Json.Serialization;
using Users.API.Configurations;
using Users.Application;
using Users.Application.Mapper;
using Users.Infrastructure.Repositories;

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
builder.Services.AddAutoMapper(typeof(MapperProfile).Assembly);
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreateRoomCommandHandler>());
builder.Services.AddFluentValidationAutoValidation().AddValidatorsFromAssemblyContaining<IAssemblyEntry>();

builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();

builder.Services.AddDbContext<RoomDbContext>(options =>
{   
	options.UseSqlServer(builder.Configuration.GetConnectionString("RoomDB"));

});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.ConfigureSwaggerUI(builder.Configuration);
}

app.UseRouting();
app.UseCors(MyAllowSpecificOrigins);
//app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.UseApiExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

//app.MapRoomEndpoints();
//app.MapMessageEndpoints();

app.Run();
