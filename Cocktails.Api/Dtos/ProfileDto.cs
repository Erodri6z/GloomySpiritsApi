using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Cocktails.Api.Dtos
{
  public class ProfileDto
  {
  [BsonId]
  [BsonRepresentation(BsonType.ObjectId)]
  public string Id { get; set; }
  [Required] public string Username { get; set; }
  [Required] public string Email { get; set; } = string.Empty;
  [Required]  DateTime CreatedAt { get; set; } = DateTime.UtcNow;
  [Required] public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}