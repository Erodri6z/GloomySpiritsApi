using Cocktails.Api.Data;
// using Cocktails.Api.Dtos;
using Cocktails.Api.Endpoints;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);


Env.Load();

var app = builder.Build();

builder.Configuration.AddEnvironmentVariables();

builder.Services.AddSingleton<MongoDbContext>();

app.MapDrinksEndpoints();

app.Run();
