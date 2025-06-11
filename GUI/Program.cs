using MasFinal.Data;
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

namespace GUI;

public static class Program
{
    public static IServiceProvider ServiceProvider { get; private set; }

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static async Task Main()
    {
        Application.SetHighDpiMode(HighDpiMode.SystemAware);
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        // Configure services
        var services = new ServiceCollection();
        ConfigureServices(services);
        ServiceProvider = services.BuildServiceProvider();

        // Initialize the database (seeding data if necessary)
        await InitDb(ServiceProvider);

        // Run the main form
        var mainForm = ServiceProvider.GetRequiredService<global::GUI.DealSelectorForm>();
        Application.Run(mainForm);
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite($"Data Source={AppDbContext.DbPath}")
                //.LogTo(Console.WriteLine, LogLevel.Information) // Optional: for debugging
                .EnableSensitiveDataLogging()
        );

        // Register Repositories
        services.AddScoped<IPersonRepository, PersonRepository>();
        services.AddScoped<IDealRepository, DealRepository>();
        services.AddScoped<IBillRepository, BillRepository>();
        services.AddScoped<IPartyRepository, PartyRepository>();
        services.AddScoped<IMovementRepository, MovementRepository>();
        services.AddScoped<IBusinessRepository, BusinessRepository>();
        services.AddScoped<IWorkerRepository, WorkerRepository>();

        // Register Services
        services.AddScoped<IBillService, BillService>();
        services.AddScoped<IProposeDealService, ProposeDealService>();
        services.AddScoped<IDealEvaluationService, DealEvaluationService>();
            
        // Register Data Helpers
        services.AddScoped<IDataSeeder, DataSeeder>();
        services.AddScoped<IDataDisplayer, DataDisplayer>(); // Not used by GUI, but good practice

        // Register Forms
        services.AddTransient<DealSelectorForm>();
        services.AddTransient<DealHistoryForm>();
        services.AddTransient<NewDealForm>();
        services.AddTransient<ProveEligibilityForm>();
    }

    private static async Task InitDb(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var scopedProvider = scope.ServiceProvider;

        // init static members
        var workerRepository = scopedProvider.GetRequiredService<IWorkerRepository>();
        Console.WriteLine("Initializing static application data...");
        await workerRepository.GetMinimumWageAsync();
        Console.WriteLine($"Worker.MinimumWage has been initialized to: {MasFinal.Models.Businesses.Worker.MinimumWage:C}");

        var context = scopedProvider.GetRequiredService<AppDbContext>();
        Console.WriteLine("Ensuring database schema is created...");
        await context.Database.EnsureCreatedAsync();
        Console.WriteLine("Database schema confirmed.");

        var dataSeeder = scopedProvider.GetRequiredService<IDataSeeder>();
        await dataSeeder.SeedIfEmptyAsync();
    }
}

