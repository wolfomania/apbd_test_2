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
    }
}