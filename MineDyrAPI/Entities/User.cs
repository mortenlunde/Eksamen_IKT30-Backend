using System.ComponentModel.DataAnnotations;

namespace MineDyrAPI.Entities;

public class User
{
    [Key]
    public int Id { get; init; }
    
    [Required(ErrorMessage = "Fornavn må fylles ut"), MaxLength(50)]
    public string FirstName { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Etternavn må fylles ut"), MaxLength(50)]
    public string LastName { get; set; } = string.Empty;
    
    public ICollection<Animal> Animals { get; init; } = new List<Animal>();
}

public record UserDto(string FirstName, string LastName );