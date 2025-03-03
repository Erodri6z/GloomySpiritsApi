// using System;
using System.Diagnostics.Metrics;
using System.Reflection;
using Cocktails.Api.Data;
using Cocktails.Api.Dtos;
using Cocktails.Api.Entities;
using Cocktails.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Identity;





namespace Cocktails.Api.Endpoints;

public static class DrinksEndpoints
{
  const string GetDrinkEndPointName = "GetDrink";                                                 

  public static RouteGroupBuilder MapDrinksEndpoints(this WebApplication app)
  {

    var group = app.MapGroup("drinks");
    
    // Get all Drinks

    group.MapGet("/", async ([FromServices] MongoDbContext context) => 
    {
      Console.WriteLine("this works");
      return await context.Drinks.Find(_ => true).ToListAsync();
    });

    // Get specific drinks details
    group.MapGet("/{id}", async (string id,  MongoDbContext context) => 
    {
      var filter = Builders<DrinkDto>.Filter.Eq(drink => drink.Id, id);
      var drink = await context.Drinks.Find(filter).FirstOrDefaultAsync();

      return drink is null ? Results.NotFound() : Results.Ok(drink);

    })
    .WithName(GetDrinkEndPointName);


    // Find Drink By Spirit
    group.MapGet("/byAlcohol/{spirit}", async (string spirit, MongoDbContext context) => 
    {
      var filter = Builders<DrinkDto>.Filter.Eq(drink => drink.MainSpirit, spirit);
      var drinks = await context.Drinks.Find(filter).ToListAsync();
      return drinks.Any() ? Results.Ok(drinks) : Results.NotFound();
    });


    // Find Drink By name
    group.MapGet("/byName/{name}", async (string name, MongoDbContext context) => 
    {
      var filter = Builders<DrinkDto>.Filter.Regex(drink => drink.Name, new BsonRegularExpression(name, "i"));
      var drinks = await context.Drinks.Find(filter).ToListAsync();

      return drinks.Any() ?  Results.Ok(drinks) : Results.NotFound();
    });

    //TODO: -
    group.MapGet("/byVibe/{vibe}", async (string vibe, MongoDbContext context) => 
    {
      var filter = Builders<DrinkDto>.Filter.Eq(drink => drink.Vibe, vibe);
      var drinks = await context.Drinks.Find(filter).ToListAsync();

      return drinks.Any() ? Results.Ok(drinks) : Results.NotFound();
    });

    // Create a drink
    // TODO: find a way to create images with cloudary
    group.MapPost("/",  async ([FromServices] MongoDbContext context, CreateDrinkDto newDrink,
    HttpRequest request) => 
    {
      Console.WriteLine("This is getting hit");
      //   var form = await request.ReadFormAsync();
      //   if (!form.ContainsKey("drinkData"))
      //       return Results.BadRequest(new { message = "Missing drinkData field in form." });


      //   // Deserialize drinkData from form field
      //   var newDrink = JsonSerializer.Deserialize<CreateDrinkDto>(drinkData);
      //   if (newDrink == null)
      //     return Results.BadRequest(new { message = "Invalid drinkData JSON format." });


      // string? imageUrl = null;
      // var file = request.Form.Files.FirstOrDefault();
      // if (file != null && file.Length > 0)
      // {
      //   using (var stream = file.OpenReadStream())
      //   {
      //     imageUrl = await cloudaryService.UploadImageAsync(stream, file.FileName);
      //   }
      // }

      if (newDrink == null)
      {
        return Results.BadRequest(new { message = "Invalid JSON: Request body is empty." });
      }

      DrinkDto drink = new DrinkDto
      {
        Name = newDrink.Name,
        MainSpirit = newDrink.MainSpirit,
        // Image = newDrink.Image,
        Ingredients = newDrink.Ingredients,
        MeasurementsOz = newDrink.MeasurementsOz,
        Bitters = newDrink.Bitters,
        Garnish = newDrink.Garnish,
        Color = newDrink.Color,
        RecommendedGlasses = newDrink.RecommendedGlasses,
        Notes = newDrink.Notes,
        Method = newDrink.Method,
        Credit = newDrink.Credit,
        Vibe = newDrink.Vibe
      };

      await context.Drinks.InsertOneAsync(drink) ;


    return Results.Created($"/drinks/{drink.Id}", drink);
    });
    // .WithParameterValidation();

    // // Update a drink

    group.MapPut("/{id}",  async (string id, UpdateDrinkDto updatedDrink, MongoDbContext context, HttpRequest request) =>
    {
      var filter = Builders<DrinkDto>.Filter.Eq(drink => drink.Id, id);
      var existingDrink = await context.Drinks.Find(filter).FirstOrDefaultAsync();

      if (existingDrink is null) {

      DrinkDto drink = new DrinkDto 
      {
        Id = id,
        Name = updatedDrink.Name,
        MainSpirit = updatedDrink.MainSpirit,
        Image = updatedDrink.Image,
        Ingredients = updatedDrink.Ingredients,
        MeasurementsOz = updatedDrink.MeasurementsOz,
        Bitters = updatedDrink.Bitters,
        Garnish = updatedDrink.Garnish,
        Color = updatedDrink.Color,
        RecommendedGlasses = updatedDrink.RecommendedGlasses,
        Notes = updatedDrink.Notes,
        Method = updatedDrink.Method,
        Credit = updatedDrink.Credit,
        Vibe = updatedDrink.Vibe
      };

      await context.Drinks.InsertOneAsync(drink) ;
      return Results.Created($"/drinks/{id}", drink);
      }

      var update = Builders<DrinkDto>.Update
        .Set(d => d.Name, updatedDrink.Name)
        .Set(d => d.MainSpirit, updatedDrink.MainSpirit)
        .Set(d => d.Image, updatedDrink.Image)
        .Set(d => d.Ingredients, updatedDrink.Ingredients)
        .Set(d => d.MeasurementsOz, updatedDrink.MeasurementsOz)
        .Set(d => d.Bitters, updatedDrink.Bitters)
        .Set(d => d.Garnish, updatedDrink.Garnish)
        .Set(d => d.Color, updatedDrink.Color)
        .Set(d => d.RecommendedGlasses, updatedDrink.RecommendedGlasses)
        .Set(d => d.Notes, updatedDrink.Notes)
        .Set(d => d.Method, updatedDrink.Method)
        .Set(d => d.Credit, updatedDrink.Credit)
        .Set(d => d.Vibe, updatedDrink.Vibe);

      await context.Drinks.UpdateOneAsync(filter, update);

      return Results.Ok(updatedDrink);
    });

    // // Delete a drink

    group.MapDelete("/{id}", [Authorize] (string id, MongoDbContext context) => 
    {
      context.Drinks.DeleteOne(drink => drink.Id == id);

      return Results.NoContent();
    });

    group.MapPut("/image/{id}", async ([FromServices] MongoDbContext context, [FromRoute] string id, HttpRequest request, [FromServices] CloudinaryService cloudinaryService) => 
    {
      Console.WriteLine("Image route correct");
      Console.WriteLine($"Received request for drinkId: {id}");


      var drink = await context.Drinks.Find(d => d.Id == id).FirstOrDefaultAsync();
      if (drink == null)
      {
        return Results.NotFound(new {message = "Drink not found"});
      }

      var file = request.Form.Files.FirstOrDefault();
      if (file == null)
      {
        return Results.BadRequest(new {message = "No Image"});
      }

      string imgUrl;
      using (var stream = file.OpenReadStream())
      {
        imgUrl = await cloudinaryService.UploadImageAsync(stream, file.FileName);
      }
      var updateDefinition = Builders<DrinkDto>.Update.Set(d => d.Image, imgUrl);
      await context.Drinks.UpdateOneAsync(d => d.Id == id, updateDefinition);

      Console.WriteLine("Image backend getting hit");

      
      drink.Image = imgUrl;
      Console.WriteLine(drink);
      return Results.Ok(drink);

    });
    
    
    
    return group;
  }
  
}
