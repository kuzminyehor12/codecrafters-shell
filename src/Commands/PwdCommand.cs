namespace CodeCrafters.Shell;

public class PwdCommand : ICommand
{
    public void Execute(string[] args)
    {
        var currentDirectory = Directory.GetCurrentDirectory();
        Console.WriteLine(currentDirectory);
    }
}