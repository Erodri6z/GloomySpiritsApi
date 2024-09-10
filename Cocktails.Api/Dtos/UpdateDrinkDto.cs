namespace Cocktails.Api.Dtos;

public record class UpdateDrinkDto
(
  string Name,
  string MainSpirit,
  string? Image,
  string[] Ingredients,
  decimal[] MeasurementsOz,
  string[]? Bitters,
  string[]? Garnish,
  string Color,
  string[] RecommendedGlasses,
  string[] Notes,
  string[] Method,
  string? Credit,
  string Vibe
);
