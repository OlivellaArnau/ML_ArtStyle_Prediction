﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ArtStyle_API.Migrations
{
    [DbContext(typeof(ArtStyleContext))]
    partial class ArtStyleContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ArtStyle_API.Models.ArtStyle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImportantAuthors")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Period")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ArtStyles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Art movement characterized by its focus on light and color.",
                            ImportantAuthors = "Claude Monet, Edgar Degas",
                            Name = "Impressionism",
                            Period = "19th Century"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Art style that breaks objects down into geometric shapes.",
                            ImportantAuthors = "Pablo Picasso, Georges Braque",
                            Name = "Cubism",
                            Period = "Early 20th Century"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Movement that seeks to express the subconscious through dreamlike images.",
                            ImportantAuthors = "Salvador Dalí, René Magritte",
                            Name = "Surrealism",
                            Period = "20th Century"
                        },
                        new
                        {
                            Id = 4,
                            Description = "Period of rediscovery of classical art and culture.",
                            ImportantAuthors = "Leonardo da Vinci, Michelangelo",
                            Name = "Renaissance",
                            Period = "15th - 16th Century"
                        },
                        new
                        {
                            Id = 5,
                            Description = "Style that focuses on shapes and colors not representative of real objects.",
                            ImportantAuthors = "Wassily Kandinsky, Piet Mondrian",
                            Name = "Abstract",
                            Period = "20th Century"
                        });
                });

            modelBuilder.Entity("ArtStyle_API.Models.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ArtStyleId")
                        .HasColumnType("int");

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ArtStyleId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("ArtStyle_API.Models.Image", b =>
                {
                    b.HasOne("ArtStyle_API.Models.ArtStyle", "ArtStyle")
                        .WithMany()
                        .HasForeignKey("ArtStyleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ArtStyle");
                });
#pragma warning restore 612, 618
        }
    }
}