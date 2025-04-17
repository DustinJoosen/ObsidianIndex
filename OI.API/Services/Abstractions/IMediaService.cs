
using Microsoft.AspNetCore.Mvc;
using OI.API.Exchange;
using OI.API.Exchange.DTOS;

namespace OI.API.Services.Abstractions;

public interface IMediaService
{

    /// <summary>
    /// Gets all media.
    /// </summary>
    /// <param name="limit">Limits to how many media are actually taken. @TODO: Make this use a pagination system.</param>
    /// <returns>List of all media in DTO format.</returns>
    Task<GetMediaResponse> GetAll(int limit = 10);

    /// <summary>
    /// Gets a specific media based on an Id.
    /// </summary>
    /// <param name="mediaId">The Id to search for.</param>
    /// <returns>Media object found</returns>
    Task<MediaDTO?> GetById(Guid mediaId);

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

    /// <summary>
    /// Checks wether a media Id exists.
    /// </summary>
    /// <param name="mediaId">Id to check for</param>
    /// <returns>True is the Id is unused. Otherwise false.</returns>
    public bool IsMediaIdUnique(Guid mediaId);
}
