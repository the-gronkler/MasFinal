using MasFinal.Data;
using MasFinal.Init;
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

        var services = Init.ConfigureServices(); // backend services
        ConfigureForms(services);

        ServiceProvider = services.BuildServiceProvider();
        await Init.InitDbAsync(ServiceProvider);
        
        var mainForm = ServiceProvider.GetRequiredService<SelectOligarchAndPoliticianForm>();
        Application.Run(mainForm);
    }

    /// <summary>
    /// Registers all the forms with the dependency injection container.
    /// </summary>
    private static void ConfigureForms(IServiceCollection services)
    {
        services.AddTransient<SelectOligarchAndPoliticianForm>();
        services.AddTransient<PersonDealsDetailForm>();
        services.AddTransient<DealHistoryForm>();
        services.AddTransient<NewDealForm>();
        services.AddTransient<ProveEligibilityForm>();
    }
}