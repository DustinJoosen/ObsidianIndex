using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OI.API.Models;

public class MediaTag
{
    [Key]
    public Guid MediaId { get; set; }
    
    [ForeignKey(nameof(MediaId))]
    public Media Media { get; set; }

    [Key]
    public Guid TagId { get; set; }

    [ForeignKey(nameof(TagId))]
    public Tag Tag { get; set; }
}
