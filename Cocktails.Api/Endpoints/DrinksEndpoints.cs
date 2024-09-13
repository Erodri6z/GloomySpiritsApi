using System;
using Cocktails.Api.Dtos;

namespace Cocktails.Api.Endpoints;

public static class DrinksEndpoints
{
  const string GetDrinkEndPointName = "GetDrink";

  private static readonly List<DrinkDto> drinks = [
  new (
    1,
    "Old Fashioned",
    "Whiskey",
    "",
    ["Whiskey", "Simple Syrup"],
    [2, 0.75M],
    ["3 dashes of Angastora Bitters"],
    ["Orange Peel"],
    "Brown",
    ["Rocks Glass"],
    [""],
    ["Stirred", "Build-In-Glass"],
    "",
    "Fancy"
  ),
  new (
    2,
    "Revolver",
    "Whiskey",
    "",
    ["Whiskey", "Coffee Liquer"],
    [2, 0.5M],
    ["2 dashes of Orange Bitters"],
    ["Orange Peel"],
    "Brown",
    ["Coupe Glass", "Martini Glass"],
    [""],
    ["Stirred"],
    "",
    "Fancy"
  ),
  new (
    3,
    "Moscow Mule",
    "Vodka",
    "",
    ["Vodka", "Lime Juice", "Ginger Beer"],
    [2, 0.75M, 8],
    [""],
    ["Lime Wedge"],
    "White",
    ["Copper Mug", "High Ball Glass"],
    [""],
    ["Shaken", "Stirred"],
    "",
    "Chill"
  )];

  public static WebApplication MapDrinksEndpoints(this WebApplication app)
  {
    
    // Get all Drinks

    app.MapGet("drinks", () => drinks);

    // Get specific drinks
    app.MapGet("drinks/{id}", (int id) => 
    {
      DrinkDto? drink = drinks.Find(drink => drink.Id == id);

      return drink is null ? Results.NotFound() : Results.Ok(drink);
    })
    .WithName(GetDrinkEndPointName);

    // Get all Drinks based on main spirit

    app.MapGet("drinks/ByAlcohol/{MainSpirit}", (string MainSpirit) => drinks.FindAll(drink => drink.MainSpirit == MainSpirit));

    // Post a new drink

    app.MapPost("drinks", (CreateDrinkDto newDrink) => {
      DrinkDto drink = new(
        drinks.Count + 1,
        newDrink.Name,
        newDrink.MainSpirit,
        newDrink.Image,
        newDrink.Ingredients,
        newDrink.MeasurementsOz,
        newDrink.Bitters,
        newDrink.Garnish,
        newDrink.Color,
        newDrink.RecommendedGlasses,
        newDrink.Notes,
        newDrink.Method,
        newDrink.Credit,
        newDrink.Vibe
      );
      drinks.Add(drink);

      return Results.CreatedAtRoute(GetDrinkEndPointName, new { id = drink.Id }, drink);
    });

    // Update a drink

    app.MapPut("drinks/{id}", (int id, UpdateDrinkDto updatedDrink) =>
    {
      var index = drinks.FindIndex(drink => drink.Id == id);

      if ( index == -1)
      {
        // TODO : if not able to find, create it with payload
        return Results.NotFound();
      }

      drinks[index] = new DrinkDto(
        id,
        updatedDrink.Name,
        updatedDrink.MainSpirit,
        updatedDrink.Image,
        updatedDrink.Ingredients,
        updatedDrink.MeasurementsOz,
        updatedDrink.Bitters,
        updatedDrink.Garnish,
        updatedDrink.Color,
        updatedDrink.RecommendedGlasses,
        updatedDrink.Notes,
        updatedDrink.Method,
        updatedDrink.Credit,
        updatedDrink.Vibe
      );

      return Results.NoContent();
    });

    // Delete a drink

    app.MapDelete("drinks/{id}", (int id) => 
    {
      drinks.RemoveAll(drink => drink.Id == id);

      return Results.NoContent();
    });

    return app;
  }
}
