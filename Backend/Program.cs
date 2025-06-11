using MasFinal.Init;
using Microsoft.Extensions.DependencyInjection;

Console.WriteLine("<--- MAS FINAL PROJECT---->\n\n");

var services = Init.ConfigureServices();
var serviceProvider = services.BuildServiceProvider();

await Init.InitDbAsync(serviceProvider, true);

Console.WriteLine("\n\nProgram finished.");