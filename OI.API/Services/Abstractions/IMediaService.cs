
namespace OI.API.Services.Abstractions;

public interface IMediaService
{
    /// <summary>
    /// Returns a stream of the found media
    /// </summary>
    /// <param name="mediaId">Media to search the stream of</param>
    /// <returns>Stream of the found media. if no media is found null is returned</returns>
    Task<StreamContent?> GetMediaStream(Guid mediaId);

    /// <summary>
    /// Imports and creates a media
    /// </summary>
    /// <param name="file">Formfile to upload and import</param>
    /// <returns>The created media ID. TODO: Have this return a DTO</returns>
    Task<Guid> ImportMedia(IFormFile file);
}
