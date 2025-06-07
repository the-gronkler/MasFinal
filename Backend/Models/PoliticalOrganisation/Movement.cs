using System.ComponentModel.DataAnnotations;

namespace MasFinal.Models.PoliticalOrganisation;


public class Movement : PoliticalOrganisation
{
    [Required]
    public string MainIssue { get; set; }
    
    [Required]
    public string TargetDemographic { get; set; }

    // Navigation property for the association class
    public virtual ICollection<MovementMembership> Memberships { get; set; } = new List<MovementMembership>();
    
    // Navigation property for supporting organisations
    public virtual ICollection<PoliticalOrganisation> SupportedBy {  get; set; } = new List<PoliticalOrganisation>();

    public override double CalculateInfluence()
    {
        // For example: double baseInfluence = Memberships.Count * 0.5;
        // double supportInfluence = SupportedBy.Sum(org => org.CalculateInfluence());
        // return baseInfluence + supportInfluence;
        throw new NotImplementedException();
    }
}