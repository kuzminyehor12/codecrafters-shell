namespace CodeCrafters.Shell;

public class EchoCommand : ICommand
{
    public void Execute(string[] args)
    {
        var output = string.Join(' ', args);
        Console.WriteLine(output);
    }
}