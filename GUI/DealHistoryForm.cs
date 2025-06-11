using MasFinal.Models;
using MasFinal.ServiceContracts;
using Microsoft.Extensions.DependencyInjection;

namespace GUI;

public partial class DealHistoryForm : Form
{
    private readonly IProposeDealService _proposeDealService;
    private Person _oligarch;
    private Person _politician;

    public DealHistoryForm(IProposeDealService proposeDealService)
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

    private async void DealHistoryForm_Load(object sender, EventArgs e)
    {
        await LoadDealHistory();
    }
        
    private async Task LoadDealHistory()
    {
        if (_oligarch == null || _politician == null) 
            return;
        
        try
        {
            var deals = await _proposeDealService.GetDealBetweenAsync(
                _oligarch.PersonId, _politician.PersonId);
                
            dealsDataGridView.DataSource = deals.Select(d => new
            {
                Proposed = d.DateProposed,
                d.Description,
                Level = d.DealLevel,
                d.Status,
                Decided = d.DateDecided
            }).ToList();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Failed to load deal history: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void makeNewDealButton_Click(object sender, EventArgs e)
    {
        var newDealForm = Program.ServiceProvider.GetRequiredService<NewDealForm>();
        newDealForm.Initialize(_oligarch, _politician);
            
        // Hide the history form and show the new deal form.
        this.Hide();
        var result = newDealForm.ShowDialog();
            
        // After the new deal form is closed, show this form again and refresh the history.
        this.Show();
        _ = LoadDealHistory(); // Refresh the list
    }

    private void closeButton_Click(object sender, EventArgs e)
    {
        this.Close();
    }
}