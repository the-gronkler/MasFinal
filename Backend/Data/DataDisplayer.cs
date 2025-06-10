// --- C:\ProgProjects\MasFinal\Backend\Data\DataDisplayer.cs ---
using MasFinal.Models;
using MasFinal.RepositoryContracts;
using MasFinal.RepositoryContracts.Businesses;
using MasFinal.RepositoryContracts.PoliticalOrganisations;
using Microsoft.EntityFrameworkCore;

namespace MasFinal.Data
{
    public interface IDataDisplayer
    {
        Task DisplayAsync();
    }

    public class DataDisplayer(AppDbContext context) : IDataDisplayer
    {
        public async Task DisplayAsync()
        {
            Console.WriteLine("\n\n<================= SYSTEM DATA DISPLAY =================>");

            await DisplayPersonsAndBusinessesAsync();
            await DisplayPoliticalOrganisationsAsync();
            await DisplayBillsAsync();
            await DisplayDealsAsync();

            Console.WriteLine("\n<================= END OF SYSTEM DATA ==================>");
        }

        private async Task DisplayPersonsAndBusinessesAsync()
        {
            Console.WriteLine("\n\n--- Persons and Businesses ---");
            var persons = await context.Persons
                .Include(p => p.OwnedBusinesses)
                .ThenInclude(b => b.Workers)
                .ToListAsync();

            if (!persons.Any())
            {
                Console.WriteLine("No persons found in the system.");
                return;
            }

            foreach (var person in persons)
            {
                var roles = string.Join(", ", person.Types);
                Console.WriteLine($"\n[ID: {person.PersonId}] {person.Name}, Age: {person.Age}, Roles: [{roles}]");

                if (person.IsPolitician())
                {
                    Console.WriteLine($"  - Politician Influence: {person.InfluenceScore}");
                }
                if (person.IsOligarch())
                {
                    Console.WriteLine($"  - Oligarch Wealth: {person.Wealth:C}");
                }

                if (person.OwnedBusinesses.Any())
                {
                    Console.WriteLine("  - Owned Businesses:");
                    foreach (var business in person.OwnedBusinesses)
                    {
                        Console.WriteLine($"    - {business.Name} (Avg. Wage: {business.AverageWage:C})");
                        if (business.Workers.Any())
                        {
                            Console.WriteLine("      - Workers:");
                            foreach (var worker in business.Workers)
                            {
                                Console.WriteLine($"        - {worker.Name ?? "Unnamed"}, Position: {worker.Position}, Wage: {worker.Wage:C}");
                            }
                        }
                    }
                }
            }
        }

        private async Task DisplayPoliticalOrganisationsAsync()
        {
            Console.WriteLine("\n\n--- Political Organisations ---");

            // Display Parties
            var parties = await context.Parties
                .Include(p => p.Memberships)
                .ThenInclude(m => m.Politician)
                .ToListAsync();

            Console.WriteLine("\n[Parties]");
            if (!parties.Any()) Console.WriteLine("No parties found.");
            foreach (var party in parties)
            {
                Console.WriteLine($"\n{party.Name} ({party.PoliticalAffiliation})");
                Console.WriteLine($"  - Seats: {party.NumSeatsInParliament}, In Election: {party.IsParticipatingInElection}");
                Console.WriteLine("  - Memberships:");
                foreach (var membership in party.Memberships)
                {
                    var status = membership.EndDate.HasValue ? $"Ended: {membership.EndDate.Value:d}" : "Active";
                    Console.WriteLine($"    - {membership.Politician.Name} as {membership.Position} (Started: {membership.StartDate:d}, Status: {status})");
                }
            }

            // Display Movements
            var movements = await context.Movements
                .Include(m => m.Memberships)
                .ThenInclude(mm => mm.Politician)
                .Include(m => m.SupportedBy)
                .ToListAsync();

            Console.WriteLine("\n[Movements]");
            if (!movements.Any()) Console.WriteLine("No movements found.");
            foreach (var movement in movements)
            {
                Console.WriteLine($"\n{movement.Name} ({movement.PoliticalAffiliation})");
                Console.WriteLine($"  - Main Issue: {movement.MainIssue}, Target: {movement.TargetDemographic}");
                Console.WriteLine("  - Memberships:");
                foreach (var membership in movement.Memberships)
                {
                    var role = membership.IsLeader ? "Leader" : "Supporter";
                    var status = membership.EndDate.HasValue ? $"Ended: {membership.EndDate.Value:d}" : "Active";
                    Console.WriteLine($"    - {membership.Politician.Name} as {role} (Started: {membership.StartDate:d}, Status: {status})");
                }
                if (movement.SupportedBy.Any())
                {
                    Console.WriteLine("  - Supported By:");
                    foreach (var org in movement.SupportedBy)
                    {
                        Console.WriteLine($"    - {org.Name}");
                    }
                }
            }
        }

        private async Task DisplayBillsAsync()
        {
            Console.WriteLine("\n\n--- Bills ---");
            var bills = await context.Bills
                .Include(b => b.Proposer)
                .Include(b => b.Supporters)
                .Include(b => b.Opposers)
                .ToListAsync();

            if (!bills.Any())
            {
                Console.WriteLine("No bills found in the system.");
                return;
            }

            foreach (var bill in bills)
            {
                Console.WriteLine($"\n[Bill ID: {bill.BillId}] {bill.Name}");
                Console.WriteLine($"  - Status: {bill.Status}, Proposed by: {bill.Proposer?.Name ?? "N/A (Proposer Quit)"}");
                Console.WriteLine($"  - Supporters ({bill.Supporters.Count}): {string.Join(", ", bill.Supporters.Select(s => s.Name))}");
                Console.WriteLine($"  - Opposers ({bill.Opposers.Count}): {string.Join(", ", bill.Opposers.Select(o => o.Name))}");
            }
        }
        
        private async Task DisplayDealsAsync()
        {
            Console.WriteLine("\n\n--- Deals ---");
            var deals = await context.Deals
                .Include(d => d.Proposer)
                .Include(d => d.Recipient)
                .OrderBy(d => d.DateProposed)
                .ToListAsync();

            if (!deals.Any())
            {
                Console.WriteLine("No deals found in the system.");
                return;
            }

            foreach (var deal in deals)
            {
                Console.WriteLine($"\n[Deal ID: {deal.DealId}] from {deal.Proposer.Name} to {deal.Recipient.Name}");
                Console.WriteLine($"  - Status: {deal.Status}, Level: {deal.DealLevel}");
                Console.WriteLine($"  - Proposed: {deal.DateProposed:g}");
                if(deal.DateDecided.HasValue) Console.WriteLine($"  - Decided: {deal.DateDecided.Value:g}");
                Console.WriteLine($"  - Description: {deal.Description}");
            }
        }
    }
}