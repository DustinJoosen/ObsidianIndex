using System.ComponentModel.DataAnnotations;

namespace OI.API.Models;

public class Media
{

    public Media()
    {
        this.MediaId = Guid.NewGuid();
        this.DateAdded = DateTime.UtcNow;
    }


    [Key]
    public Guid MediaId { get; set; }

    [Required]
    public string FileTypeExtension { get; set; }

    public string? Description { get; set; }

    public long? FileSizeInKb { get; set; }
    public string? Dimensions { get; set; }

    [Required]
    public DateTime DateAdded { get; set; }

    public virtual ICollection<MediaTag> MediaTags { get; set; } = new List<MediaTag>();
}
