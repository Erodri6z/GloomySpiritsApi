using System;

namespace Cocktails.Api.Dtos;

public record class Profile
{
  public string Id { get; set; }
  public string Username { get; set; }
  public string Email { get; set; }
  public bool Admin {get; set; }
  
  
}
