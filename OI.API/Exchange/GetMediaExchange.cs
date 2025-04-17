using OI.API.Exchange.DTOS;
using OI.API.Models;
using System.ComponentModel.DataAnnotations;

namespace OI.API.Exchange;

public record GetMediaResponse(List<MediaDTO> Media);

