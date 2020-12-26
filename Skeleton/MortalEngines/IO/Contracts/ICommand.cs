namespace MortalEngines.IO.Contracts
{
    public interface ICommand
    {
        string Name { get; set; }
        string[] Parameters { get; set; }
    }
}