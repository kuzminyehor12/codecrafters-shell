namespace CodeCrafters.Shell;

public class JobsCommand : ICommand
{
    public void Execute(string[] args)
    {
        var output = Output.Create(args);
        output.StandardOutput.WriteLine();
    }
}