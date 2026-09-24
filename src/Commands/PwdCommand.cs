namespace CodeCrafters.Shell;

public class PwdCommand : ICommand
{
    public void Execute(string[] args)
    {
        var output = Output.Create(args);
        var currentDirectory = Directory.GetCurrentDirectory();
        output.StandardOutput.WriteLine(currentDirectory);
        output.Flush();
    }
}