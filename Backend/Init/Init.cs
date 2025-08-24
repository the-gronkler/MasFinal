using MasFinal.Data;
using MasFinal.Models.Businesses;
using MasFinal.Repositories;
using MasFinal.Repositories.Businesses;
using MasFinal.Repositories.PoliticalOrganisations;
using MasFinal.RepositoryContracts;
using MasFinal.RepositoryContracts.Businesses;
using MasFinal.RepositoryContracts.PoliticalOrganisations;
using MasFinal.ServiceContracts;
using MasFinal.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MasFinal.Init
{
    public static class Init
    {
        /// <summary>
        /// Configures all the services for the application's dependency injection container.
        /// </summary>
        /// <returns>A configured IServiceCollection.</returns>
        public static IServiceCollection ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite($"Data Source={AppDbContext.DbPath}")
                       .LogTo(Console.WriteLine, LogLevel.Error)
                       .EnableSensitiveDataLogging()
            );

            // Repositories
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IDealRepository, DealRepository>();
            services.AddScoped<IBillRepository, BillRepository>();
            services.AddScoped<IPartyRepository, PartyRepository>();
            services.AddScoped<IMovementRepository, MovementRepository>();
            services.AddScoped<IBusinessRepository, BusinessRepository>();
            services.AddScoped<IWorkerRepository, WorkerRepository>();

            // Services
            services.AddScoped<IBillService, BillService>();
            services.AddScoped<IProposeDealService, ProposeDealService>();
            services.AddScoped<IDealEvaluationService, DealEvaluationService>();
            services.AddScoped<IDecideDealService, DecideDealService>();

            // Data Helpers
            services.AddScoped<IDataSeeder, DataSeeder>();
            services.AddScoped<IDataDisplayer, DataDisplayer>();

            return services;
        }

        /// <summary>
        /// Initialize  database, ensure schema is created, init static values, seed data if db empty.
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="showData">If true, displays seeded data in the console.</param>
        public static async Task InitDbAsync(IServiceProvider serviceProvider, bool showData = false)
        {
            using var scope = serviceProvider.CreateScope();
            var scopedProvider = scope.ServiceProvider;

            // create db schema if doesnt exist
            var context = scopedProvider.GetRequiredService<AppDbContext>();
            Console.WriteLine("Ensuring database schema is created...");
            await context.Database.EnsureCreatedAsync();
            Console.WriteLine("Confirmed, database at " + AppDbContext.DbPath);

            // Initialize static class members from the database
            var workerRepository = scopedProvider.GetRequiredService<IWorkerRepository>();
            Console.WriteLine("Initializing static application data...");
            await workerRepository.GetMinimumWageAsync();
            Console.WriteLine($"Worker.MinimumWage has been initialized to: {Worker.MinimumWage:C}");
            
            // Normally this would be a scheduled job, but for simplicity, we run it here on app start.
            var dealEvalService = serviceProvider.GetRequiredService<IDealEvaluationService>();
            var count = await dealEvalService.CleanUpPreScreeningDealsAsync();
            Console.WriteLine($"Set {count} old pre-screening deals to AutoRejected");

            // Seed db
            var dataSeeder = scopedProvider.GetRequiredService<IDataSeeder>();
            await dataSeeder.SeedIfEmptyAsync();


            if (showData)
            {
                var dataDisplayer = scopedProvider.GetRequiredService<IDataDisplayer>();
                await dataDisplayer.DisplayAsync();
            }
        }
    }
}