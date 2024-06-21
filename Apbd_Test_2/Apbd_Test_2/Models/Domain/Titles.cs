using System.ComponentModel.DataAnnotations;

namespace Apbd_Test_2.Models.Domain;


public class Titles
{
    [Key]
    public int Id { get; set; }

    [MaxLength(100)]
    public string? Name { get; set; }
    
    public ICollection<CharacterTitles> CharacterTitles { get; set; } = new HashSet<CharacterTitles>();
}