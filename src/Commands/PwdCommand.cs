namespace CodeCrafters.Shell;

public class PwdCommand : ICommand
{
    public void Execute(string[] args)
    {
        var outStream = Output.Resolve(args, out _);
        var currentDirectory = Directory.GetCurrentDirectory();
        outStream.WriteLine(currentDirectory);
        Output.Flush(outStream);
    }
}