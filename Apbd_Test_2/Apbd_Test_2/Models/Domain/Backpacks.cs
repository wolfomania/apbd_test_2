using System.ComponentModel.DataAnnotations.Schema;

namespace Apbd_Test_2.Models.Domain;

public class Backpacks
{
    public int CharacterId { get; set; }
    public int ItemId { get; set; }

    public int Amount { get; set; }

    [ForeignKey(nameof(CharacterId))] 
    public Characters Characters { get; set; } = null!;

    [ForeignKey(nameof(ItemId))] 
    public Items Items { get; set; } = null!;
}