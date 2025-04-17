using OI.API.DTOS;

namespace OI.API.Services.Abstractions;

public interface ILinkingService
{
    /// <summary>
    /// Adds a tag to the media.
    /// </summary>
    /// <param name="mediaId">Media to add the tag to.</param>
    /// <param name="tagId">Tag to add.</param>
    /// <returns>Boolean indicating success.</returns>
    Task<bool> AddTagToMedia(Guid mediaId, Guid tagId);

    /// <summary>
    /// Removes a tag from the media.
    /// </summary>
    /// <param name="mediaId">Media to remove the tag from.</param>
    /// <param name="tagId">Tag to remove.</param>
    /// <returns>Boolean indicating success.</returns>
    Task<bool> RemoveTagFromMedia(Guid mediaId, Guid tagId);
}
