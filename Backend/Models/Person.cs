using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MasFinal.Models.Businesses;
using MasFinal.Models.PoliticalOrganisation;


namespace MasFinal.Models;

public enum PersonType
{
    Politician,
    Oligarch
}

public class Person
{
    // // Public constructor with required fields
    // public Person(string name, int age, IEnumerable<PersonType> types)
    // {
    //     var personTypes = types.ToHashSet();
    //     if (types == null || personTypes.Count == 0)
    //         throw new ArgumentException("At least one person type must be specified.", nameof(types));
    //     
    //     Name = name ?? throw new ArgumentNullException(nameof(name));
    //     Age = age;
    //     Types = personTypes;
    // }
    //
    // // Parameterless constructor for EF Core
    // private Person() { }
    
    
    
    // // <--- Common Properties --->
    
    [Key]
    public int PersonId { get; set; }
    
    [Required]
    [StringLength(100)]
    public string Name { get; set; }
    
    [Required]
    [Range(18, 120)]
    public int Age { get; set; }
    
    public ICollection<PersonType> Types { get; set; } = new HashSet<PersonType>();


    // <--- Politician --->

    [Range(1, 10, ErrorMessage = "InfluenceScore must be between 1 and 10.")]
    public int? InfluenceScore { get; set; }

    public virtual ICollection<Bill> BillsProposed { get; set; } = new List<Bill>();
    public virtual ICollection<Bill> BillsSupported { get; set; } = new List<Bill>();
    public virtual ICollection<Bill> BillsOpposed { get; set; } = new List<Bill>();

    public virtual ICollection<PartyMembership> PartyMemberships { get; set; } = new List<PartyMembership>();
    public virtual ICollection<MovementMembership> MovementMemberships { get; set; } = new List<MovementMembership>();

    [InverseProperty("Proposer")]
    public virtual ICollection<Deal> DealsProposed { get; set; } = new List<Deal>();

    // <--- Oligarch --->

    public double? Wealth { get; set; }

    public virtual ICollection<Business> OwnedBusinesses { get; set; } = new List<Business>();

    [InverseProperty("Recipient")]
    public virtual ICollection<Deal> DealsReceived{ get; set; } = new List<Deal>();
    
    // helper methods
    public bool IsPolitician() => Types.Contains(PersonType.Politician);
    public bool IsOligarch() => Types.Contains(PersonType.Oligarch);
}
