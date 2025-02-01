namespace Cocktails.Api.Dtos;

public record class UpdateDrinkDto
{
  
  public required string Name { get; set; }
  public required string MainSpirit { get; set; }
  public string? Image { get; set; } 
  public required string[] Ingredients { get; set; } 

  public required decimal[] MeasurementsOz { get; set; } 

  public string[]? Bitters { get; set; } 

  public string[]? Garnish { get; set; } 

  public required string Color { get; set; } 
  public required string[] RecommendedGlasses { get; set; } 
  public required string[] Notes { get; set; } 
  public required string[] Method { get; set; } 

  public required string? Credit { get; set; } 
  public required string Vibe { get; set; } 

};
