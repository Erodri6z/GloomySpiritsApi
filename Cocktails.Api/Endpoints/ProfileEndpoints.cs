using Cocktails.Api.Data;
using Cocktails.Api.Dtos;
using Cocktails.Api.Entities;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using BCrypt.Net;
using Sprache;


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

      bool isPasswordValid = Bcrypt.Net.BCrypt.Verify(login.Password, user.PasswordHash);

      return  isPasswordValid && user != null ? Result.Ok("Login sucessful") : Result.NotFound();
    });


    return group;
  }

} 