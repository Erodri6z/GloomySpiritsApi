using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Cocktails.Api.Entities;


public class Drink
{
  [BsonId]
  [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
  public int Id { get; set;}
  [BsonElement("Name")]
  public required string Name { get; set;}
  [BsonElement("Main_Spirit")]
  public required string MainSpirit { get; set;}
  [BsonElement("Ingredients")]
  public required string[] Ingredients { get; set;}
  [BsonElement("Mesurements_Oz")]
  public required decimal[] MeasurementsOz { get; set;}
  [BsonElement("Bittes")]
  public string[]? Bitters { get; set;}
  [BsonElement("Garnish")]
  public string[]? Garnish { get; set;}
  [BsonElement("Color")]
  public required string Color { get; set;}
  [BsonElement("Recommended_Glass")]
  public required string[] RecommendedGlasses { get; set;}
  [BsonElement("Notes")]
  public string[]? Notes { get; set;}
  [BsonElement("Method")]
  public required string[] Method { get; set;}
  [BsonElement("Credit")]
  public string? Credit { get; set;}
  [BsonElement("Vibe")]
  public required string Vibe { get; set;}

}
