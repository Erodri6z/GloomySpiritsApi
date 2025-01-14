using Cocktails.Api.Data;
using Cocktails.Api.Dtos;
using Cocktails.Api.Entities;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;


namespace Cocktails.Api.Endpoints; 

public static class ProfileEndpoints
{
  public static RouteGroupBuilder MapProfilesEndpoints(this WebApplication app)
  {
    var group = app.MapGroup("profiles");

    group.MapGet("/{id}", async (int id, [FromServices] MongoDbContext context) => 
    {
      var filter = Builders<ProfileDto>.Filter.Eq(profile => profile.Id, id);
      var profile = await context.Profiles.Find(filter).FirstOrDefaultAsync();

    });

    group.MapPost("/", async ([FromServices] MongoDbContext context, ProfileDto newProfile) => 
    {
      ProfileDto profile = new ProfileDto
      {
        Id = newProfile.Id, 
        Username = newProfile.Username,
        Email = newProfile.Email,
        PasswordHash = newProfile.PasswordHash,
        DateOfBirth = newProfile.DateOfBirth
      };

      await context.Profiles.InsertOneAsync(profile);

      return Results.Created($"/profiles/{profile.Id}", profile);
    })
    .WithParameterValidation();


    return group;
  }

}