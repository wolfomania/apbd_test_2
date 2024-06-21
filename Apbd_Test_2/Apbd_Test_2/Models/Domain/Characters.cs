using System.ComponentModel.DataAnnotations;
using Apbd_Test_2.Models.Domain;

namespace Apbd_Test_2.Models.Domain;

public class Characters
{
    [Key]
    public int Id { get; set; }

    [MaxLength(50)]
    public string? FirstName { get; set; }
    
    [MaxLength(120)]
    public string? LastName { get; set; }

    public int CurrentWei { get; set; }

    public int MaxWeight { get; set; }

    public ICollection<Backpacks> Backpacks { get; set; } = new HashSet<Backpacks>();

    public ICollection<CharacterTitles> CharacterTitles { get; set; } = new HashSet<CharacterTitles>();
}
