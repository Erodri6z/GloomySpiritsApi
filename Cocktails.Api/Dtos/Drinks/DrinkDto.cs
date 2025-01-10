using System.ComponentModel.DataAnnotations;
using System.IO.Pipelines;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Cocktails.Api.Attributes;

namespace Cocktails.Api.Dtos;

public record class DrinkDto
{
    [BsonId] 
    [BsonRepresentation(BsonType.ObjectId)]
    public required string Id { get; set; } 
    

    [Required] public string Name { get; set; } = string.Empty;
    [Required] public string MainSpirit { get; set; } = string.Empty;
    public string? Image { get; set; }
    [Required, NonEmptyArray] public required string[] Ingredients { get; set; }
    [Required, NonEmptyArray] public required decimal[] MeasurementsOz { get; set; }
    public string[]? Bitters { get; set; }
    public string[]? Garnish { get; set; }
    [Required] public string Color { get; set; } = string.Empty;
    [Required, NonEmptyArray] public required string[] RecommendedGlasses { get; set; }
    public required string[] Notes { get; set; }
    [Required, NonEmptyArray] public required string[] Method { get; set; }  
    public string? Credit { get; set; }
    [Required] public string Vibe { get; set; } = string.Empty;
}