using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace ApiCikanda;

[ApiController]
[Route("article")]
public class ArticleController : ControllerBase
{
    private readonly AppDbContext dbContext;

    public ArticleController(AppDbContext context)
    {
        dbContext = context;
    }

    [HttpGet("gets")]
    public async Task<IEnumerable<Article>> GetArticlesAsync()
    {
        return await dbContext.Articles
        .Include(e => e.Category)
        .ToListAsync();
    }

    [HttpGet("get/{id}")]
    public async Task<Article?> GetArticleAsync(string id)
    {
        return await dbContext.Articles
        .Include(e => e.Category)
        .FirstOrDefaultAsync(e => e.Code == id);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateArticleAsync([FromBody] Article article)
    {
        dbContext.Articles.Add(article);

        try
        {
            await dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetArticleAsync), new { id = article.Id }, article);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateArticleAsync(string id, [FromBody] Article article)
    {
        if (id != article.Code)
            return BadRequest();

        dbContext.Articles.Update(article);

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
    public async Task<IActionResult> DeleteArticleAsync(string id)
    {
        var article = await dbContext.Articles.FirstOrDefaultAsync(e => e.Code == id);

        if (article == null)
            return NotFound();

        article.Delete = true;
        dbContext.Articles.Update(article);

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
        return dbContext.Articles.Any(e => e.Code == id);
    }
}