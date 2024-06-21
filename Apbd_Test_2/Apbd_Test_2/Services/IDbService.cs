
using Apbd_Test_2.Models.Domain;

namespace Apbd_Test_2.Services;

public interface IDbService
{
    Task<bool> DoesCharacterExit(int characterId);

    Task<Characters> GetCharacterById(int characterId);
    
    Task<bool> DoesItemExist(int itemId);
    
    Task<Items> GetItemById(int itemId);

    Task<bool> DoesBackpackExit(int characterId, int itemId);
    
    Task<Backpacks> GetBackpack(int characterId, int itemId);

    Task<Backpacks> UpdateBackpack(Backpacks backpack);
    
    Task<Items> UpdateItem(Items item);
    
    Task<Backpacks> AddBackpackItem(Backpacks backpack);
    
    Task UpdateCharacter(Characters character);
    
    
}