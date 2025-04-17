using OI.API.Data;
using OI.API.DTOS;
using OI.API.Models;
using OI.API.Services.Abstractions;

namespace OI.API.Services;

public class TagService : ITagService
{
    private ApplicationDbContext _context;
    public TagService(ApplicationDbContext context)
    {
        this._context = context;
    }

    /// <summary>
    /// Creates a tag.
    /// </summary>
    /// <param name="createTagRequest">Data for the tag to be created.</param>
    /// <returns>Created data.</returns>
    public async Task<CreateTagResponse> CreateTag(CreateTagRequest createTagRequest)
    {
        var tag = new Tag
        {
            TagId = Guid.NewGuid(),
            Name = createTagRequest.Name,
            Description = createTagRequest.Description,
            Primary = false,
            DateAdded = DateTime.UtcNow,
        };

        this._context.Tags.Add(tag);
        await this._context.SaveChangesAsync();

        return new CreateTagResponse(tag.TagId, tag.Name, tag.Description, tag.DateAdded);
    }

    /// <summary>
    /// Checks wether a tag name exists.
    /// </summary>
    /// <param name="tagName">Name to check for</param>
    /// <returns>True is the Name is unused. Otherwise false.</returns>
    public bool IsTagNameUnique(string name) =>
        !this._context.Tags.Any(tag => tag.Name == name);

    /// <summary>
    /// Checks wether a tag Id exists.
    /// </summary>
    /// <param name="tagId">Id to check for</param>
    /// <returns>True is the Id is unused. Otherwise false.</returns>
    public bool IsTagIdUnique(Guid tagId) =>
        !this._context.Tags.Any(tag => tag.TagId == tagId);
}
