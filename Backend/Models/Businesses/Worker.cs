using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasFinal.Models.Businesses;

/// <summary>
/// Represents a worker employed by a Business.
/// </summary>
public class Worker
{
    [Key]
    public int WorkerId { get; set; }

    public string? Name { get; set; }
    
    [Required]
    public string Position { get; set; }

    [Required]
    public double Wage { get; set; }
    public static double MinimumWage { get; set; } = 15.00; 


    [Required]
    public int BusinessId { get; set; }
    [ForeignKey("BusinessId")]
    public virtual Businesses.Business Business { get; set; }
}