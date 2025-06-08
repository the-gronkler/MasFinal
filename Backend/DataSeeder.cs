namespace MasFinal;

public interface IDataSeeder
{
    Task SeedAsync();
}

public class DataSeeder: IDataSeeder
{
    public async Task SeedAsync()
    {
        throw new NotImplementedException();
    }
}