using System.ComponentModel.DataAnnotations;

namespace Apbd_Test_2.Models.Domain;


public class Items
{
    [Key]
    public int Id { get; set; }

    [MaxLength(100)]
    public string? Name { get; set; }
    
    public int Weight { get; set; }

    public ICollection<Backpacks> Backpacks { get; set; } = new HashSet<Backpacks>();

}