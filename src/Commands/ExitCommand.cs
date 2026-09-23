namespace CodeCrafters.Shell;

public class ExitCommand : ICommand
{
    public void Execute(string[] args)
    {
        Environment.Exit(0);
    }
}