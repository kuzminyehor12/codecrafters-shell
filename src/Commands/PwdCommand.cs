namespace CodeCrafters.Shell;

public class PwdCommand : ICommand
{
    public void Execute(string[] args)
    {
        var outStream = OutputResolver.Resolve(args, out _);
        var currentDirectory = Directory.GetCurrentDirectory();
        outStream.WriteLine(currentDirectory);
    }
}