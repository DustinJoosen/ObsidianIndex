using System.ComponentModel.DataAnnotations;

namespace OI.API.Models;

public class Tag
{
    public Tag()
    {
        this.TagId = Guid.NewGuid();
        this.DateAdded = DateTime.UtcNow;
    }

    [Key]
    public Guid TagId { get; set; }

    [Required]
    public string Name { get; set; }

    public string? Description { get; set; }

    public bool Primary { get; set; } = false;

    [Required]
    public DateTime DateAdded { get; set; }

    public virtual ICollection<MediaTag> MediaTags { get; set; } = new List<MediaTag>();

}
