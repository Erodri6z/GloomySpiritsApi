using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace Cocktails.Api.Dtos;

public class CreateDrinkDto
{
  [Required] public required  string Name { get; set; }
  [Required] public required string MainSpirit { get; set; }
  public string? Image { get; set; }
  [Required] public required string[] Ingredients { get; set; }
  [Required] public required decimal[] MeasurementsOz { get; set; }
  public string[]? Bitters { get; set; }
  public string[]? Garnish { get; set; }
  [Required] public required string Color { get; set; }
  [Required] public required string[] RecommendedGlasses { get; set; }
  [Required]public required string[] Notes { get; set; }
  [Required] public required string[] Method { get; set; }
  public string? Credit { get; set; }
  [Required] public required string Vibe { get; set; }
};
