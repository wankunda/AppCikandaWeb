using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace ApiCikanda;

[ApiController]
[Route("pointvente")]
public class PointVenteController : ControllerBase
{
    private readonly AppDbContext dbContext;

    public PointVenteController(AppDbContext context)
    {
        dbContext = context;
    }

    [HttpGet("gets")]
    public async Task<IEnumerable<PointVente>> GetPointVentesAsync()
    {
        return await dbContext.PointVentes
        .Include(e => e.PrixVentes)
        .ToListAsync();
    }

    [HttpGet("get/{id}")]
    public async Task<PointVente?> GetPointVenteAsync(int id)
    {
        return await dbContext.PointVentes
        .Include(e => e.PrixVentes)
        .ThenInclude(e => e.Article)
        .FirstOrDefaultAsync(e => e.Id == id);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreatePointVenteAsync([FromBody] PointVente pointvente)
    {
        dbContext.PointVentes.Add(pointvente);

        try
        {
            await dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPointVenteAsync), new { id = pointvente.Id }, pointvente);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdatePointVenteAsync(int id, [FromBody] PointVente pointvente)
    {
        if (id != pointvente.Id)
            return BadRequest();


        dbContext.PointVentes.Update(pointvente);

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
    public async Task<IActionResult> DeletePointVenteAsync(int id)
    {
        var pointvente = await dbContext.PointVentes.FirstOrDefaultAsync(e => e.Id == id);

        if (pointvente == null)
            return NotFound();

        pointvente.Delete = true;
        dbContext.PointVentes.Update(pointvente);

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
        return dbContext.PointVentes.Any(e => e.Id == id);
    }
}