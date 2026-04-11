using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using webapi.Application.Repositories;
using webapi.Application.Services.Jwt;
using webapi.Application.Services.UnitOfWorks;
using webapi.Application.UseCases.Base;
using webapi.Application.UseCases.SignIn;
using webapi.Infrastructure.Database;
using webapi.Infrastructure.Services.Jwt;
using webapi.Infrastructure.Services.UnitOfWork;
using webapi.WebAPI.SignalRHubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<MyAppDbContext>(opt => opt.UseSqlServer(builder.Configuration["ConnectionStrings:Default"]));

builder.Services.AddScoped<ISignInUC, SignInUC>();
builder.Services.AddScoped<ISignInUC, SignInUC>();

builder.Services.AddScoped<IUnitOfWorks, UnitOfWorks>();
builder.Services.AddScoped<IJwtService, JwtService>();

builder.Services.AddScoped<IUserRepository>();



var jwt = builder.Configuration.GetSection("Jwt");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,

        ValidateIssuerSigningKey = true,

        ValidIssuer = jwt["Issuer"],
        ValidAudience = jwt["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwt["Key"])
        )
    };
});


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

app.UseAuthentication();
app.UseAuthorization();

app.MapHub<ChatHub>("/chat");
app.Run();