using System.Transactions;
using Apbd_Test_2.Models.Domain;
using Apbd_Test_2.Models.DTOs;
using Apbd_Test_2.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Apbd_Test_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController(IDbService dbService) : ControllerBase
    {
        [HttpGet("{characterId:int}")]
        public async Task<IActionResult> GetCharacter(int characterId)
        {
            if (!await dbService.DoesCharacterExit(characterId))
                return NotFound($"No Character with id: {characterId}");

            var character = await dbService.GetCharacterById(characterId);

            var result = new
            {
                firstName = character.FirstName,
                lastName = character.LastName,
                currentWeight = character.CurrentWei,
                maxWeight = character.MaxWeight,
                backpackItems = character.Backpacks
                    .Select(backpacks => new 
                    {
                        itemName = backpacks.Items.Name,
                        itemWeight = backpacks.Items.Weight,
                        amount = backpacks.Amount
                    }),
                titles = character.CharacterTitles
                    .Select(characterTitles => new
                    {
                        title = characterTitles.Titles.Name,
                        acquiredAt = characterTitles.AcquiredAt
                    })
            };

            return Ok(result);
        }
        
        [HttpPost("{characterId:int}/backpacks")]
        public async Task<IActionResult> AddBackpackItem(int characterId, ICollection<AddItemReq> addItemsReq)
        {
            if (!await dbService.DoesCharacterExit(characterId))
                return NotFound($"No Character with id: {characterId}");
            
            var character = await dbService.GetCharacterById(characterId);

            var totalWeight = 0;
            
            foreach (var addItemReq in addItemsReq)
            {
                if (!await dbService.DoesItemExist(addItemReq.ItemId))
                    return NotFound($"No Item with id: {addItemReq.ItemId}");
                var item = await dbService.GetItemById(addItemReq.ItemId);
                totalWeight += item.Weight * addItemReq.Amount;
            }

            if (character.CurrentWei + totalWeight > character.MaxWeight)
                return BadRequest("Character can't carry that much weight");

            using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            
            var updatedBackpacks = new List<Backpacks>();

            foreach (var addItemReq in addItemsReq)
            {
                if (await dbService.DoesBackpackExit(characterId, addItemReq.ItemId))
                {
                    var backpack = await dbService.GetBackpack(characterId, addItemReq.ItemId);
                    backpack.Amount += addItemReq.Amount;
                    await dbService.UpdateBackpack(backpack);
                    
                    updatedBackpacks.Add(backpack);
                }
                else
                {
                    var item = await dbService.GetItemById(addItemReq.ItemId);
                    var backpack = new Backpacks()
                    {
                        CharacterId = characterId,
                        ItemId = addItemReq.ItemId,
                        Amount = addItemReq.Amount,
                        Characters = character,
                        Items = item
                    };
                    item.Backpacks.Add(backpack);
                    character.Backpacks.Add(backpack);
                    await dbService.AddBackpackItem(backpack);
                    await dbService.UpdateItem(item);
                    
                    updatedBackpacks.Add(backpack);
                }
            }

            character.CurrentWei += totalWeight;
            await dbService.UpdateCharacter(character);
            
            transaction.Complete();
            
            var result = updatedBackpacks.Select(backpack => new
            {
                amount = backpack.Amount,
                itemId = backpack.ItemId,
                characterId = backpack.CharacterId
            });

            return Created("api/character", result);
        }
        
    }
    
    
}
