namespace UserManagement.Infrastructure;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

public class PersistedGrantDbContextFactory : IDesignTimeDbContextFactory<PersistedGrantDbContext>
{
	public PersistedGrantDbContext CreateDbContext(string[] args)
	{
		var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

		// Build config
		IConfiguration config = new ConfigurationBuilder()
		.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../UserManagement.API"))
		.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
		.AddJsonFile($"appsettings.{environment}.json", optional: true)
		.AddEnvironmentVariables()
		.Build();
		var connectionString = config.GetConnectionString("UserDB");

		var optionsBuilder = new DbContextOptionsBuilder<PersistedGrantDbContext>();
		optionsBuilder.UseSqlServer(connectionString,
		sql => sql.MigrationsAssembly(typeof(PersistedGrantDbContextFactory).GetTypeInfo().Assembly.GetName().Name));

		return new PersistedGrantDbContext(optionsBuilder.Options, new OperationalStoreOptions());
	}
}
