using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasFinal.Models;

public enum DealStatus
{
    PreScreening,
    PendingDecision,
    AutoRejected,
    Declined,
    Accepted
}

public class Deal
{

    [Key]
    public int DealId { get; set; }

    public string? Description { get; set; }

    [Required]
    public DateTime DateProposed { get; set; }
    
    public DateTime? DateDecided { get; set; }

    [Required]
    [Range(1, 5, ErrorMessage = "Deal Level must be between 1 and 5.")]
    public int DealLevel { get; set; }

    [Required]
    public DealStatus Status { get; set; }
    
    [Required]
    public int ProposerId { get; set; }
    [ForeignKey(nameof(ProposerId))]
    public virtual Person Proposer { get; set; }

    [Required]
    public int RecipientId { get; set; }
    [ForeignKey(nameof(RecipientId))]
    public virtual Person Recipient { get; set; }

    
    // --- State transition methods ---
    public void PreApprove()
    {
        if (Status != DealStatus.PreScreening)
            throw new InvalidOperationException("Deal must be in 'PreScreening' to pre-approve.");

        Status = DealStatus.PendingDecision;
    }

    public void AutoReject()
    {
        if (Status != DealStatus.PreScreening)
            throw new InvalidOperationException("Deal must be in 'PreScreening' to auto-reject.");

        Status = DealStatus.AutoRejected;
    }

    public void Accept()
    {
        if (Status != DealStatus.PendingDecision && Status != DealStatus.AutoRejected)
            throw new InvalidOperationException("Deal must be in 'PendingDecision' or 'AutoRejected' to accept.");

        Status = DealStatus.Accepted;
    }

    public void Decline()
    {
        if (Status != DealStatus.PendingDecision)
            throw new InvalidOperationException("Deal must be in 'PendingDecision' to decline.");

        Status = DealStatus.Declined;
    }
}
