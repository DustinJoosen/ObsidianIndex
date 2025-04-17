using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OI.API.Services.Abstractions;

namespace OI.API.Controllers;
[Route("media")]
[ApiController]
public class LinkingController : ControllerBase
{
    private readonly IMediaService _mediaService;
    private readonly ITagService _tagService;
    private readonly ILinkingService _linkingService;
    public LinkingController(IMediaService mediaService, ITagService tagService, 
        ILinkingService linkingService)
    {
        this._mediaService = mediaService;
        this._tagService = tagService;
        this._linkingService = linkingService;
    }

    [HttpPut]
    [Route("{mediaId:Guid}/add/{tagId:Guid}")]
    public async Task<IActionResult> AddTag([FromRoute] Guid mediaId, [FromRoute] Guid tagId)
    {
        if (this._mediaService.IsMediaIdUnique(mediaId))
            return NotFound("Could not find mediaId");

        if (this._tagService.IsTagIdUnique(tagId))
            return NotFound("Could not find tagId");

        return await this._linkingService.AddTagToMedia(mediaId, tagId)
            ? Ok("Tag added to media")
            : BadRequest("Could not add tag");
    }

    [HttpDelete]
    [Route("{mediaId:Guid}/remove/{tagId:Guid}")]
    public async Task<IActionResult> RemoveTag([FromRoute] Guid mediaId, [FromRoute] Guid tagId)
    {
        if (this._mediaService.IsMediaIdUnique(mediaId))
            return NotFound("Could not find mediaId");

        if (this._tagService.IsTagIdUnique(tagId))
            return NotFound("Could not find tagId");

        return await this._linkingService.RemoveTagFromMedia(mediaId, tagId)
            ? Ok("Tag removed from media")
            : BadRequest("Could not remove tag");
    }

}
