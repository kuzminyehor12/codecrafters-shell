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
        var programName = Path.GetFileNameWithoutExtension(ExecutablePath);
        Process.Start(ExecutablePath, [programName, ..args]).WaitForExit();
    }
}