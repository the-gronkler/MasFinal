using MasFinal.Data;
using MasFinal.Init;
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
    public static IServiceProvider ServiceProvider { get; private set; } = null!;

    [STAThread]
    private static async Task Main()
    {
        Application.SetHighDpiMode(HighDpiMode.SystemAware);
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        // Configure services
        var services = new ServiceCollection();
        ConfigureServices(services);
        ServiceProvider = services.BuildServiceProvider();

        await Init.InitDb(ServiceProvider);

        // Run the main form
        var mainForm = ServiceProvider.GetRequiredService<SelectOligarchAndPoliticianForm>();
        Application.Run(mainForm);
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite($"Data Source={AppDbContext.DbPath}")
                //.LogTo(Console.WriteLine, LogLevel.Information) //  for debug
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
        services.AddScoped<IDataDisplayer, DataDisplayer>(); 

        // Register Forms
        services.AddTransient<SelectOligarchAndPoliticianForm>();
        services.AddTransient<PersonDealsDetailForm>();
        services.AddTransient<DealHistoryForm>();
        services.AddTransient<NewDealForm>();
        services.AddTransient<ProveEligibilityForm>();
    }

    
}

