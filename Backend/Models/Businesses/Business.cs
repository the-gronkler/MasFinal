using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasFinal.Models.Businesses;

public class Business
{
    [Key]
    public int BusinessId { get; set; }

    [Required(ErrorMessage = "Business name is required.")]
    [StringLength(150, MinimumLength = 1, ErrorMessage = "Business name must be between 1 and 150 characters.")]
    public string Name { get; set; }

    [NotMapped]
    public double AverageWage => Workers.Count != 0
        ? Workers.Average(worker => worker.Wage) 
        : 0;
    
    [Required]
    public int OwnerId { get; set; }
    [ForeignKey("OwnerId")]
    public virtual Person Owner { get; set; }
    
    public virtual ICollection<Worker> Workers { get; set; } = new List<Worker>();
}



