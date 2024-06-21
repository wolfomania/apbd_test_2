using Apbd_Test_2.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Apbd_Test_2.Data;

public class DatabaseContext : DbContext
{
    
    
    protected DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Backpacks> Backpacks { get; set; }
    public DbSet<Characters> Characters { get; set; }
    public DbSet<CharacterTitles> CharacterTitles { get; set; }
    public DbSet<Items> Items { get; set; }
    public DbSet<Titles> Titles { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Characters>().HasData(
            new Characters { Id = 1, FirstName = "John", LastName = "Yakuza", CurrentWei = 21, MaxWeight = 200 },
            new Characters { Id = 2, FirstName = "Jane", LastName = "Doe", CurrentWei = 12, MaxWeight = 200 }
        );

        modelBuilder.Entity<Items>().HasData(
            new Items { Id = 1, Name = "Item1", Weight = 10 },
            new Items { Id = 2, Name = "Item2", Weight = 11 },
            new Items { Id = 3, Name = "Item3", Weight = 12 }
        );

        modelBuilder.Entity<Titles>().HasData(
            new Titles { Id = 1, Name = "Title1" },
            new Titles { Id = 2, Name = "Title2" },
            new Titles { Id = 3, Name = "Title3" }
        );

        modelBuilder.Entity<Backpacks>().HasData(
            new Backpacks { CharacterId = 1, ItemId = 1, Amount = 2 },
            new Backpacks { CharacterId = 1, ItemId = 2, Amount = 1 },
            new Backpacks { CharacterId = 2, ItemId = 3, Amount = 1 }
        );

        modelBuilder.Entity<CharacterTitles>().HasData(
            new CharacterTitles { CharacterId = 1, TitleId = 1, AcquiredAt = DateTime.Parse("2024-06-10T00:00:00") },
            new CharacterTitles { CharacterId = 1, TitleId = 2, AcquiredAt = DateTime.Parse("2024-06-09T00:00:00") },
            new CharacterTitles { CharacterId = 2, TitleId = 3, AcquiredAt = DateTime.Parse("2024-06-08T00:00:00") }
        );
    }
}