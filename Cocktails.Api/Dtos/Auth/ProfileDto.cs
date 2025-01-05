using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

      
namespace Cocktails.Api.Dtos
{
public record class ProfileDto
  {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public int Id { get; set; }

    [Required] public required string Username { get; set; }

    [Required] public required string Email { get; set; }

    [Required] public required string PasswordHash { get; set; }

    [Required] public DateTime DateOfBirth { get; set; }
  }
}