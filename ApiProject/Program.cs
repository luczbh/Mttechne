using ApiProject.Infrastructure.Repository;
using ApiProject.Interfaces.Services;
using ApiProject.Repository;
using ApiProject.Service;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

Log.Logger = new LoggerConfiguration()
               .ReadFrom.Configuration(builder.Configuration)
               .CreateLogger();

builder.Logging.ClearProviders();

builder.Services.AddSerilog(Log.Logger);

// Add services to the container.
builder.Services.AddDbContext<IApiProjectContext, OperationDBContext>(
        options => options.UseSqlServer("name=ConnectionStrings:OperationDatabase"));

Log.Debug("Added Context");

builder.Services.AddScoped<IOperationService, OperationService>();
builder.Services.AddScoped<IBalanceService, BalanceService>();
builder.Services.AddScoped<IApiProjectRepository, ApiProjectRepository>();

Log.Debug("Added Services");

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var xmlPath = Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");

builder.Services.AddSwaggerGen(c => c.IncludeXmlComments(xmlPath));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<OperationDBContext>();
    db.Database.Migrate();
}

Log.Debug("Migrated.");

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseExceptionHandler(config =>
{
    config.Run(async context =>
    {
        var error = context.Features.Get<IExceptionHandlerFeature>();

        if (error != null)
        {
            var ex = error.Error;
            Log.Error("Error", ex);
        }

        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(new
        {
            StatusCode = 500,
            ErrorMessage = "Internal Error",
            TraceId = context.TraceIdentifier
        }.ToString()); //ToString() is overridden to Serialize object
    });
});

Log.Debug("Running");

app.Run();
