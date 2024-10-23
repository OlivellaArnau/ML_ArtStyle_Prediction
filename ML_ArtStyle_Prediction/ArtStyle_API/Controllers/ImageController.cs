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
        if (file == null || file.Length == 0)
        {
            return BadRequest("No file uploaded.");
        }

        // Aquí puedes realizar la predicción y obtener la etiqueta
        var imageBytes = await ConvertFileToByteArray(file);
        var input = new ArtStyle_MLModell.ModelInput { ImageSource = imageBytes };
        var prediction = ArtStyle_MLModell.Predict(input);
        var predictedLabel = prediction.PredictedLabel;

        // Define la ruta donde deseas guardar la imagen
        string directoryPath = Path.Combine(@"E:\Documentos\Estudios\Erasmus\ML_Project_Artstyle_Prediction\ML_ArtStyleTrainingData\ML_Collected_Data", predictedLabel);
        Directory.CreateDirectory(directoryPath); // Crea la carpeta si no existe

        // Define la ruta completa del archivo
        string filePath = Path.Combine(directoryPath, file.FileName);

        // Guarda el archivo en la ruta especificada
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return Ok(new { FilePath = filePath, PredictedLabel = predictedLabel });
    }

    private async Task<byte[]> ConvertFileToByteArray(IFormFile file)
    {
        using (var memoryStream = new MemoryStream())
        {
            await file.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }
    }
}

