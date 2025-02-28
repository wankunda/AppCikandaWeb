using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace ApiCikanda;

[ApiController]
[Route("produitachat")]
public class ProduitAchatController : ControllerBase
{
    private readonly AppDbContext dbContext;

    public ProduitAchatController(AppDbContext context)
    {
        dbContext = context;
    }

    [HttpGet("gets")]
    public async Task<IEnumerable<ProduitAchat>> GetProduitAchatsAsync()
    {
        return await dbContext.ProduitAchats
        .Include(e => e.Achat)
        .ToListAsync();
    }

    [HttpGet("get/{id}")]
    public async Task<ProduitAchat?> GetProduitAchatAsync(int id)
    {
        return await dbContext.ProduitAchats
        .Include(e => e.Achat)
        .Include(e => e.Article)
        .FirstOrDefaultAsync(e => e.Id == id);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateProduitAchatAsync([FromBody] ProduitAchat produitachat)
    {
        dbContext.ProduitAchats.Add(produitachat);

        try
        {
            await dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProduitAchatAsync), new { id = produitachat.Id }, produitachat);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateProduitAchatAsync(int id, [FromBody] ProduitAchat produitachat)
    {
        if (id != produitachat.Id)
            return BadRequest();


        dbContext.ProduitAchats.Update(produitachat);

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
    public async Task<IActionResult> DeleteProduitAchatAsync(int id)
    {
        var produitachat = await dbContext.ProduitAchats.FirstOrDefaultAsync(e => e.Id == id);

        if (produitachat == null)
            return NotFound();

        produitachat.Delete = true;
        dbContext.ProduitAchats.Update(produitachat);

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
        return dbContext.ProduitAchats.Any(e => e.Id == id);
    }
}