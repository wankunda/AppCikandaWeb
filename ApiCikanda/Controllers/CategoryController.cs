using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace ApiCikanda;

[ApiController]
[Route("category")]
public class CategoryController : ControllerBase
{
    private readonly AppDbContext dbContext;

    public CategoryController(AppDbContext context)
    {
        dbContext = context;
    }

    [HttpGet("gets")]
    public async Task<IEnumerable<Category>> GetCategoriesAsync()
    {
        return await dbContext.Categories
        .Include(e => e.Articles)
        .ToListAsync();
    }

    [HttpGet("get/{id}")]
    public async Task<Category?> GetCategoryAsync(string id)
    {
        return await dbContext.Categories
        .Include(e => e.Articles)
        .FirstOrDefaultAsync(e => e.Code == id);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateCategoryAsync([FromBody] Category category)
    {
        dbContext.Categories.Add(category);

        try
        {
            await dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCategoryAsync), new { id = category.Id }, category);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateCategoryAsync(string id, [FromBody] Category category)
    {
        if (id != category.Code)
            return BadRequest();


        dbContext.Categories.Update(category);

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
    public async Task<IActionResult> DeleteCategoryAsync(string id)
    {
        var category = await dbContext.Categories.FirstOrDefaultAsync(e => e.Code == id);

        if (category == null)
            return NotFound();

        category.Delete = true;
        dbContext.Categories.Update(category);

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

    private bool Exists(string id)
    {
        return dbContext.Categories.Any(e => e.Code == id);
    }
}