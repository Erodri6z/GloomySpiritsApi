using Cocktails.Api.Dtos;
using Cocktails.Api.Endpoints;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

Env.Load();

var app = builder.Build();

app.MapDrinksEndpoints();

app.Run();
