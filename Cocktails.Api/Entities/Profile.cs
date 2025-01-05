using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
      
namespace Cocktails.Api.Entities;

public record class Profile
{
  [BsonId]
  [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
  public int Id { get; set; }
  [BsonElement("Username")]
  public required string Username { get; set; }
  [BsonElement("Email")]
  public required string Email { get; set; }
  [BsonElement("PasswordHash")]
  public required string FullName { get; set; }
  [BsonElement("DateOfBirth")]
  public DateTime DateOfBirth { get; set; }
}
