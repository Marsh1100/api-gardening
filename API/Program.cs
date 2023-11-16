using System.Reflection;
using API.Extension;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Persistence;
using Persistence.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<GardeningContext>(optionBuilder =>
{
    string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    optionBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureCors();
builder.Services.AddAutoMapper(Assembly.GetEntryAssembly());
builder.Services.AddAplicacionServices();
builder.Services.ConfigureRateLimiting();
builder.Services.ConfigureApiVersioning();
builder.Services.AddJwt(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
	var services = scope.ServiceProvider;
	var loggerFactory = services.GetRequiredService<ILoggerFactory>();
	try
	{
		var context = services.GetRequiredService<GardeningContext>();
		await context.Database.MigrateAsync();
		await GardeningContextSeed.SeedAsync(context,loggerFactory);

	}
	catch (Exception ex)
	{
		var _logger = loggerFactory.CreateLogger<Program>();
		_logger.LogError(ex, "Ocurrio un error durante la migración");
	}
}

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
