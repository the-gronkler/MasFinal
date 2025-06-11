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

namespace MasFinal.Init;

public static class Init
{
    public static async Task InitDb(IServiceProvider serviceProvider, bool showData = false)
    {
        using var scope = serviceProvider.CreateScope();

        var scopedProvider = scope.ServiceProvider;

     

        var context = scopedProvider.GetRequiredService<AppDbContext>();
        Console.WriteLine("Ensuring database schema is created...");
        await context.Database.EnsureCreatedAsync();
        Console.WriteLine("Confirmed");

        // init static members
        var workerRepository = scopedProvider.GetRequiredService<IWorkerRepository>();
        Console.WriteLine("Initializing static application data...");
        await workerRepository.GetMinimumWageAsync();
        Console.WriteLine(
            $"Worker.MinimumWage has been initialized to: {Worker.MinimumWage:C}");
        
        var dataSeeder = scopedProvider.GetRequiredService<IDataSeeder>();
        var dataDisplayer = scopedProvider.GetRequiredService<IDataDisplayer>();

        await dataSeeder.SeedIfEmptyAsync();
        
        if (showData)
            await dataDisplayer.DisplayAsync();
    }

    public static ServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();

        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite($"Data Source={AppDbContext.DbPath}")
                .LogTo(Console.WriteLine, LogLevel.Error)
                .EnableSensitiveDataLogging()
        );

        services.AddScoped<IPersonRepository, PersonRepository>();
        services.AddScoped<IDealRepository, DealRepository>();
        services.AddScoped<IBillRepository, BillRepository>();

        services.AddScoped<IPartyRepository, PartyRepository>();
        services.AddScoped<IMovementRepository, MovementRepository>();

        services.AddScoped<IBusinessRepository, BusinessRepository>();
        services.AddScoped<IWorkerRepository, WorkerRepository>();

        services.AddScoped<IBillService, BillService>();
        services.AddScoped<IProposeDealService, ProposeDealService>();
        services.AddScoped<IDealEvaluationService, DealEvaluationService>();

        services.AddScoped<IDataSeeder, DataSeeder>();
        services.AddScoped<IDataDisplayer, DataDisplayer>();

        return services.BuildServiceProvider();
    }
}