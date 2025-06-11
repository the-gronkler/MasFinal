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
using Microsoft.Extensions.Logging;


Console.WriteLine("<--- MAS FINAL PROJECT---->\n\n");

var serviceProvider = Init.ConfigureServices();
Init.InitDb(serviceProvider, true);

Console.WriteLine("\n\nProgram finished.");

