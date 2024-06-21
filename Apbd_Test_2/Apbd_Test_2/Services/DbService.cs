using Apbd_Test_2.Data;
using Apbd_Test_2.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Apbd_Test_2.Services;


public class DbService : IDbService
{
    private readonly DatabaseContext _context;

    public DbService(DatabaseContext context)
    {
        _context = context;
    }

    public Task<bool> DoesCharacterExit(int characterId)
    {
        return _context.Characters.AnyAsync(e => e.Id == characterId);
    }

    public Task<Characters> GetCharacterById(int characterId)
    {
        return _context.Characters
            .Include(e => e.Backpacks)
                .ThenInclude(b => b.Items)
            .Include(e => e.CharacterTitles)
                .ThenInclude(t => t.Titles)
            .Where(e => e.Id == characterId)
            .FirstAsync();
    }
}