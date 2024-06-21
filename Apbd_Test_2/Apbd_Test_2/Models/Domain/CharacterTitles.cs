using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Apbd_Test_2.Models.Domain;


[Table("character_titles")]
[PrimaryKey(nameof(CharacterId), nameof(TitleId))]
public class CharacterTitles
{
    public int CharacterId { get; set; }
    public int TitleId { get; set; }

    public DateTime AcquiredAt { get; set; }

    [ForeignKey(nameof(CharacterId))] 
    public Characters Characters { get; set; } = null!;
    
    [ForeignKey(nameof(TitleId))] 
    public Titles Titles { get; set; } = null!;
}