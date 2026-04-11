using Microsoft.EntityFrameworkCore;
using webapi.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<MyAppDbContext>(opt => opt.UseSqlServer(builder.Configuration["ConnectionStrings:Default"]));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

using (var scope = app.Services.CreateScope())
{
    ILogger<Program> logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    try
    {
        
        var db = scope.ServiceProvider.GetRequiredService<MyAppDbContext>();
        db.Database.OpenConnection();
        logger.LogInformation("Database connect successfully!");
        db.Database.CloseConnection();
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Database connect failed!");
    }
}

app.Run();