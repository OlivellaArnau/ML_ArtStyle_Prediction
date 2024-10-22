using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ArtStyle_API.Models;

[ApiController]
[Route("api/[controller]")]
public class ImageController : ControllerBase
{
    private readonly ArtStyleContext _context;

    public ImageController(ArtStyleContext context)
    {
        _context = context;
    }

    // GET: api/image
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Image>>> GetImages()
    {
        return await _context.Images.Include(i => i.ArtStyle).ToListAsync();
    }

    // POST: api/image
    [HttpPost]
    public async Task<ActionResult<Image>> PostImage(Image image)
    {
        _context.Images.Add(image);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetImage", new { id = image.Id }, image);
    }
    [HttpPost("upload")]
    public async Task<IActionResult> UploadImage(IFormFile file, string predictedArtStyle)
    {
        if (file.Length > 0)
        {
            // Define the path based on predicted art style
            var folderPath = Path.Combine("ML_ArtStyleTrainingData", "ML_Collected_Data", predictedArtStyle);

            // Create the directory if it doesn't exist
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // Combine the file path
            var filePath = Path.Combine(folderPath, file.FileName);

            // Save the image to the specified path
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Save image metadata in the database
            var image = new Image
            {
                ImagePath = filePath,
                //ArtStyle = predictedArtStyle
            };

            _context.Images.Add(image);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Image uploaded successfully", imagePath = filePath });
        }

        return BadRequest("No file uploaded");
    }
}

