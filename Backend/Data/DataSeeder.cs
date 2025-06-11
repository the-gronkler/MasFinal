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
        Task SeedIfEmptyAsync();
    }

    public class DataSeeder(
        AppDbContext context,
        IPersonRepository personRepository,
        IDealRepository dealRepository,
        IBillRepository billRepository,
        IPartyRepository partyRepository,
        IMovementRepository movementRepository,
        IBusinessRepository businessRepository,
        IWorkerRepository workerRepository
    ) : IDataSeeder
    {
        public async Task SeedIfEmptyAsync()
        {
            if (await personRepository.GetAllAsync() is { } people && people.Any())
                return;

            Console.WriteLine("Database is empty. Seeding sample data...");

            // --- 1. Create Persons ---
            var eleanorVance = new Person
                { Name = "Eleanor Vance", Age = 45, InfluenceScore = 8, Types = { PersonType.Politician } };
            var maximilianSterling = new Person
                { Name = "Maximilian Sterling", Age = 62, Wealth = 600_000_000, Types = { PersonType.Oligarch } };
            var isabellaRossi = new Person
            {
                Name = "Isabella Rossi", Age = 53, InfluenceScore = 9, Wealth = 1_200_000_000,
                Types = { PersonType.Politician, PersonType.Oligarch }
            };
            var kenjiTanaka = new Person
                { Name = "Kenji Tanaka", Age = 38, InfluenceScore = 6, Types = { PersonType.Politician } };
            var sofiaPetrova = new Person
                { Name = "Sofia Petrova", Age = 51, InfluenceScore = 7, Types = { PersonType.Politician } };
            var arthurPendleton = new Person
                { Name = "Arthur Pendleton", Age = 71, Wealth = 2_500_000_000, Types = { PersonType.Oligarch } };
            var helenaMarkov = new Person
                { Name = "Helena Markov", Age = 49, InfluenceScore = 5, Types = { PersonType.Politician } };
            
            // --- TEST CHARACTERS ---
            var cheapBrokovich = new Person { Name = "Cheap Brokovich", Age = 55, Wealth = 500_000, Types = { PersonType.Oligarch } };
            var senatorArmstrong = new Person { Name = "Senator Armstrong", Age = 50, InfluenceScore = 10, Types = { PersonType.Politician } };

            await personRepository.AddAsync(eleanorVance);
            await personRepository.AddAsync(maximilianSterling);
            await personRepository.AddAsync(isabellaRossi);
            await personRepository.AddAsync(kenjiTanaka);
            await personRepository.AddAsync(sofiaPetrova);
            await personRepository.AddAsync(arthurPendleton);
            await personRepository.AddAsync(helenaMarkov);
            
            await personRepository.AddAsync(cheapBrokovich);
            await personRepository.AddAsync(senatorArmstrong);

            await context.SaveChangesAsync();

            // --- Businesses and Workers ---
            await businessRepository.AddAsync(new Business
                { Name = "Sterling Enterprises", OwnerId = maximilianSterling.PersonId });
            await businessRepository.AddAsync(new Business
                { Name = "Rossi Innovations", OwnerId = isabellaRossi.PersonId });
            await businessRepository.AddAsync(new Business
                { Name = "Pendleton Global", OwnerId = arthurPendleton.PersonId });
            
            var brokovichBiz = new Business { Name = "Brokovich Budget Imports", OwnerId = cheapBrokovich.PersonId };
            brokovichBiz.Workers.Add(new Worker { Name = "Ivan", Position = "Driver", Wage = 75_000});
            brokovichBiz.Workers.Add(new Worker { Name = "Boris", Position = "Accountant", Wage = 75_000});
            await businessRepository.AddAsync(brokovichBiz);

            // --- Political Organisations and Memberships ---
            var centristAlliance = new Party
            {
                Name = "The Centrist Alliance", PoliticalAffiliation = PoliticalPosition.Center,
                NumSeatsInParliament = 45
            };
            centristAlliance.Memberships.Add(new PartyMembership
            {
                PoliticianId = eleanorVance.PersonId, Position = PartyPosition.Leader,
                StartDate = DateTime.UtcNow.AddYears(-5)
            });
            await partyRepository.AddAsync(centristAlliance );

            var greenMovement = new Movement
            {
                Name = "Green Future Initiative", PoliticalAffiliation = PoliticalPosition.CenterLeft,
                MainIssue = "Climate Change", TargetDemographic = "Environmentally Conscious Citizens"
            };
            await movementRepository.AddAsync(greenMovement);
            
            
            await context.SaveChangesAsync(); 
            
            await partyRepository.AddPartyMemberAsync(centristAlliance.OrganisationId, senatorArmstrong.PersonId, PartyPosition.Member);

            await partyRepository.AddPartyMemberAsync(centristAlliance.OrganisationId, kenjiTanaka.PersonId,
                PartyPosition.Spokesperson);
            await movementRepository.AddMovementMemberAsync(greenMovement.OrganisationId, eleanorVance.PersonId, false);
            await movementRepository.AddMovementMemberAsync(greenMovement.OrganisationId, sofiaPetrova.PersonId, true);
            await movementRepository.AddSupportingOrganisationAsync(greenMovement.OrganisationId,
                centristAlliance.OrganisationId);

            // --- 4. Create Bills ---
            var cleanAirAct = new Bill
                { Name = "Clean Air Act 2.0", Status = BillStatus.Proposed, ProposerId = sofiaPetrova.PersonId };
            await billRepository.AddAsync(cleanAirAct);

            await context.SaveChangesAsync();

            await billRepository.SupportBillAsync(cleanAirAct.BillId, eleanorVance.PersonId);
            await billRepository.SupportBillAsync(cleanAirAct.BillId, isabellaRossi.PersonId);
            await billRepository.OpposeBillAsync(cleanAirAct.BillId, kenjiTanaka.PersonId);
            await billRepository.SupportBillAsync(cleanAirAct.BillId, senatorArmstrong.PersonId); 

            // --  Deals ---

            await dealRepository.AddAsync(new Deal
            {
                ProposerId = maximilianSterling.PersonId, RecipientId = eleanorVance.PersonId, DealLevel = 2,
                Description = "Support for deregulation.", Status = DealStatus.Accepted,
                DateProposed = DateTime.UtcNow.AddMonths(-6), DateDecided = DateTime.UtcNow.AddMonths(-5)
            });
            await dealRepository.AddAsync(new Deal
            {
                ProposerId = isabellaRossi.PersonId, RecipientId = kenjiTanaka.PersonId, DealLevel = 4,
                Description = "Minor zoning variance request.", Status = DealStatus.AutoRejected,
                DateProposed = DateTime.UtcNow.AddMonths(-2), DateDecided = DateTime.UtcNow.AddMonths(-2)
            });
            await dealRepository.AddAsync(new Deal
            {
                ProposerId = maximilianSterling.PersonId, RecipientId = isabellaRossi.PersonId, DealLevel = 1,
                Description = "Major government contract steering.", Status = DealStatus.PendingDecision,
                DateProposed = DateTime.UtcNow.AddDays(-10)
            });
            await dealRepository.AddAsync(new Deal
            {
                ProposerId = arthurPendleton.PersonId, RecipientId = helenaMarkov.PersonId, DealLevel = 3,
                Description = "Infrastructure project approval.", Status = DealStatus.Declined,
                DateProposed = DateTime.UtcNow.AddDays(-30), DateDecided = DateTime.UtcNow.AddDays(-28)
            });
            await dealRepository.AddAsync(new Deal
            {
                ProposerId = arthurPendleton.PersonId, RecipientId = eleanorVance.PersonId, DealLevel = 2,
                Description = "Favorable tax legislation.", Status = DealStatus.Accepted,
                DateProposed = DateTime.UtcNow.AddDays(-25), DateDecided = DateTime.UtcNow.AddDays(-20)
            });
            await dealRepository.AddAsync(new Deal
            {
                ProposerId = isabellaRossi.PersonId, RecipientId = helenaMarkov.PersonId, DealLevel = 5,
                Description = "Dinner meeting to discuss 'cooperation'.", Status = DealStatus.PendingDecision,
                DateProposed = DateTime.UtcNow.AddDays(-2)
            });
            await dealRepository.AddAsync(new Deal
            {
                ProposerId = maximilianSterling.PersonId, RecipientId = sofiaPetrova.PersonId, DealLevel = 3,
                Description = "Delaying environmental report.", Status = DealStatus.Accepted,
                DateProposed = DateTime.UtcNow.AddMonths(-4), DateDecided = DateTime.UtcNow.AddMonths(-4).AddDays(5)
            });
            await dealRepository.AddAsync(new Deal
            {
                ProposerId = arthurPendleton.PersonId, RecipientId = kenjiTanaka.PersonId, DealLevel = 2,
                Description = "Blocking a competitor's import license.", Status = DealStatus.Declined,
                DateProposed = DateTime.UtcNow.AddMonths(-3), DateDecided = DateTime.UtcNow.AddMonths(-3).AddDays(10)
            });
            await dealRepository.AddAsync(new Deal
            {
                ProposerId = maximilianSterling.PersonId, RecipientId = kenjiTanaka.PersonId, DealLevel = 5,
                Description = "Securing tickets to a sports final.", Status = DealStatus.Accepted,
                DateProposed = DateTime.UtcNow.AddMonths(-1), DateDecided = DateTime.UtcNow.AddMonths(-1)
            });
            await dealRepository.AddAsync(new Deal
            {
                ProposerId = isabellaRossi.PersonId, RecipientId = sofiaPetrova.PersonId, DealLevel = 2,
                Description = "Fast-tracking a pharmaceutical patent.", Status = DealStatus.PendingDecision,
                DateProposed = DateTime.UtcNow.AddDays(-5)
            });
            await dealRepository.AddAsync(new Deal
            {
                ProposerId = arthurPendleton.PersonId, RecipientId = isabellaRossi.PersonId, DealLevel = 1,
                Description = "A 'strategic alliance' on media ownership.", Status = DealStatus.PendingDecision,
                DateProposed = DateTime.UtcNow.AddDays(-1)
            });
            await dealRepository.AddAsync(new Deal
            {
                ProposerId = isabellaRossi.PersonId, RecipientId = eleanorVance.PersonId, DealLevel = 3,
                Description = "Appointing a friendly regulator.", Status = DealStatus.Accepted,
                DateProposed = DateTime.UtcNow.AddYears(-1), DateDecided = DateTime.UtcNow.AddYears(-1).AddDays(15)
            });
            await dealRepository.AddAsync(new Deal
            {
                ProposerId = maximilianSterling.PersonId, RecipientId = helenaMarkov.PersonId, DealLevel = 4,
                Description = "Obtaining confidential bidding information.", Status = DealStatus.Declined,
                DateProposed = DateTime.UtcNow.AddDays(-45), DateDecided = DateTime.UtcNow.AddDays(-40)
            });
            
            
            // --- DEALS FOR TEST CASE ---
            await dealRepository.AddAsync(new Deal { ProposerId = cheapBrokovich.PersonId, RecipientId = kenjiTanaka.PersonId, DealLevel = 5, Description = "Gift basket for the holidays.", Status = DealStatus.Accepted, DateProposed = DateTime.UtcNow.AddMonths(-12), DateDecided = DateTime.UtcNow.AddMonths(-11) });
            await dealRepository.AddAsync(new Deal { ProposerId = cheapBrokovich.PersonId, RecipientId = sofiaPetrova.PersonId, DealLevel = 5, Description = "Campaign 'donation' (small).", Status = DealStatus.Accepted, DateProposed = DateTime.UtcNow.AddMonths(-10), DateDecided = DateTime.UtcNow.AddMonths(-9) });
            await dealRepository.AddAsync(new Deal { ProposerId = cheapBrokovich.PersonId, RecipientId = helenaMarkov.PersonId, DealLevel = 4, Description = "Looking the other way on an expired permit.", Status = DealStatus.Accepted, DateProposed = DateTime.UtcNow.AddMonths(-8), DateDecided = DateTime.UtcNow.AddMonths(-7) });
            await dealRepository.AddAsync(new Deal { ProposerId = cheapBrokovich.PersonId, RecipientId = eleanorVance.PersonId, DealLevel = 4, Description = "Introduction at a gala.", Status = DealStatus.Accepted, DateProposed = DateTime.UtcNow.AddMonths(-6), DateDecided = DateTime.UtcNow.AddMonths(-5) });

            // // This deal from Brokovich to Armstrong should fail the initial check and require proof.
            // // Proposing a Level 2 deal ($200M required) when he only has ~$150M in assets.
            // await dealRepository.AddAsync(new Deal { ProposerId = cheapBrokovich.PersonId, RecipientId = senatorArmstrong.PersonId, DealLevel = 2, Description = "Nanomachines, son! (And a small government subsidy).", Status = DealStatus.PreScreening, DateProposed = DateTime.UtcNow.AddDays(-1) });

            // --- Final Save ---
            await context.SaveChangesAsync();
            Console.WriteLine("Sample data has been seeded successfully.");
        }
    }
}