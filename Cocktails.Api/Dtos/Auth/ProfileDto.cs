using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;


namespace Cocktails.Api.Dtos
{
public record class ProfileDto
{
  [BsonId]
  [BsonRepresentation(BsonType.ObjectId)]
  public string Id { get; set; }
  [Required] public required string Username { get; set; }
  [Required] public required string Email { get; set; }
  [Required] public required string PasswordHash { get; set; }
  public DateTime DateOfBirth { get; set; }
}

public class LoginRequest
{
  public string Username { get; set; } = string.Empty;
  public string Password { get; set; } = string.Empty;
}
}
