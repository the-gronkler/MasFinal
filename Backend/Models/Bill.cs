using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasFinal.Models;

public enum BillStatus
{
    Proposed,
    Passed,
    Rejected
}

public class Bill
{
    [Key]
    public int BillId { get; set; }

    [Required]
    [StringLength(200)]
    public string Name { get; set; }
    
    public string? Description { get; set; }

    [Required]
    public BillStatus Status { get; set; }
    
    public int? ProposerId { get; set; }
    [ForeignKey("ProposerId")]
    
    public virtual Person? Proposer { get; set; }
    public virtual ICollection<Person> Supporters { get; set; } = new List<Person>();
    public virtual ICollection<Person> Opposers { get; set; } = new List<Person>();
}
