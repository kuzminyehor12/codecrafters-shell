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
        var output = Output.Create(args);
        var processArgs = output.OutRedirectionIndex != -1 ? args[..output.OutRedirectionIndex] : args;

        var processInfo = new ProcessStartInfo
        {
            FileName = Path.GetFileName(ExecutableFile),
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

        Task.WhenAll(stdoutTask, stderrTask);

        output.StandardOutput.Write(stdoutTask.Result);
        output.StandardError.Write(stderrTask.Result);

        output.Flush();
    }
}
