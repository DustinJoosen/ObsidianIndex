﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OI.API.Data;

#nullable disable

namespace OI.API.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.4");

            modelBuilder.Entity("OI.API.Models.Media", b =>
                {
                    b.Property<Guid>("MediaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Dimensions")
                        .HasColumnType("TEXT");

                    b.Property<long?>("FileSizeInKb")
                        .HasColumnType("INTEGER");

                    b.Property<string>("FileTypeExtension")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("MediaId");

                    b.ToTable("Media");
                });

            modelBuilder.Entity("OI.API.Models.MediaTag", b =>
                {
                    b.Property<Guid>("MediaId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("TagId")
                        .HasColumnType("TEXT");

                    b.HasKey("MediaId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("MediaTags");
                });

            modelBuilder.Entity("OI.API.Models.Tag", b =>
                {
                    b.Property<Guid>("TagId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("Primary")
                        .HasColumnType("INTEGER");

                    b.HasKey("TagId");

                    b.ToTable("Tag");
                });

            modelBuilder.Entity("OI.API.Models.MediaTag", b =>
                {
                    b.HasOne("OI.API.Models.Media", "Media")
                        .WithMany("MediaTags")
                        .HasForeignKey("MediaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OI.API.Models.Tag", "Tag")
                        .WithMany("MediaTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Media");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("OI.API.Models.Media", b =>
                {
                    b.Navigation("MediaTags");
                });

            modelBuilder.Entity("OI.API.Models.Tag", b =>
                {
                    b.Navigation("MediaTags");
                });
#pragma warning restore 612, 618
        }
    }
}
