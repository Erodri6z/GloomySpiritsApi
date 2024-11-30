// using System;
using System.Diagnostics.Metrics;
using System.Reflection;
using Cocktails.Api.Data;
using Cocktails.Api.Dtos;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;




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


    //TODO: Find Drink By Spirit
    group.MapGet("/byAlcohol/{spirit}", async (string spirit, MongoDbContext context) => 
    {
      var filter = Builders<DrinkDto>.Filter.Eq(drink => drink.MainSpirit, spirit);
      var drinks = await context.Drinks.Find(filter).ToListAsync();
      return drinks.Any() ? Results.NotFound() : Results.Ok(drinks);
    });


    //TODO: Find Drink By name


    //TODO: Find Drink By Vibe 

    // Create a drink
    // TODO: find a way to create images with cloudary
    group.MapPost("/", async ([FromServices] MongoDbContext context, CreateDrinkDto newDrink) => 
    {

      DrinkDto drink = new DrinkDto 
      {
        Name = newDrink.Name,
        MainSpirit = newDrink.MainSpirit,
        Image = newDrink.Image,
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
    })
    .WithParameterValidation();

    // // Update a drink

    group.MapPut("/{id}", async (string id, UpdateDrinkDto updatedDrink, MongoDbContext context) =>
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

      return Results.NoContent();
    });

    // // Delete a drink

    group.MapDelete("/{id}", (string id, MongoDbContext context) => 
    {
      context.Drinks.DeleteOne(drink => drink.Id == id);

      return Results.NoContent();
    });

    return group;
  }
}
