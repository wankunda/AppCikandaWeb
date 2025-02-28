using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace ApiCikanda;

[ApiController]
[Route("prixvente")]
public class PrixVenteController : ControllerBase
{
    private readonly AppDbContext dbContext;

    public PrixVenteController(AppDbContext context)
    {
        dbContext = context;
    }

    [HttpGet("gets")]
    public async Task<IEnumerable<PrixVente>> GetPrixVentesAsync()
    {
        return await dbContext.PrixVentes
        .Include(e => e.Article)
        .ToListAsync();
    }

    [HttpGet("get/{id}")]
    public async Task<PrixVente?> GetPrixVenteAsync(int id)
    {
        return await dbContext.PrixVentes
        .Include(e => e.Article)
        .Include(e => e.PointVente)
        .FirstOrDefaultAsync(e => e.Id == id);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreatePrixVenteAsync([FromBody] PrixVente prixvente)
    {
        dbContext.PrixVentes.Add(prixvente);

        try
        {
            await dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPrixVenteAsync), new { id = prixvente.Id }, prixvente);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdatePrixVenteAsync(int id, [FromBody] PrixVente prixvente)
    {
        if (id != prixvente.Id)
            return BadRequest();


        dbContext.PrixVentes.Update(prixvente);

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

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeletePrixVenteAsync(int id)
    {
        var prixvente = await dbContext.PrixVentes.FirstOrDefaultAsync(e => e.Id == id);

        if (prixvente == null)
            return NotFound();

        prixvente.Delete = true;
        dbContext.PrixVentes.Update(prixvente);

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

    private bool Exists(int id)
    {
        return dbContext.PrixVentes.Any(e => e.Id == id);
    }
}