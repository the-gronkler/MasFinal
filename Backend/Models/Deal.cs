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
    public int DealId { get; private set; }

    public string? Description { get; private set; }

    [Required]
    public DateTime DateProposed { get; private set; }
    
    public DateTime? DateDecided { get; private set; }

    [Required]
    [Range(1, 5, ErrorMessage = "Deal Level must be between 1 and 5.")]
    public int DealLevel { get; private set; }

    [Required]
    public DealStatus Status { get; private set; }
    
    [Required]
    public int ProposingOligarchId { get; private set; }

    [ForeignKey(nameof(ProposingOligarchId))]
    public virtual Person ProposingOligarch { get; private set; }

    [Required]
    public int ReceivingPoliticianId { get; private set; }

    [ForeignKey(nameof(ReceivingPoliticianId))]
    public virtual Person ReceivingPolitician { get; private set; }

    
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
