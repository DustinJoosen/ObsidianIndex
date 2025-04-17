using Microsoft.EntityFrameworkCore;
using OI.API.Data;
using OI.API.DTOS;
using OI.API.Models;
using OI.API.Services.Abstractions;

namespace OI.API.Services;

public class LinkingService : ILinkingService
{
    private ApplicationDbContext _context;
    public LinkingService(ApplicationDbContext context)
    {
        this._context = context;
    }

    /// <summary>
    /// Adds a tag to the media.
    /// </summary>
    /// <param name="mediaId">Media to add the tag to.</param>
    /// <param name="tagId">Tag to add.</param>
    /// <returns>Boolean indicating success.</returns>
    public async Task<bool> AddTagToMedia(Guid mediaId, Guid tagId)
    {
        try
        {
            this._context.MediaTags.Add(new()
            {
                TagId = tagId,
                MediaId = mediaId
            });
            await this._context.SaveChangesAsync();
            return true;
        } 
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Removes a tag from the media.
    /// </summary>
    /// <param name="mediaId">Media to remove the tag from.</param>
    /// <param name="tagId">Tag to remove.</param>
    /// <returns>Boolean indicating success.</returns>
    public async Task<bool> RemoveTagFromMedia(Guid mediaId, Guid tagId)
    {
        var link = await this._context.MediaTags
            .SingleOrDefaultAsync(mt => mt.TagId == tagId && mt.MediaId == mediaId);

        if (link == null)
            return false;

        this._context.MediaTags.Remove(link);
        await this._context.SaveChangesAsync();
        return true;
    }
}
