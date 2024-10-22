using ArtStyle_API.Models;
using Microsoft.EntityFrameworkCore;

public class ArtStyleContext : DbContext
{
    public ArtStyleContext(DbContextOptions<ArtStyleContext> options) : base(options) { }

    public DbSet<ArtStyle> ArtStyles { get; set; }
    public DbSet<Image> Images { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Image>()
            .HasOne(i => i.ArtStyle)                // Cada imagen tiene un estilo artístico
            .WithMany()                             // Un estilo artístico puede tener muchas imágenes
            .HasForeignKey(i => i.ArtStyleId);     // Establece ArtStyleId como Foreign Key

        // Datos iniciales para los estilos artísticos
        modelBuilder.Entity<ArtStyle>().HasData(
        new ArtStyle { Id = 1, Name = "Impressionism", Description = "Art movement characterized by its focus on light and color.", ImportantAuthors = "Claude Monet, Edgar Degas", Period = "19th Century" },
        new ArtStyle { Id = 2, Name = "Cubism", Description = "Art style that breaks objects down into geometric shapes.", ImportantAuthors = "Pablo Picasso, Georges Braque", Period = "Early 20th Century" },
        new ArtStyle { Id = 3, Name = "Surrealism", Description = "Movement that seeks to express the subconscious through dreamlike images.", ImportantAuthors = "Salvador Dalí, René Magritte", Period = "20th Century" },
        new ArtStyle { Id = 4, Name = "Renaissance", Description = "Period of rediscovery of classical art and culture.", ImportantAuthors = "Leonardo da Vinci, Michelangelo", Period = "15th - 16th Century" },
        new ArtStyle { Id = 5, Name = "Abstract", Description = "Style that focuses on shapes and colors not representative of real objects.", ImportantAuthors = "Wassily Kandinsky, Piet Mondrian", Period = "20th Century" } // Added abstract style
   );
    }
}