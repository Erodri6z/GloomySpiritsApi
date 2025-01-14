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

    group.MapPost("/", async ([FromServices] MongoDbContext context, ProfileDto profile) => 
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

      return Results.Ok(profile);
    })
    .WithParameterValidation();


    return group;
  }

}