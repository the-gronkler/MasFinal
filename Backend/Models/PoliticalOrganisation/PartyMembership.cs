using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasFinal.Models.PoliticalOrganisation;

public enum PartyPosition
{
    Leader,
    Spokesperson,
    Treasurer,
    Member
}


public class PartyMembership
{
    [Key]
    public int MembershipId { get; set; }
    
    [Required]
    public DateTime StartDate { get; set; }
    
    public DateTime? EndDate { get; set; } 
    
    [Required]
    public PartyPosition Position { get; set; }
    
    [Required]
    public int PoliticianId { get; set; }
    [ForeignKey("PoliticianId")]
    public virtual Person Politician { get; set; }

    [Required]
    public int PartyId { get; set; }
    [ForeignKey("PartyId")]
    public virtual Party Party { get; set; }
}
