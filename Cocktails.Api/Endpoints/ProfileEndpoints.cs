using Cocktails.Api.Data;
using Cocktails.Api.Dtos;
using Cocktails.Api.Entities;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using BCrypt.Net;
using Sprache;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;


namespace Cocktails.Api.Endpoints; 

public static class ProfileEndpoints
{
  public static RouteGroupBuilder MapProfilesEndpoints(this WebApplication app)
  {
    var group = app.MapGroup("profiles");

    group.MapGet("/{id}", async (string id, [FromServices] MongoDbContext context) => 
    {
      var filter = Builders<ProfileDto>.Filter.Eq(profile => profile.Id, id);
      var profile = await context.Profiles.Find(filter).FirstOrDefaultAsync();

      return profile is null ? Results.NotFound() : Results.Ok(profile);

    });

    group.MapPost("/", async ([FromServices] MongoDbContext context, CreateProfileDto newProfile) => 
    {
      string hashedPassword = BCrypt.Net.BCrypt.HashPassword(newProfile.PasswordHash);
      DateTime now = DateTime.Now;

      ProfileDto profile = new ProfileDto
      {
        Username = newProfile.Username,
        Email = newProfile.Email,
        PasswordHash = hashedPassword,
        DateOfBirth = now
      };

      await context.Profiles.InsertOneAsync(profile);

      return Results.Created($"/profiles/{profile.Id}", profile);
    })
    .WithParameterValidation();

    group.MapPost("/login", async ([FromServices] MongoDbContext context, LoginRequest login) => 
    {
      var user = await context.Profiles.Find(p => p.Username == login.Username).FirstOrDefaultAsync();

      bool isPasswordValid = BCrypt.Net.BCrypt.Verify(login.Password, user.PasswordHash);

      if (user == null) 
      {
        return Results.NotFound();
      } 
      
      if (!isPasswordValid)
      {
        return Results.Unauthorized();
      } 

      var jwtSecret = Environment.GetEnvironmentVariable("JWT_SECRET") ?? "Development_Secret_Key_pls_no_sharing";
      var tokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.UTF8.GetBytes(jwtSecret);
      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(new[]
        {
          new Claim(ClaimTypes.Name, user.Username),
          new Claim(ClaimTypes.Email, user.Email),
          new Claim("UserId", user.Id)
        }),
        Expires = DateTime.UtcNow.AddDays(1),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
      };

      var token = tokenHandler.CreateToken(tokenDescriptor);
      var tokenString = tokenHandler.WriteToken(token);

      return Results.Ok(new { Token = tokenString });
    });

    group.MapDelete("/{id}", [Authorize](string id, MongoDbContext context) => 
    {
      context.Profiles.DeleteOne(profile => profile.Id == id);


      return Results.NoContent();
    });


    return group;
  }

} 