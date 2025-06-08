namespace MasFinal;

public interface IDataDisplayer
{
    Task DisplayAsync();
}

public class DataDisplayer : IDataDisplayer
{
    public async Task DisplayAsync()
    {
        throw new NotImplementedException();
    }
}