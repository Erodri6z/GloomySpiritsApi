using Cocktails.Api.Data;
// using Cocktails.Api.Dtos;
using Cocktails.Api.Endpoints;
using DotNetEnv;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

// var mongoConnectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");

builder.Services.AddSingleton<MongoDbContext>();


var app = builder.Build();

app.MapDrinksEndpoints();
app.MapProfilesEndpoints();

app.Run();