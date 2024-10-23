using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ArtStyle_API.Models;
using ArtStyle_MLModell_Console_1;

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
    public async Task<IActionResult> UploadImage(IFormFile file)
    {
        if (file.Length > 0)
        {
            // Save the uploaded file temporarily
            var tempFilePath = Path.Combine("ruta/temporal", file.FileName);

            using (var stream = new FileStream(tempFilePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Load the image as a byte array for ML model input
            byte[] imageBytes;
            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                imageBytes = stream.ToArray();
            }

            // Prepare the input for the ML model
            var mlContext = new MLContext();
            var modelInput = new ArtStyle_MLModell_Console_1.ModelInput
            {
                ImageSource = imageBytes
            };

            // Get the prediction result
            var result = ArtStyle_MLModell_Console_1.Predict(modelInput);
            var predictedStyle = result.PredictedLabel;

            // Move the image to the folder based on the predicted style
            var targetFolder = Path.Combine("ruta/ML_Collected_Data", predictedStyle);
            if (!Directory.Exists(targetFolder))
            {
                Directory.CreateDirectory(targetFolder);
            }

            var finalFilePath = Path.Combine(targetFolder, file.FileName);
            System.IO.File.Move(tempFilePath, finalFilePath);

            // Save the image information to the database
            var image = new Image
            {
                ImagePath = finalFilePath,
                ArtStyle = predictedStyle
            };

            _context.Images.Add(image);
            await _context.SaveChangesAsync();

            return Ok(new { predictedStyle, finalFilePath });
        }

        return BadRequest("No file uploaded");
    }

}

