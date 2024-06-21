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
                    .Select(backpacks => backpacks.Items)
                    .GroupBy(items => items.Id)
                    .Select(group => new
                    {
                        itemName = group.First().Name,
                        itemWeight = group.First().Weight,
                        amount = group.Count()
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
        
    }
    
    
}
