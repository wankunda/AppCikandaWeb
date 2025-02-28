using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace ApiCikanda;

[ApiController]
[Route("command")]
public class CommandController : ControllerBase
{
    private readonly AppDbContext dbContext;

    public CommandController(AppDbContext context)
    {
        dbContext = context;
    }

    [HttpGet("gets")]
    public async Task<IEnumerable<Command>> GetCommandsAsync()
    {
        return await dbContext.Commands
        .Include(e => e.ProduitVendus)
        .ToListAsync();
    }

    [HttpGet("get/{id}")]
    public async Task<Command?> GetCommandAsync(int id)
    {
        return await dbContext.Commands
        .Include(e => e.ProduitVendus)
        .ThenInclude(e => e.PrixVente)
        .ThenInclude(e => e!.Article)
        .FirstOrDefaultAsync(e => e.Id == id);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateCommandAsync([FromBody] Command command)
    {
        dbContext.Commands.Add(command);

        try
        {
            await dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCommandAsync), new { id = command.Id }, command);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateCommandAsync(int id, [FromBody] Command command)
    {
        if (id != command.Id)
            return BadRequest();


        dbContext.Commands.Update(command);

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
    public async Task<IActionResult> DeleteCommandAsync(int id)
    {
        var command = await dbContext.Commands.FirstOrDefaultAsync(e => e.Id == id);

        if (command == null)
            return NotFound();

        command.Delete = true;
        dbContext.Commands.Update(command);

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
        return dbContext.Commands.Any(e => e.Id == id);
    }
}