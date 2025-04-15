using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OI.API.Data;
using OI.API.Models;
using OI.API.Options;
using OI.API.Services.Abstractions;
using System.Drawing;
using Microsoft.AspNetCore.Http;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

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

        var filePath = Path.Combine(this._config.RootFolder, $"{media.MediaId}.{media.FileTypeExtension}");

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

    private string GetDimensions(IFormFile file)
    {
        using var stream = file.OpenReadStream();
        using var image = System.Drawing.Image.FromStream(stream);
        
        return $"{image.Width}x{image.Height}";
    }

    private string GetMediaContentType(string fileExtension)
    {
        return fileExtension.ToLower() switch
        {
            "jpg" => "image/jpeg",
            "jpeg" => "image/jpeg",
            "png" => "image/png",
            "gif" => "image/gif",
            _ => "application/octet-stream"
        };
    }

}
