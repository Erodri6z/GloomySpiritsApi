using Cocktails.Api.Dtos;
using Cocktails.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapDrinksEndpoints();

app.Run();
