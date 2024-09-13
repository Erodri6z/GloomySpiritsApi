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
}
