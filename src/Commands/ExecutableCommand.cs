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
        var outStream = OutputResolver.Resolve(args, out int redirectionIndex);
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

        var stdoutTask = process.StandardOutput.ReadToEndAsync();
        var stderrTask = process.StandardError.ReadToEndAsync();
        process.WaitForExit();
        Task.WaitAll(stdoutTask, stderrTask);

        outStream.Write(stdoutTask.Result);
        outStream.Flush();
    }
}
