using MasFinal.Models;
using MasFinal.Models.Businesses;
using MasFinal.Models.PoliticalOrganisation;
using MasFinal.RepositoryContracts;
using MasFinal.RepositoryContracts.Businesses;
using MasFinal.RepositoryContracts.PoliticalOrganisations;

namespace MasFinal.Data
{
    public interface IDataSeeder
    {
        Task SeedAsync();
    }

    public class DataSeeder(
        AppDbContext context, // Inject context for a single SaveChanges call
        IPersonRepository personRepository,
        IDealRepository dealRepository,
        IBillRepository billRepository,
        IPartyRepository partyRepository,
        IMovementRepository movementRepository,
        IBusinessRepository businessRepository,
        IWorkerRepository workerRepository
    ) : IDataSeeder
    {
        public async Task SeedAsync()
        {
            // Only seed if there's no data
            if (await personRepository.GetAllAsync() is { } people && people.Any())
                return;
            

            Console.WriteLine("Database is empty. Seeding sample data...");

            // --- 1. Create Persons ---
            var eleanorVance = new Person
            {
                Name = "Eleanor Vance", Age = 45, InfluenceScore = 8, Types = { PersonType.Politician }
            };
            var maximilianSterling = new Person
            {
                Name = "Maximilian Sterling", Age = 62, Wealth = 600_000_000, Types = { PersonType.Oligarch }
            };
            var isabellaRossi = new Person
            {
                Name = "Isabella Rossi", Age = 53, InfluenceScore = 9, Wealth = 1_200_000_000,
                Types = { PersonType.Politician, PersonType.Oligarch }
            };
            var kenjiTanaka = new Person
            {
                Name = "Kenji Tanaka", Age = 38, InfluenceScore = 6, Types = { PersonType.Politician }
            };
            var sofiaPetrova = new Person
            {
                Name = "Sofia Petrova", Age = 51, InfluenceScore = 7, Types = { PersonType.Politician }
            };
            eleanorVance = await personRepository.AddAsync(eleanorVance);
            maximilianSterling = await personRepository.AddAsync(maximilianSterling);
            isabellaRossi = await personRepository.AddAsync(isabellaRossi);
            kenjiTanaka = await personRepository.AddAsync(kenjiTanaka);
            sofiaPetrova = await personRepository.AddAsync(sofiaPetrova);


            // --- 2. Create Businesses and Workers ---
            var sterlingEnterprises = new Business { Name = "Sterling Enterprises", Owner = maximilianSterling };
            sterlingEnterprises.Workers.Add(new Worker { Position = "CEO", Wage = 350_000 });
            sterlingEnterprises.Workers.Add(new Worker { Name = "John Doe", Position = "Engineer", Wage = 120_000 });
            sterlingEnterprises.Workers.Add(new Worker { Name = "Jane Smith", Position = "Accountant", Wage = 95_000 });
            await businessRepository.AddAsync(sterlingEnterprises);

            var rossiInnovations = new Business { Name = "Rossi Innovations", Owner = isabellaRossi };
            rossiInnovations.Workers.Add(new Worker { Name = "Chen Wei", Position = "Lead Scientist", Wage = 250_000 });
            rossiInnovations.Workers.Add(new Worker { Name = "Emily White", Position = "HR Manager", Wage = 85_000 });
            await businessRepository.AddAsync(rossiInnovations);


            // --- 3. Create Political Organisations and Memberships ---
            // Create a Party (must have at least one member on creation)
            var centristAlliance = new Party
            {
                Name = "The Centrist Alliance",
                PoliticalAffiliation = PoliticalPosition.Center,
                NumSeatsInParliament = 45,
                IsParticipatingInElection = true,
                PrimaryColors = { "Blue", "Gold" }
            };
            centristAlliance.Memberships.Add(new PartyMembership
            {
                Politician = eleanorVance, Position = PartyPosition.Leader, StartDate = DateTime.UtcNow.AddYears(-5)
            });
            await partyRepository.AddAsync(centristAlliance);

            // Create a Movement
            var greenMovement = new Movement
            {
                Name = "Green Future Initiative",
                PoliticalAffiliation = PoliticalPosition.CenterLeft,
                MainIssue = "Climate Change",
                TargetDemographic = "Young Urban Professionals"
            };
            await movementRepository.AddAsync(greenMovement);


            // --- Must save here to get Organisation IDs before adding more members/relations ---
            await context.SaveChangesAsync();


            // --- Add more members and relations now that IDs exist ---
            await partyRepository.AddPartyMemberAsync(centristAlliance.OrganisationId, kenjiTanaka.PersonId,
                PartyPosition.Spokesperson);

            await movementRepository.AddMovementMemberAsync(greenMovement.OrganisationId, eleanorVance.PersonId, false);
            await movementRepository.AddMovementMemberAsync(greenMovement.OrganisationId, sofiaPetrova.PersonId, true);

            // Add a supporting organization
            await movementRepository.AddSupportingOrganisationAsync(greenMovement.OrganisationId,
                centristAlliance.OrganisationId);


            // --- 4. Create Bills ---
            var cleanAirAct = new Bill
            {
                Name = "Clean Air Act 2.0",
                Description = "A bill to tighten regulations on industrial emissions.",
                Status = BillStatus.Proposed,
                Proposer = sofiaPetrova
            };
            await billRepository.AddAsync(cleanAirAct);

            // --- Must save here to get Bill ID before adding supporters/opposers ---
            await context.SaveChangesAsync();

            await billRepository.SupportBillAsync(cleanAirAct.BillId, eleanorVance.PersonId);
            await billRepository.SupportBillAsync(cleanAirAct.BillId, isabellaRossi.PersonId);
            await billRepository.OpposeBillAsync(cleanAirAct.BillId, kenjiTanaka.PersonId);


            // --- 5. Create Deals ---
            var deal1 = new Deal
            {
                Proposer = maximilianSterling, Recipient = eleanorVance, DealLevel = 2,
                Description = "Support for deregulation in exchange for campaign funding.",
                DateProposed = DateTime.UtcNow.AddMonths(-6), DateDecided = DateTime.UtcNow.AddMonths(-5),
                Status = DealStatus.Accepted
            };
            var deal2 = new Deal
            {
                Proposer = isabellaRossi, Recipient = kenjiTanaka, DealLevel = 4,
                Description = "Minor zoning variance request.",
                DateProposed = DateTime.UtcNow.AddMonths(-2), DateDecided = DateTime.UtcNow.AddMonths(-2),
                Status = DealStatus.AutoRejected
            };
            var deal3 = new Deal
            {
                Proposer = maximilianSterling, Recipient = isabellaRossi, DealLevel = 1,
                Description = "Major government contract steering.",
                DateProposed = DateTime.UtcNow.AddDays(-10),
                Status = DealStatus.PendingDecision
            };
            await dealRepository.AddAsync(deal1);
            await dealRepository.AddAsync(deal2);
            await dealRepository.AddAsync(deal3);


            // --- 6. Final Save ---
            // This single call will commit all the changes made in this session to the database.
            await context.SaveChangesAsync();
            Console.WriteLine("Sample data has been seeded successfully.");
        }
    }
}