using MasFinal.Models;
using MasFinal.ServiceContracts;
using System.Text;

namespace GUI;

public partial class PersonDealsDetailForm : Form
{
    private readonly IProposeDealService _proposeDealService;
    private Person _person;

    public PersonDealsDetailForm(IProposeDealService proposeDealService)
    {
        _proposeDealService = proposeDealService;
        InitializeComponent();
    }

    public void Initialize(Person person)
    {
        _person = person;
    }

    private async void PersonDealsDetailForm_Load(object sender, EventArgs e)
    {
        if (_person == null)
        {
            MessageBox.Show("No person selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            this.Close();
            return;
        }

        // Populate Person Details
        nameLabel.Text = $"Name: {_person.Name}";
        ageLabel.Text = $"Age: {_person.Age}";
        rolesLabel.Text = $"Roles: {string.Join(", ", _person.Types)}";

        var roleDetails = new StringBuilder();
        if (_person.IsPolitician())
        {
            roleDetails.AppendLine($"Influence Score: {_person.InfluenceScore}");
        }
        if (_person.IsOligarch())
        {
            roleDetails.AppendLine($"Wealth: {_person.Wealth:C}");
        }
        roleSpecificLabel.Text = roleDetails.ToString();

        // Populate Deals
        try
        {
            var deals = await _proposeDealService.GetAllDealsFor(_person.PersonId);
            dealsDataGridView.DataSource = deals.Select(d => new
            {
                Proposer = d.Proposer.Name,
                Recipient = d.Recipient.Name,
                d.Description,
                d.Status,
                Level = d.DealLevel,
                Proposed = d.DateProposed
            }).ToList();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Failed to load deals: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void closeButton_Click(object sender, EventArgs e)
    {
        this.Close();
    }
}