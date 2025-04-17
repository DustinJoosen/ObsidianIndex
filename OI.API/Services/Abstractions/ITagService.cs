using OI.API.DTOS;

namespace OI.API.Services.Abstractions;

public interface ITagService
{

    /// <summary>
    /// Creates a tag.
    /// </summary>
    /// <param name="createTagRequest">Data for the tag to be created.</param>
    /// <returns>Created data.</returns>
    Task<CreateTagResponse> CreateTag(CreateTagRequest createTagRequest);

    /// <summary>
    /// Checks wether a tag name exists.
    /// </summary>
    /// <param name="tagName">Name to check for</param>
    /// <returns>True is the Name is unused. Otherwise false.</returns>
    bool IsTagNameUnique(string name);

    /// <summary>
    /// Checks wether a tag Id exists.
    /// </summary>
    /// <param name="tagId">Id to check for</param>
    /// <returns>True is the Id is unused. Otherwise false.</returns>
    bool IsTagIdUnique(Guid tagId);


}
