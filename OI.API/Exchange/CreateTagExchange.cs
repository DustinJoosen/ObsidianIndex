
namespace OI.API.DTOS;

public record CreateTagRequest(
    string Name,
    string? Description);

public record CreateTagResponse(
    Guid TagId,
    string Name,
    string? Description,
    DateTime DateAdded);

