using JwtBearer;
using JwtBearer.Models;
using JwtBearer.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<AuthService>();
builder.Services.AddSingleton<Configuration>();

var app = builder.Build();

app.MapGet("/auth", (AuthService service)
    => service.GenereteToken(new User(1, "user1@teste.com", "123", new[] { "user" })));

app.Run();