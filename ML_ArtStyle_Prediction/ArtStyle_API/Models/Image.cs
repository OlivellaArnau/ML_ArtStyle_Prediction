namespace ArtStyle_API.Models
{
    public class Image
    {
        public int Id { get; set; }          
        public string? ImagePath { get; set; }
        public int ArtStyleId { get; set; }  
        public ArtStyle? ArtStyle { get; set; }
    }
}
