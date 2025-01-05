using Cocktails.Api.Data;
using Cocktails.Api.Dtos;


namespace Cocktails.Api.Endpoints; 

public static class ProfileEndpoints
{
  public static RouteGroupBuilder MapAuthEndpoints(this webApplications app)
  {
    var group = MapGroup("auth"); 

    public async Task CreateProfileAsync(ProfileDto profile)
    {
      await _profilesCollection.InsertOneAsync(profile)
    }
    
  }

}