using MasFinal.Models;
using MasFinal.ServiceContracts;

namespace GUI;

public partial class ProveEligibilityForm : Form
{
    private readonly IProposeDealService _proposeDealService;
    private int _dealId;
    private int _oligarchId;

    public ProveEligibilityForm(IProposeDealService proposeDealService)
    {
        _proposeDealService = proposeDealService;
        InitializeComponent();
    }

    public void Initialize(int dealId, int oligarchId)
    {
        _dealId = dealId;
        _oligarchId = oligarchId;
    }

    private async void ProveEligibilityForm_Load(object sender, EventArgs e)
    {
        try
        {
            var politicians = await _proposeDealService.GetPreviouslyDealtPoliticiansAsync(_oligarchId);
            politiciansCheckedListBox.DataSource = politicians.ToList();
            politiciansCheckedListBox.DisplayMember = "Name";
            politiciansCheckedListBox.ValueMember = "PersonId";
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Failed to load politicians: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            this.Close();
        }
    }

    private async void submitProofButton_Click(object sender, EventArgs e)
    {
        var selectedPoliticians = politiciansCheckedListBox.CheckedItems.Cast<Person>().ToList();

        if (selectedPoliticians.Count == 0 || selectedPoliticians.Count > 3)
        {
            MessageBox.Show("Please select between 1 and 3 politicians.", "Invalid Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var selectedIds = selectedPoliticians.Select(p => p.PersonId).ToList();

        try
        {
            var updatedDeal = await _proposeDealService.ProveDealEligibilityAsync(_dealId, selectedIds);

            if (updatedDeal.Status == DealStatus.PendingDecision)
            {
                MessageBox.Show("Eligibility proven! The deal is now pending the politician's decision.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else // AutoRejected
            {
                MessageBox.Show("Eligibility could not be proven with the selected politicians. The deal has been auto-rejected.", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            this.Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"An error occurred while submitting proof: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
