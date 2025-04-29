using System.ComponentModel.DataAnnotations;

namespace MineDyrAPI.Entities;

public class Animal
{
    [Key]
    public int Id { get; init; }
    
    [Required(ErrorMessage = "Dyret må ha et navn"), MaxLength(50)]
    public string Name { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Du må velge en type dyr (hund, katt, etc...)"), MaxLength(50)]
    public string Species { get; set; } = string.Empty;

    public int? OwnerId { get; set; }
    
    public User? Owner { get; init; }
}

public record AnimalCreateDto(string Name, string Species, int OwnerId );
public record AnimalUpdateDto(string Name, string Species);