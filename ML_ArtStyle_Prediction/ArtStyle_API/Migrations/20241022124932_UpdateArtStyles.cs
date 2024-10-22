using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ArtStyle_API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateArtStyles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ArtStyles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImportantAuthors = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Period = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtStyles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ArtStyleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_ArtStyles_ArtStyleId",
                        column: x => x.ArtStyleId,
                        principalTable: "ArtStyles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ArtStyles",
                columns: new[] { "Id", "Description", "ImportantAuthors", "Name", "Period" },
                values: new object[,]
                {
                    { 1, "Art movement characterized by its focus on light and color.", "Claude Monet, Edgar Degas", "Impressionism", "19th Century" },
                    { 2, "Art style that breaks objects down into geometric shapes.", "Pablo Picasso, Georges Braque", "Cubism", "Early 20th Century" },
                    { 3, "Movement that seeks to express the subconscious through dreamlike images.", "Salvador Dalí, René Magritte", "Surrealism", "20th Century" },
                    { 4, "Period of rediscovery of classical art and culture.", "Leonardo da Vinci, Michelangelo", "Renaissance", "15th - 16th Century" },
                    { 5, "Style that focuses on shapes and colors not representative of real objects.", "Wassily Kandinsky, Piet Mondrian", "Abstract", "20th Century" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Images_ArtStyleId",
                table: "Images",
                column: "ArtStyleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "ArtStyles");
        }
    }
}
