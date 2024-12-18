using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
      
namespace Cocktails.Api.Dtos
{
public record class ProfileDto
  {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("Username")]
    public string Username { get; set; }

    [BsonElement("Email")]
    public string Email { get; set; }

    [BsonElement("PasswordHash")]
    public string PasswordHash { get; set; }

    [BsonElement("FullName")]
    public string FullName { get; set; }

    [BsonElement("DateOfBirth")]
    public DateTime DateOfBirth { get; set; }
  }
}