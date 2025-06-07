using System.ComponentModel.DataAnnotations;

namespace MasFinal.Models.PoliticalOrganisation;

public enum PoliticalPosition
{
    FarLeft = 0,
    Left = 1,
    CenterLeft = 2,
    Center = 3,
    CenterRight = 4,
    Right = 5,
    FarRight = 6
}

public abstract class PoliticalOrganisation
{
    [Key]
    public int OrganisationId { get; set; }
    
    [Required]
    [StringLength(150)]
    public string Name { get; set; }

    [Required]
    public PoliticalPosition PoliticalAffiliation { get; set; }
    
    public virtual ICollection<Movement> SupportedMovements { get; set; } = new List<Movement>(); 

    public abstract double CalculateInfluence();
}
