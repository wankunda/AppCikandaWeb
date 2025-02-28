using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace ApiCikanda;

[ApiController]
[Route("taux")]
public class TauxController : ControllerBase
{
    private readonly AppDbContext dbContext;

    public TauxController(AppDbContext context)
    {
        dbContext = context;
    }

    [HttpGet("gets")]
    public async Task<IEnumerable<Taux>> GetTauxAsync()
    {
        return await dbContext.Taux
        .ToListAsync();
    }

    [HttpGet("get/{id}")]
    public async Task<Taux?> GetTauxAsync(int id)
    {
        return await dbContext.Taux
        .Include(e => e.PointVente)
        .FirstOrDefaultAsync(e => e.Id == id);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateTauxAsync([FromBody] Taux taux)
    {
        dbContext.Taux.Add(taux);

        try
        {
            await dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTauxAsync), new { id = taux.Id }, taux);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateTauxAsync(int id, [FromBody] Taux taux)
    {
        if (id != taux.Id)
            return BadRequest();


        dbContext.Taux.Update(taux);

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
    public async Task<IActionResult> DeleteTauxAsync(int id)
    {
        var taux = await dbContext.Taux.FirstOrDefaultAsync(e => e.Id == id);

        if (taux == null)
            return NotFound();

        taux.Delete = true;
        dbContext.Taux.Update(taux);

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
        return dbContext.Taux.Any(e => e.Id == id);
    }
}