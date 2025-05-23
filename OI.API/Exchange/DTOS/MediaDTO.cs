﻿using OI.API.Models;
using System.ComponentModel.DataAnnotations;

namespace OI.API.Exchange.DTOS;

public record MediaDTO(
    Guid MediaId,
    string FileTypeExtension,
    string? Description,
    long? FileSizeInKb,
    string? Dimensions,
    string DateAdded,
    ICollection<TagDTO> Tags);
