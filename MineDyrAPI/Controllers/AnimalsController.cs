using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MineDyrAPI.Data;
using MineDyrAPI.Entities;

namespace MineDyrAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnimalsController : ControllerBase
{
    private readonly AppDbContext _db;
    public AnimalsController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _db.Animals.ToListAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) =>
        await _db.Animals.Include(a => a.Owner).FirstOrDefaultAsync(a => a.Id == id) is Animal a
            ? Ok(a)
            : NotFound();

    
    
    [HttpPost]
    public async Task<IActionResult> Create(AnimalCreateDto input)
    {
        // Krever at OwnerId finnes
        if (!await _db.Users.AnyAsync(u => u.Id == input.OwnerId))
            return BadRequest("Ugyldig OwnerId");

        var animal = new Animal();
        
        animal.Name = input.Name;
        animal.Species = input.Species;
        animal.OwnerId = input.OwnerId;

        _db.Animals.Add(animal);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = input.OwnerId }, input);
    }
    

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, AnimalUpdateDto input)
    {
        var animal = await _db.Animals.FindAsync(id);
        if (animal is null) return NotFound();

        animal.Name = input.Name;
        animal.Species = input.Species;

        await _db.SaveChangesAsync();
        return NoContent();
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var animal = await _db.Animals.FindAsync(id);
        if (animal is null) return NotFound();
        _db.Animals.Remove(animal);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}