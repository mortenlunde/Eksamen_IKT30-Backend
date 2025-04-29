using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MineDyrAPI.Data;
using MineDyrAPI.Entities;

namespace MineDyrAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly AppDbContext _db;
    public UsersController(AppDbContext db) => _db = db;

    // GET: api/users
    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _db.Users.Include(u => u.Animals).ToListAsync());

    // GET: api/users/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) =>
        await _db.Users.Include(u => u.Animals)
            .FirstOrDefaultAsync(u => u.Id == id) is User u
            ? Ok(u)
            : NotFound();

    // POST: api/users
    [HttpPost]
    public async Task<IActionResult> Create(UserDto input)
    {
        var user = new User();
        user.FirstName = input.FirstName;
        user.LastName = input.LastName;
        
        _db.Users.Add(user);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = user.Id }, input);
    }

    // PUT: api/users/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UserDto input)
    {
        var user = await _db.Users.FindAsync(id);
        if (user is null) return NotFound();
        
        user.FirstName = input.FirstName;
        user.LastName = input.LastName;
        
        await _db.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: api/users/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var user = await _db.Users.FindAsync(id);
        if (user is null) return NotFound();
        _db.Users.Remove(user);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}