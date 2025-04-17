using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OI.API.Exchange;
using OI.API.Exchange.DTOS;
using OI.API.Services.Abstractions;

namespace OI.API.Controllers;
[Route("media")]
[ApiController]
public class MediaController : ControllerBase
{

    private readonly IMediaService _mediaService;
    public MediaController(IMediaService mediaService)
    {
        this._mediaService = mediaService;
    }

    [HttpGet]
    [Route("")]
    public async Task<GetMediaResponse> GetAll() =>
        await this._mediaService.GetAll();


    [HttpGet]
    [Route("{mediaId:Guid}")]
    public async Task<ActionResult<MediaDTO>> GetById([FromRoute] Guid mediaId)
    {
        var media = await this._mediaService.GetById(mediaId);
        return media != null
            ? Ok(media)
            : NotFound("Could not find media");
    }


    [HttpGet]
    [Route("{mediaId:Guid}/stream")]
    public async Task<IActionResult> GetMediaStream([FromRoute] Guid mediaId)
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
