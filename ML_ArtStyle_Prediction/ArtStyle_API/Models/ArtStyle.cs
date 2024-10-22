namespace ArtStyle_API.Models
{
    public class ArtStyle
    {
        public int Id { get; set; }         
        public string? Name { get; set; }     
        public string? Description { get; set; } 
        public string? ImportantAuthors { get; set; } 
        public string? Period { get; set; }   
    }
}
