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
using Microsoft.Extensions.Logging;


Console.WriteLine("<--- MAS FINAL PROJECT---->\n\n");


var serviceProvider = ConfigureServices();

using (var scope = serviceProvider.CreateScope())
{
    var scopedProvider = scope.ServiceProvider;
    
    // init static members
    var workerRepository = scopedProvider.GetRequiredService<IWorkerRepository>();
    Console.WriteLine("Initializing static application data...");
    await workerRepository.GetMinimumWageAsync();
    Console.WriteLine($"Worker.MinimumWage has been initialized to: {MasFinal.Models.Businesses.Worker.MinimumWage:C}");
    
    
    var context = scopedProvider.GetRequiredService<AppDbContext>();
    Console.WriteLine("Ensuring database schema is created...");
    await context.Database.EnsureCreatedAsync();
    Console.WriteLine("Confirmed");


  

    var dataSeeder = scopedProvider.GetRequiredService<IDataSeeder>();
    var dataDisplayer = scopedProvider.GetRequiredService<IDataDisplayer>(); 
    
    // await dataSeeder.SeedAsync();
    // await dataDisplayer.DisplayAsync();
    
}


Console.WriteLine("\n\nProgram finished.");
return;


ServiceProvider ConfigureServices()
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
    services.AddScoped<IOligarchDealService, OligarchDealService>();
    services.AddScoped<IDealEvaluationService, DealEvaluationService>();

    services.AddScoped<IDataSeeder, DataSeeder>();
    services.AddScoped<IDataDisplayer, DataDisplayer>();

    return services.BuildServiceProvider();
}