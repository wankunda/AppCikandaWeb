using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace ApiCikanda;

[ApiController]
[Route("depense")]
public class DepenseController : ControllerBase
{
    private readonly AppDbContext dbContext;

    public DepenseController(AppDbContext context)
    {
        dbContext = context;
    }

    [HttpGet("gets")]
    public async Task<IEnumerable<Depense>> GetDepensesAsync()
    {
        return await dbContext.Depenses
        .Include(e => e.PointVente)
        .ToListAsync();
    }

    [HttpGet("get/{id}")]
    public async Task<Depense?> GetDepenseAsync(int id)
    {
        return await dbContext.Depenses
        .Include(e => e.PointVente)
        .FirstOrDefaultAsync(e => e.Id == id);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateDepenseAsync([FromBody] Depense depense)
    {
        dbContext.Depenses.Add(depense);

        try
        {
            await dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetDepenseAsync), new { id = depense.Id }, depense);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateDepenseAsync(int id, [FromBody] Depense depense)
    {
        if (id != depense.Id)
            return BadRequest();


        dbContext.Depenses.Update(depense);

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
    public async Task<IActionResult> DeleteDepenseAsync(int id)
    {
        var depense = await dbContext.Depenses.FirstOrDefaultAsync(e => e.Id == id);

        if (depense == null)
            return NotFound();

        depense.Delete = true;
        dbContext.Depenses.Update(depense);

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
        return dbContext.Depenses.Any(e => e.Id == id);
    }
}