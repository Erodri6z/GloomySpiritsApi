using System.ComponentModel.DataAnnotations;

namespace Cocktails.Api.Dtos;

public class CreateProfileDto
{
  [Required] public required string Username { get; set; }
  [Required] public required string Email { get; set; }
  [Required] public required string PasswordHash { get; set; }
  public DateTime DateOfBirth { get; set; }
};