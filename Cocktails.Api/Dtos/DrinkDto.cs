namespace Cocktails.Api.Dtos;

public record class DrinkDto
(
  int Id, 
  string Name,
  string MainSpirit,
  string[] Ingredients,
  decimal[] MeasurementsOz,
  string[]? Garnish,
  string Color,
  string? Image,
  string[] RecommendedGlasses,
  string[] Notes,
  string[] Method,
  string? Credit,
  string Vibe
)
