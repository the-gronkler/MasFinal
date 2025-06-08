using System.ComponentModel.DataAnnotations;

namespace MasFinal.Models;

public class StaticAttribute
{
    [Key]
    [StringLength(100)]
    public string Key { get; set; }

    [Required]
    [StringLength(255)]
    public string Value { get; set; }
}