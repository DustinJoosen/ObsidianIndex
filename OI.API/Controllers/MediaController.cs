using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OI.API.Services.Abstractions;

namespace OI.API.Controllers;
[Route("media")]
[ApiController]
public class MediaController : ControllerBase
{

    private IMediaService _mediaService;
    public MediaController(IMediaService mediaService)
    {
        this._mediaService = mediaService;
    }

    [HttpGet]
    [Route("media/{mediaId:Guid}")]
    public async Task<IActionResult> GetMedia([FromRoute] Guid mediaId)
    {
        var streamContent = await this._mediaService.GetMediaStream(mediaId);
        if (streamContent == null)
            return NotFound($"Could not find media with id [{mediaId}]");

        return this.File(
            fileStream: streamContent.stream,
            contentType: streamContent.contentType);
    }

    [HttpPost]
    [Route("import")]
    public async Task<IActionResult> ImportMedia(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("No file attached");

        var newMediaId = await this._mediaService.ImportMedia(file);
        if (newMediaId == Guid.Empty)
            return BadRequest("Could not import media");

        return Ok(newMediaId);
    }
}
