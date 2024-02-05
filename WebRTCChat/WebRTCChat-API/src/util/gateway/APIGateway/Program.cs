using Keycloak.AuthServices.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
	options.AddPolicy("CorsPolicy",
		builder => builder
		.SetIsOriginAllowed((host) => true)
		.AllowAnyMethod()
		.AllowAnyHeader()
		.AllowCredentials());
});

var identityUrl = builder.Configuration.GetValue<string>("IdentityServer:Url");
var requireHTTPS = builder.Configuration.GetValue<bool>("IdentityServer:RequireHTTPS");
var identityApiKey = builder.Configuration.GetValue<string>("IdentityServer:IdentityApiKey");
builder.Services.AddAuthentication()
	.AddJwtBearer(identityApiKey, options =>
	{
		options.Authority = identityUrl;
		options.RequireHttpsMetadata = requireHTTPS;
		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateAudience = false
		};
	});

builder.Services.AddReverseProxy()
	   .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));
builder.Services.AddHealthChecks();
builder.Services.AddControllers();

var app = builder.Build();



// ...

app.UseRouting();
app.UseHttpsRedirection();

app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();
app.MapReverseProxy();

app.UseEndpoints(endpoints =>
{
	endpoints.MapReverseProxy();
});

app.Run();
