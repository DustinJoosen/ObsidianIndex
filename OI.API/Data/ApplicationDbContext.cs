using Microsoft.EntityFrameworkCore;
using OI.API.Models;
using System.Reflection.Emit;

namespace OI.API.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> context) : base(context)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<MediaTag>()
            .HasKey(mt => new
            {
                mt.MediaId,
                mt.TagId
            });

        modelBuilder.Entity<MediaTag>()
            .HasOne(mt => mt.Media)
            .WithMany(m => m.MediaTags)
            .HasForeignKey(mt => mt.MediaId);

        modelBuilder.Entity<MediaTag>()
            .HasOne(mt => mt.Tag)
            .WithMany(t => t.MediaTags)
            .HasForeignKey(mt => mt.TagId);
    }

    public DbSet<Media> Media { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<MediaTag> MediaTags { get; set; }
}

