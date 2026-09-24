using System.Diagnostics;

namespace CodeCrafters.Shell;

public class ExecutableCommand : ICommand
{
    public string ExecutableFile { get; }

    public ExecutableCommand(string executableFile)
    {
        ExecutableFile = executableFile;
    }

    public void Execute(string[] args)
    {
        var processInfo = new ProcessStartInfo(ExecutableFile, args)
        { 
            RedirectStandardOutput = true,
            RedirectStandardError = false
        };

        var process = Process.Start(processInfo);
        process?.StandardError.Close();
        process?.WaitForExit();
    }
}