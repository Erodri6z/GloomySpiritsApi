using CloudinaryDotNet;
using CloudinaryDotNet.Actions;


namespace Cocktails.Api.Services;

  public class CloudinaryService
  {
    private readonly Cloudinary _cloudinary;

    public CloudinaryService(string cloudName, string apiKey, string apiSecret)
    {
      var account = new Account(cloudName, apiKey, apiSecret);
      _cloudinary = new Cloudinary(account);
    }

    public async Task<string> UploadImageAsync(Stream fileStream, string fileName)
    {
      var uploadParams = new ImageUploadParams
      {
        File = new FileDescription(fileName, fileStream),
        PublicId = $"cocktails/{Path.GetFileNameWithoutExtension(fileName)}",
        Overwrite = true,
        // ResourceType = ResourceType.Image
      };

      var uploadResult = await _cloudinary.UploadAsync(uploadParams);

      if (uploadResult.StatusCode != System.Net.HttpStatusCode.OK)
      {
        throw new Exception($"Image upload failed: {uploadResult.Error?.Message}");
      }

      return uploadResult.SecureUrl.ToString();
    }
  }
