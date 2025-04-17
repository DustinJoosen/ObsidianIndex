using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OI.API.Exchange;
using OI.API.Services.Abstractions;

namespace OI.API.Controllers;
[Route("search")]
[ApiController]
public class SearchingController : ControllerBase
{
    private readonly ITagService _tagService;
    private readonly IMediaService _mediaService;
    public SearchingController(ITagService tagService, IMediaService mediaService)
    {
        this._tagService = tagService;
        this._mediaService = mediaService;
    }

    [HttpPost]
    [Route("")]
    public async Task<SearchResponse> Search([FromBody]SearchRequest searchRequest)
    {
        return new();
    }
}
