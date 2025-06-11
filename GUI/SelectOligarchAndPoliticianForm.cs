using MasFinal.Models;
using MasFinal.ServiceContracts;
using Microsoft.Extensions.DependencyInjection;

namespace GUI;

public partial class SelectOligarchAndPoliticianForm : Form
{
    private readonly IProposeDealService _proposeDealService;

    public SelectOligarchAndPoliticianForm(IProposeDealService proposeDealService)
    {
        _proposeDealService = proposeDealService;
        InitializeComponent();
    }

    private async void DealSelectorForm_Load(object sender, EventArgs e)
    {
        try
        {
            // Load Oligarchs
            var oligarchs = await _proposeDealService.GetOligarchsAsync();
            oligarchsListBox.DataSource = oligarchs.ToList();
            oligarchsListBox.DisplayMember = "Name";
            oligarchsListBox.ValueMember = "PersonId";

            // Load Politicians
            var politicians = await _proposeDealService.GetPoliticiansAsync();
            politiciansListBox.DataSource = politicians.ToList();
            politiciansListBox.DisplayMember = "Name";
            politiciansListBox.ValueMember = "PersonId";
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Failed to load data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void viewDealsButton_Click(object sender, EventArgs e)
    {
        var selectedOligarch = (Person?) oligarchsListBox.SelectedItem;
        var selectedPolitician = (Person?) politiciansListBox.SelectedItem;

        if (selectedOligarch == null || selectedPolitician == null)
        {
            MessageBox.Show("Please select both an oligarch and a politician.", "Selection Required",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (selectedOligarch.PersonId == selectedPolitician.PersonId)
        {
            MessageBox.Show("An oligarch cannot make a deal with themselves.", "Invalid Selection",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        
        var dealHistoryForm = Program.ServiceProvider.GetRequiredService<DealHistoryForm>();
        dealHistoryForm.Initialize(selectedOligarch, selectedPolitician);
        dealHistoryForm.ShowDialog(); 
    }
}