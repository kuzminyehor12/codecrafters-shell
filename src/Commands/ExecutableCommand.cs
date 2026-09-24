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
        var outputStream = OutputResolver.Resolve(args, out int redirectionIndex);
        var processArgs = redirectionIndex != -1 ? args[..redirectionIndex] : args;

        var processInfo = new ProcessStartInfo
        {
            FileName = ExecutableFile,
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
        };

        foreach (var arg in processArgs)
        {
            processInfo.ArgumentList.Add(arg);
        }

        var process = Process.Start(processInfo);
        if (process is null)
        {
            return;
        }

        var stdout = process.StandardOutput.ReadToEnd();
        var stderr = process.StandardError.ReadToEnd();

        process.WaitForExit();

        outputStream.Write(stdout);
        Console.Write(stderr);
        OutputResolver.Commit(outputStream);
    }
}
