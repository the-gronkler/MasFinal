using System.ComponentModel.DataAnnotations;

namespace MasFinal.Models.PoliticalOrganisation;

public class Party : PoliticalOrganisation
{
    [Required] public int NumSeatsInParliament { get; set; }

    public bool IsParticipatingInElection { get; set; }

    public ICollection<string> PrimaryColors { get; set; } = new List<string>();


    public virtual ICollection<PartyMembership> Memberships { get; set; } = new List<PartyMembership>();

    public override double CalculateInfluence()
    {
        throw new NotImplementedException();
    }
}