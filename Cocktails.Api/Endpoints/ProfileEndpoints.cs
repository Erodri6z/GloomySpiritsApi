using Cocktails.Api.Data;
using Cocktails.Api.Dtos;


namespace Cocktails.Api.Endpoints; 

public static class ProfileEndpoints
{
  public static RouteGroupBuilder MapProfilesEndpoints(this WebApplication app)
  {
    var group = app.MapGroup("profiles");

    return group;
  }

}