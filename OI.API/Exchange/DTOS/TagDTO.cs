using OI.API.Models;
using System.ComponentModel.DataAnnotations;

namespace OI.API.Exchange.DTOS;

public record TagDTO(
    Guid TagId,
    string Name,
    string? Description,
    bool Primary,
    DateTime DateAdded);
