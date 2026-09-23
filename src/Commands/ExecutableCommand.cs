using System.Diagnostics;

namespace CodeCrafters.Shell;

public class ExecutableCommand : ICommand
{
    public string ExecutablePath { get; }
    
    public ExecutableCommand(string executablePath)
    {
        ExecutablePath = executablePath;
    }

    public void Execute(string[] args)
    {
        Process.Start(ExecutablePath, args);
    }
}