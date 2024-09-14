using System.ComponentModel.DataAnnotations;

namespace Cocktails.Api.Dtos;

public record class CreateDrinkDto
(
  [Required] string Name,
  [Required] string MainSpirit,
  string? Image,
  [Required] string[] Ingredients,
  [Required] decimal[] MeasurementsOz,
  string[]? Bitters,
  string[]? Garnish,
  [Required] string Color,
  [Required] string[] RecommendedGlasses,
  string[] Notes,
  [Required] string[] Method,
  string? Credit,
  [Required] string Vibe
);
