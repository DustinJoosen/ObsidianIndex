using Microsoft.AspNetCore.Mvc;
using OI.API.DTOS;
using OI.API.Services.Abstractions;

namespace OI.API.Controllers;
[Route("tags")]
[ApiController]
public class TagController : ControllerBase
{
    private readonly ITagService _tagService;
    public TagController(ITagService tagService)
    {
        this._tagService = tagService;
    }

    [HttpPost]
    [Route("create")]
    public async Task<ActionResult<CreateTagResponse>> CreateTag(CreateTagRequest createTagRequest)
    {
        if (!this._tagService.IsTagNameUnique(createTagRequest.Name))
            return BadRequest("Tag name already in use");

        var createdTag = await this._tagService.CreateTag(createTagRequest);
        return Ok(createdTag);
    }
}
