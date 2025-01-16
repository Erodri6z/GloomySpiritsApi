using Cocktails.Api.Data;
// using Cocktails.Api.Dtos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Cocktails.Api.Endpoints;
using DotNetEnv;
using CloudinaryDotNet.Actions;
using Cocktails.Api.Services;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<MongoDbContext>();

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


var cloud = Environment.GetEnvironmentVariable("CLOUDINARY_UR");

builder.Services.AddSingleton(new CloudinaryService(cloud));

builder.Services.AddAuthorization(); 


var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();


app.MapDrinksEndpoints();
app.MapProfilesEndpoints();

app.Run();