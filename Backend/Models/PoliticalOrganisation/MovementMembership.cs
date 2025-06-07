using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasFinal.Models.PoliticalOrganisation;

public class MovementMembership
{
    [Key]
    public int MembershipId { get; set; }

    [Required]
    public DateTime StartDate { get; set; }
    
    public DateTime? EndDate { get; set; } 
    
    public bool IsLeader { get; set; }

    [Required]
    public int PoliticianId { get; set; }
    [ForeignKey("PoliticianId")]
    public virtual Person Politician { get; set; }

    [Required]
    public int MovementId { get; set; }
    [ForeignKey("MovementId")]
    public virtual Movement Movement { get; set; }
}