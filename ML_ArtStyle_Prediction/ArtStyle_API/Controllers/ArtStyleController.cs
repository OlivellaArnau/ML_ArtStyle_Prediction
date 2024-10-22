using ArtStyle_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class ArtStyleController : ControllerBase
{
    private readonly ArtStyleContext _context;

    public ArtStyleController(ArtStyleContext context)
    {
        _context = context;
    }

    // GET: api/artstyle
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ArtStyle>>> GetArtStyles()
    {
        return await _context.ArtStyles.ToListAsync();
    }

    // POST: api/artstyle
    [HttpPost]
    public async Task<ActionResult<ArtStyle>> PostArtStyle(ArtStyle artStyle)
    {
        _context.ArtStyles.Add(artStyle);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetArtStyle", new { id = artStyle.Id }, artStyle);
    }
}