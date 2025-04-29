using Microsoft.EntityFrameworkCore;
using MineDyrAPI.Entities;

namespace MineDyrAPI.Data;

public class AppDbContext : DbContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public string Schema { get; private set; }

    public AppDbContext(DbContextOptions<AppDbContext> options,
        IHttpContextAccessor httpContextAccessor)
        : base(options)
    {
        _httpContextAccessor = httpContextAccessor;
        Schema = _httpContextAccessor.HttpContext?
                     .Request.Headers["X-Tenant-Schema"].FirstOrDefault()
                 ?? "public";

        // Opprett schema dynamisk (opptil 10 ulike)
        if (_httpContextAccessor.HttpContext?.Request != null)
        {
            Database.ExecuteSqlRaw($"CREATE SCHEMA IF NOT EXISTS \"{Schema}\";");
        }
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Animal> Animals { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schema);
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<User>()
            .HasMany(u => u.Animals)
            .WithOne(a => a.Owner!)
            .HasForeignKey(a => a.OwnerId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, FirstName = "Alice", LastName = "Hansson" },
            new User { Id = 2, FirstName = "Bob",   LastName = "Jackson" },
            new User { Id = 3, FirstName = "Charlie", LastName = "Smith" },
            new User { Id = 4, FirstName = "David",   LastName = "Brown" },
            new User { Id = 5, FirstName = "Eve",     LastName = "White" }
        );
        modelBuilder.Entity<Animal>().HasData(
            new Animal { Id = 1, Name = "Toto",     Species = "Dog", OwnerId = 1 },
            new Animal { Id = 2, Name = "Whiskers", Species = "Cat", OwnerId = 2 },
            new Animal { Id = 3, Name = "Bella",    Species = "Dog", OwnerId = 2 },
            new Animal { Id = 4, Name = "Buster",   Species = "Cat", OwnerId = 1 },
            new Animal { Id = 5, Name = "Bubba",    Species = "Dog", OwnerId = 1 },
            new Animal { Id = 6, Name = "Bingo",    Species = "Cat", OwnerId = 2 },
            new Animal { Id = 7,  Name = "Snowball",  Species = "Rabbit", OwnerId = 4 },
            new Animal { Id = 8,  Name = "Max",       Species = "Dog", OwnerId = 5 },
            new Animal { Id = 9,  Name = "Luna",      Species = "Cat", OwnerId = 3 },
            new Animal { Id = 10, Name = "Charlie",   Species = "Parrot", OwnerId = 5 }
        );
    }
}