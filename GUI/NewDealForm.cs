using MasFinal.Models;
using MasFinal.ServiceContracts;
using Microsoft.Extensions.DependencyInjection;

namespace GUI;

public partial class NewDealForm : Form
{
    private readonly IProposeDealService _proposeDealService;
    private Person _oligarch;
    private Person _politician;

    public NewDealForm(IProposeDealService proposeDealService)
    {
        _proposeDealService = proposeDealService;
        InitializeComponent();
    }

    public void Initialize(Person oligarch, Person politician)
    {
        _oligarch = oligarch;
        _politician = politician;
        oligarchLabel.Text = $"Oligarch: {_oligarch.Name}";
        politicianLabel.Text = $"Politician: {_politician.Name}";
    }

    private async void submitDealButton_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(descriptionTextBox.Text))
        {
            MessageBox.Show("Deal description is required.", "Validation Error", MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return;
        }

        try
        {
            var newDeal = await _proposeDealService.TryProposeDealAsync(
                _oligarch.PersonId,
                _politician.PersonId,
                descriptionTextBox.Text,
                (int)dealLevelUpDown.Value
            );

            if (newDeal.Status == DealStatus.PendingDecision)
            {
                MessageBox.Show("Deal proposed successfully and is pending the politician's decision.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else if (newDeal.Status == DealStatus.PreScreening)
            {
                MessageBox.Show(
                    "Initial eligibility check failed. Please provide proof of influence by selecting up to 3 politicians you have successfully dealt with before.",
                    "Proof Required", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Open the proof form
                var proofForm = Program.ServiceProvider.GetRequiredService<ProveEligibilityForm>();
                proofForm.Initialize(newDeal.DealId, _oligarch.PersonId);
                proofForm.ShowDialog();
                this.Close(); // Close the new deal form after the proof form is handled
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Failed to propose deal: {ex.Message}", "Error", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    private void cancelButton_Click(object sender, EventArgs e)
    {
        this.Close();
    }
}