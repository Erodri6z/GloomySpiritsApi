using System.ComponentModel.DataAnnotations;
using System.IO.Pipelines;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Cocktails.Api.Dtos;

public record class DrinkDto
{
    [BsonId] // This attribute marks the field as the primary key
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; } // MongoDB generates this automatically if null

    [Required] public string Name { get; set; } = string.Empty;
    [Required] public string MainSpirit { get; set; } = string.Empty;
    public string? Image { get; set; }
    [Required, NotEmptyArray] public string[] Ingredients { get; set; }
    [Required] public decimal[] MeasurementsOz { get; set; }
    public string[]? Bitters { get; set; }
    public string[]? Garnish { get; set; }
    [Required] public string Color { get; set; } = string.Empty;
    [Required] public string[] RecommendedGlasses { get; set; }
    public string[] Notes { get; set; }
    [Required] public string[] Method { get; set; }  
    public string? Credit { get; set; }
    [Required] public string Vibe { get; set; } = string.Empty
}