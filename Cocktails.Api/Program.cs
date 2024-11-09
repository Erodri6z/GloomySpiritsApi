using Cocktails.Api.Data;
// using Cocktails.Api.Dtos;
using Cocktails.Api.Endpoints;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);


Env.Load();


builder.Configuration.AddEnvironmentVariables();

builder.Services.AddSingleton<MongoDbContext>();

var app = builder.Build();

app.MapDrinksEndpoints();

app.Run();
