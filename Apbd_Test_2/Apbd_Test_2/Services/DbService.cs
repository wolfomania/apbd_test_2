using Apbd_Test_2.Data;
using Apbd_Test_2.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Apbd_Test_2.Services;


public class DbService(DatabaseContext context) : IDbService
{
    public async Task<bool> DoesCharacterExit(int characterId)
    {
        return await context.Characters.AnyAsync(e => e.Id == characterId);
    }

    public async Task<Characters> GetCharacterById(int characterId)
    {
        return await context.Characters
            .Include(e => e.Backpacks)
                .ThenInclude(b => b.Items)
            .Include(e => e.CharacterTitles)
                .ThenInclude(t => t.Titles)
            .Where(e => e.Id == characterId)
            .FirstAsync();
    }

    public async Task<bool> DoesItemExist(int itemId)
    {
        return await context.Items.AnyAsync(e => e.Id == itemId);
    }

    public async Task<Items> GetItemById(int itemId)
    {
        return await context.Items
            .Where(e => e.Id == itemId)
            .FirstAsync();
    }

    public async Task<Items> UpdateItem(Items item)
    {
        context.Items.Update(item);
        await context.SaveChangesAsync();
        return item;
    }


    public async Task<bool> DoesBackpackExit(int characterId, int itemId)
    {
        return await context.Backpacks.AnyAsync(e => e.CharacterId == characterId && e.ItemId == itemId);
    }
    
    public async Task<Backpacks> GetBackpack(int characterId, int itemId)
    {
        return await context.Backpacks
            .Where(e => e.CharacterId == characterId && e.ItemId == itemId)
            .FirstAsync();
    }

    public async Task<Backpacks> AddBackpackItem(Backpacks backpack)
    {
        await context.Backpacks.AddAsync(backpack);
        await context.SaveChangesAsync();
        return backpack;
    }
    
    public async Task<Backpacks> UpdateBackpack(Backpacks backpack)
    {
        context.Backpacks.Update(backpack);
        await context.SaveChangesAsync();
        return backpack;
    }

    public async Task UpdateCharacter(Characters character)
    {
        context.Characters.Update(character);
        await context.SaveChangesAsync();
    }
}