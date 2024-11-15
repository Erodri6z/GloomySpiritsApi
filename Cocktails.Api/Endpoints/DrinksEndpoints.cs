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

  // private static readonly List<DrinkDto> drinks = [
  // new (
  //   1,
  //   "Old Fashioned",
  //   "Whiskey",
  //   "",
  //   ["Whiskey", "Simple Syrup"],
  //   [2, 0.75M],
  //   ["3 dashes of Angastora Bitters"],
  //   ["Orange Peel"],
  //   "Brown",
  //   ["Rocks Glass"],
  //   [""],
  //   ["Stirred", "Build-In-Glass"],
  //   "",
  //   "Fancy"
  // ),
  // new (
  //   2,
  //   "Revolver",
  //   "Whiskey",
  //   "",
  //   ["Whiskey", "Coffee Liquer"],
  //   [2, 0.5M],
  //   ["2 dashes of Orange Bitters"],
  //   ["Orange Peel"],
  //   "Brown",
  //   ["Coupe Glass", "Martini Glass"],
  //   [""],
  //   ["Stirred"],
  //   "",
  //   "Fancy"
  // ),
  // new (
  //   3,
  //   "Moscow Mule",
  //   "Vodka",
  //   "",
  //   ["Vodka", "Lime Juice", "Ginger Beer"],
  //   [2, 0.75M, 8],
  //   [""],
  //   ["Lime Wedge"],
  //   "White",
  //   ["Copper Mug", "High Ball Glass"],
  //   [""],
  //   ["Shaken", "Stirred"],
  //   "",
  //   "Chill"
  // )];

  public static RouteGroupBuilder MapDrinksEndpoints(this WebApplication app)
  {

    var group = app.MapGroup("drinks");
    
    // Get all Drinks

    group.MapGet("/", async ([FromServices] MongoDbContext context) => 
    {
      Console.WriteLine("this works");
      return await context.Drinks.Find(_ => true).ToListAsync();
    });

    // commented out until I figure out what I am doing wrong

    // Get specific drinks
    // group.MapGet("/{id}", async (int id, MongoDbContext context) => 
    // {
    //   // DrinkDto? drink = context.Drinks.Find(drink => drink.Id == id);\
    //   var filter = Builders<DrinkDto>.Filter.Eq(drinks => drinks.Id, id);
    //   var drink = await context.Drinks.Find(filter).FirstOrDefaultAsync();

    //   return drink is null ? Results.NotFound: Results.Ok(drink);

    //   // return drink is null ? Results.NotFound() : Results.Ok(drink);

    // })
    // .WithName(GetDrinkEndPointName);


    group.MapPost("/", async ([FromServices] MongoDbContext context, CreateDrinkDto newDrink) => 
    {
      // DrinkDto drink = new(
      //   drinks.Count + 1,
      //   newDrink.Name,
      //   newDrink.MainSpirit,
      //   newDrink.Image,
      //   newDrink.Ingredients,
      //   newDrink.MeasurementsOz,
      //   newDrink.Bitters,
      //   newDrink.Garnish,
      //   newDrink.Color,
      //   newDrink.RecommendedGlasses,
      //   newDrink.Notes,
      //   newDrink.Method,
      //   newDrink.Credit,
      //   newDrink.Vibe
      // );

      DrinkDto drink = new DrinkDto 
      {
        newId,
        Name = newDrink.Name,
        MainSpirit = newDrink.MainSpirit,
        Image = newDrink.Image,
        MeasurementsOz = newDrink.MeasurementsOz.ToArray(),
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

      return Results.CreatedAtRoute(GetDrinkEndPointName, new { id = drink.Id }, drink);
    })
    .WithParameterValidation();

    // // Update a drink

    // group.MapPut("/{id}", (int id, UpdateDrinkDto updatedDrink) =>
    // {
    //   var index = drinks.FindIndex(drink => drink.Id == id);

    //   if ( index == -1)
    //   {
    //     // TODO : if not able to find, create it with payload
    //     return Results.NotFound();
    //   }

    //   drinks[index] = new DrinkDto(
    //     id,
    //     updatedDrink.Name,
    //     updatedDrink.MainSpirit,
    //     updatedDrink.Image,
    //     updatedDrink.Ingredients,
    //     updatedDrink.MeasurementsOz,
    //     updatedDrink.Bitters,
    //     updatedDrink.Garnish,
    //     updatedDrink.Color,
    //     updatedDrink.RecommendedGlasses,
    //     updatedDrink.Notes,
    //     updatedDrink.Method,
    //     updatedDrink.Credit,
    //     updatedDrink.Vibe
    //   );

    //   return Results.NoContent();
    // });

    // // Delete a drink

    // group.MapDelete("/{id}", (int id) => 
    // {
    //   drinks.RemoveAll(drink => drink.Id == id);

    //   return Results.NoContent();
    // });

    return group;
  }
}
