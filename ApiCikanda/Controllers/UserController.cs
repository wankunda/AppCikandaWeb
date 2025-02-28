using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace ApiCikanda;

[ApiController]
[Route("user")]
public class UserController : ControllerBase
{
    private readonly AppDbContext dbContext;

    public UserController(AppDbContext context)
    {
        dbContext = context;
    }

    [HttpGet("gets")]
    public async Task<IEnumerable<User>> GetUsersAsync()
    {
        return await dbContext.Users
        .ToListAsync();
    }

    [HttpGet("get/{id}")]
    public async Task<User?> GetUserAsync(int id)
    {
        return await dbContext.Users
        .FirstOrDefaultAsync(e => e.Id == id);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateUserAsync([FromBody] User user)
    {
        dbContext.Users.Add(user);

        try
        {
            await dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUserAsync), new { id = user.Id }, user);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateUserAsync(int id, [FromBody] User user)
    {
        if (id != user.Id)
            return BadRequest();


        dbContext.Users.Update(user);

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
    public async Task<IActionResult> DeleteUserAsync(int id)
    {
        var user = await dbContext.Users.FirstOrDefaultAsync(e => e.Id == id);

        if (user == null)
            return NotFound();

        user.Delete = true;
        dbContext.Users.Update(user);

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
        return dbContext.Users.Any(e => e.Id == id);
    }
}