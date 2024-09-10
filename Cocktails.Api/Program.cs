using Cocktails.Api.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


List<DrinkDto> drinks = [
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
  )
];

// Get all Drinks

app.MapGet("drinks", () => drinks);

// Get specific drinks
app.MapGet("drinks/{id}", (int id) => drinks.Find(drink => drink.Id == id));

// Get all Drinks based on main spirit

app.MapGet("drinks/ByAlcohol/{MainSpirit}", (string MainSpirit) => drinks.FindAll(drink => drink.MainSpirit == MainSpirit));

// 

app.MapGet("/", () => "Hello World!");

app.Run();
