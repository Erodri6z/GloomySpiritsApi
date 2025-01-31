using Cocktails.Api.Data;
// using Cocktails.Api.Dtos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Cocktails.Api.Endpoints;
using DotNetEnv;
using CloudinaryDotNet.Actions;
using Cocktails.Api.Services;
using CloudinaryDotNet;

Env.Load();

var builder = WebApplication.CreateBuilder(args);


var jwtSecret = Environment.GetEnvironmentVariable("JWT_SECRET") ?? "Development_Secret_Key_pls_no_sharing";

if (string.IsNullOrEmpty(jwtSecret))
{
  throw new ArgumentNullException("No Secret");
};

var key = Encoding.UTF8.GetBytes(jwtSecret);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
  options.TokenValidationParameters = new TokenValidationParameters
  {
    ValidateIssuerSigningKey = true,
    IssuerSigningKey = new SymmetricSecurityKey(key),
    ValidateIssuer = false,
    ValidateAudience = false,
    ValidateLifetime = true,
  };
});


builder.Services.AddCors(options =>
{
  options.AddPolicy("AllowAll", policy =>
  {
    policy.AllowAnyOrigin()
      .AllowAnyMethod()
      .AllowAnyHeader();
  });
});

var cloudName = Environment.GetEnvironmentVariable("CLOUDINARY_CLOUD_NAME");
var apiKey = Environment.GetEnvironmentVariable("CLOUDINARY_API_KEY");
var apiSecret = Environment.GetEnvironmentVariable("CLOUDINARY_API_SECRET");


builder.Services.AddSingleton(new CloudinaryService(cloudName, apiKey, apiSecret)); 

builder.Services.AddSingleton<MongoDbContext>();

builder.Services.AddAuthorization(); 


var app = builder.Build();

app.UseStaticFiles(); 
app.UseAuthentication();
app.UseAuthorization();

app.UseCors("AllowAll");

app.MapDrinksEndpoints();
app.MapProfilesEndpoints();

app.Run();