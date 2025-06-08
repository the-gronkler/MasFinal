using MasFinal.Data;
using MasFinal.Models;
using MasFinal.Models.Businesses;
using MasFinal.RepositoryContracts.Businesses;

namespace MasFinal.Repositories.Businesses;

public class WorkerRepository(AppDbContext context) 
    : Repository<Worker>(context), IWorkerRepository
{
    
    private const string MinimumWageKey = "MinimumWage";
    
    private static void ValidateWage(Worker entity)
    {
        if (entity.Wage < Worker.MinimumWage)
            throw new InvalidOperationException($"Worker wage ({entity.Wage:C}) cannot be less than the minimum wage ({Worker.MinimumWage:C}).");
        
    }

    public override async Task AddAsync(Worker entity)
    {
        ValidateWage(entity);
        await base.AddAsync(entity);
    }

    /// <summary>
    /// Enforces the minimum wage constraint on update.
    /// </summary>
    public override void Update(Worker entity)
    {
        ValidateWage(entity);
        base.Update(entity);
    }
    
    public async Task<double> GetMinimumWageAsync()
    {
        var config = await _context.StaticAttributes.FindAsync(MinimumWageKey);
        double value;

        if (config != null && double.TryParse(config.Value, out var dbValue))
        {
            value = dbValue;
        }
        else
        {
            // Value is not in DB or is invalid. Create/Fix it.
            value = 15.00;
            if (config == null)
            {
                config = new StaticAttribute { Key = MinimumWageKey, Value = value.ToString() };
                await _context.StaticAttributes.AddAsync(config);
            }
            else
                config.Value = value.ToString();
            
            // Save this default value so it exists for the next startup.
            await _context.SaveChangesAsync(); 
        }
            
        // Synchronize the static property
        Worker.MinimumWage = value;
        return value;
    }
    
    public async Task UpdateMinimumWageAsync(double newValue)
    {
        if (newValue <= 0)
            throw new ArgumentOutOfRangeException(nameof(newValue), "Minimum wage must be a positive number.");
        
        var config = await _context.StaticAttributes.FindAsync(MinimumWageKey);
        if (config == null)
        {
            config = new StaticAttribute { Key = MinimumWageKey };
            await _context.StaticAttributes.AddAsync(config);
        }

        config.Value = newValue.ToString();
        
        Worker.MinimumWage = newValue;
            
        // up wages for all workers
        var workers = _context.Workers.Where(w => w.Wage < newValue);
        foreach (var worker in workers)
            worker.Wage = Worker.MinimumWage;
    }
    
}
