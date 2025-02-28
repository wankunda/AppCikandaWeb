using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace ApiCikanda;

[ApiController]
[Route("produitvendu")]
public class ProduitVenduController : ControllerBase
{
    private readonly AppDbContext dbContext;

    public ProduitVenduController(AppDbContext context)
    {
        dbContext = context;
    }

    [HttpGet("gets")]
    public async Task<IEnumerable<ProduitVendu>> GetProduitVendusAsync()
    {
        return await dbContext.ProduitVendus
        .Include(e => e.PrixVente)
        .Include(e => e.Command)
        .ToListAsync();
    }

    [HttpGet("get/{id}")]
    public async Task<ProduitVendu?> GetProduitVenduAsync(int id)
    {
        return await dbContext.ProduitVendus
        .Include(e => e.PrixVente)
        .ThenInclude(e => e!.Article)
        .Include(e => e.Command)
        .FirstOrDefaultAsync(e => e.Id == id);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateProduitVenduAsync([FromBody] ProduitVendu produitvendu)
    {
        dbContext.ProduitVendus.Add(produitvendu);

        try
        {
            await dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProduitVenduAsync), new { id = produitvendu.Id }, produitvendu);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateProduitVenduAsync(int id, [FromBody] ProduitVendu produitvendu)
    {
        if (id != produitvendu.Id)
            return BadRequest();


        dbContext.ProduitVendus.Update(produitvendu);

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
    public async Task<IActionResult> DeleteProduitVenduAsync(int id)
    {
        var produitvendu = await dbContext.ProduitVendus.FirstOrDefaultAsync(e => e.Id == id);

        if (produitvendu == null)
            return NotFound();

        produitvendu.Delete = true;
        dbContext.ProduitVendus.Update(produitvendu);

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
        return dbContext.ProduitVendus.Any(e => e.Id == id);
    }
}