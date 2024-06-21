
using Apbd_Test_2.Models.Domain;

namespace Apbd_Test_2.Services;

public interface IDbService
{
    Task<bool> DoesCharacterExit(int characterId);

    Task<Characters> GetCharacterById(int characterId);
}