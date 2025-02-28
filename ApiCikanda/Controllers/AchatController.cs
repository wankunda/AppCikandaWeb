using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace ApiCikanda;

[ApiController]
[Route("achat")]
public class AchatController : ControllerBase
{
    private readonly AppDbContext dbContext;

    public AchatController(AppDbContext context)
    {
        dbContext = context;
    }

    [HttpGet("gets")]
    public async Task<IEnumerable<Achat>> GetAchatsAsync()
    {
        return await dbContext.Achats
        .Include(e => e.Produits)
        .ThenInclude(e => e.Article)
        .ToListAsync();
    }

    [HttpGet("get/{id}")]
    public async Task<Achat?> GetAchatAsync(string id)
    {
        return await dbContext.Achats
        .Include(e => e.Produits)
        .ThenInclude(e => e.Article)
        .FirstOrDefaultAsync(e => e.Code == id);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateAchatAsync([FromBody] Achat achat)
    {
        dbContext.Achats.Add(achat);

        try
        {
            await dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAchatAsync), new { id = achat.Id }, achat);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateAchatAsync(string id, [FromBody] Achat achat)
    {
        if (id != achat.Code)
            return BadRequest();

        dbContext.Achats.Update(achat);

        try
        {
            await dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            if (!Exists(id))
                return NotFound(ex.Message);
            else
                throw;
        }
        return NoContent();
    }

    [HttpDelete("delete/{code}")]
    public async Task<IActionResult> DeleteAchatAsync(string code)
    {
        var achat = await dbContext.Achats.FirstOrDefaultAsync(e => e.Code == code);

        if (achat == null)
            return NotFound();

        achat.Delete = true;
        dbContext.Achats.Update(achat);

        try
        {
            await dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            if (!Exists(code))
                return NotFound(ex.Message);
            else
                throw;
        }
        return NoContent();
    }

    private bool Exists(string id)
    {
        return dbContext.Achats.Any(e => e.Code == id);
    }
}