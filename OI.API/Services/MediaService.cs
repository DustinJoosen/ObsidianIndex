using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OI.API.Data;
using OI.API.Exchange;
using OI.API.Exchange.DTOS;
using OI.API.Models;
using OI.API.Options;
using OI.API.Services.Abstractions;

namespace OI.API.Services;

public record StreamContent(FileStream stream, string contentType);

public class MediaService : IMediaService
{
    private ApplicationDbContext _context;
    private MediaConfig _config;
    public MediaService(ApplicationDbContext context, IOptions<MediaConfig> config)
    {
        this._context = context;
        this._config = config.Value;
    }

    /// <summary>
    /// Gets all media.
    /// </summary>
    /// <param name="limit">Limits to how many media are actually taken. @TODO: Make this use a pagination system.</param>
    /// <returns>List of all media in DTO format.</returns>
    public async Task<GetMediaResponse> GetAll(int limit = 10)
    {
        var mediaList = await this._context.Media
            .Take(limit)
            .Include(media => media.MediaTags)
                .ThenInclude(mt => mt.Tag)
            .ToListAsync();

        return new GetMediaResponse(mediaList.Select(this.MapMediaToMediaDTO)
            .ToList());
    }

    /// <summary>
    /// Gets a specific media based on an Id.
    /// </summary>
    /// <param name="mediaId">The Id to search for.</param>
    /// <returns>Media object found</returns>
    public async Task<MediaDTO?> GetById(Guid mediaId)
    {
        var media = await this._context.Media
            .Include(media => media.MediaTags)
                .ThenInclude(mt => mt.Tag)
            .SingleOrDefaultAsync(media => media.MediaId.Equals(mediaId));

        if (media == null)
            return null;

        return this.MapMediaToMediaDTO(media);
    }


    /// <summary>
    /// Returns a stream of the found media
    /// </summary>
    /// <param name="mediaId">Media to search the stream of</param>
    /// <returns>Stream of the found media. if no media is found null is returned</returns>
    public virtual async Task<StreamContent?> GetMediaStream(Guid mediaId)
    {
        var media = await this._context.Media
            .SingleOrDefaultAsync(media => media.MediaId == mediaId);

        if (media == null)
            return null;

        var filePath = Path.Combine(this._config.RootFolder, $"{media.MediaId}{media.FileTypeExtension}");

        if (!File.Exists(filePath))
            return null;

        try
        {
            // Return the relevant information of the media stream.
            return new StreamContent(
                stream: new FileStream(filePath, FileMode.Open, FileAccess.Read),
                contentType: this.GetMediaContentType(media.FileTypeExtension));
        } 
        catch
        {
            return null;
        }
    }


    /// <summary>
    /// Imports and creates a media
    /// </summary>
    /// <param name="file">Formfile to upload and import</param>
    /// <returns>The created media ID. TODO: Have this return a DTO</returns>
    public async Task<Guid> ImportMedia(IFormFile file)
    {
        var mediaId = Guid.NewGuid();

        var fileExtension = Path.GetExtension(file.FileName);
        var filePath = Path.Combine(this._config.RootFolder, $"{mediaId}{fileExtension}");

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        this._context.Media.Add(new Media
        {
            MediaId = mediaId,
            FileTypeExtension = fileExtension,
            Dimensions = this.GetDimensions(file),
            FileSizeInKb = file.Length / 1024,
            DateAdded = DateTime.UtcNow,
        });
        await this._context.SaveChangesAsync();

        return mediaId;
    }

    /// <summary>
    /// Checks wether a media ID exists.
    /// </summary>
    /// <param name="mediaId">Id to check for</param>
    /// <returns>True is the ID is unused. Otherwise false.</returns>
    public bool IsMediaIdUnique(Guid mediaId) =>
        !this._context.Media.Any(media => media.MediaId== mediaId);


    private MediaDTO MapMediaToMediaDTO(Media media)
    {
        return new MediaDTO(
            media.MediaId,
            media.FileTypeExtension,
            media.Description,
            media.FileSizeInKb,
            media.Dimensions,
            media.DateAdded.ToString("dd/MM/yyyy"),
            media.MediaTags.Select(mt => this.MapTagToTagDTO(mt.Tag)).ToList()
        );
    }
    private TagDTO MapTagToTagDTO(Tag tag)
    {
        return new TagDTO(
            tag.TagId,
            tag.Name,
            tag.Description,
            tag.Primary,
            tag.DateAdded);
    }

    private string? GetDimensions(IFormFile file)
    {
        try
        {
            using var stream = file.OpenReadStream();
            using var image = System.Drawing.Image.FromStream(stream);
        
            return $"{image.Width}x{image.Height}";
        }
        catch
        {
            return null;
        }
    }

    private string GetMediaContentType(string fileExtension)
    {
        return fileExtension.ToLower() switch
        {
            ".jpg" => "image/jpeg",
            ".jpeg" => "image/jpeg",
            ".png" => "image/png",
            ".gif" => "image/gif",
            ".mp4" => "video/mp4",
            ".pdf" => "application/pdf",
            _ => "application/octet-stream"
        };
    }

}
