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
        var isBackground = args[args.Length - 1] == "&";
        var output = Output.Create(args);
        var processArgs = CommandArguments(isBackground ? args[..(args.Length - 1)] : args, output);

        var isBackgroundOrRedirect = !isBackground || (output.OutRedirectionIndex == -1 && output.ErrRedirectionIndex == -1);

        var processInfo = new ProcessStartInfo
        {
            FileName = Path.GetFileName(ExecutableFile),
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = !isBackgroundOrRedirect,
            CreateNoWindow = isBackgroundOrRedirect
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

        if (isBackground)
        {
            output.StandardOutput.WriteLine($"[1] {process.Id}");
        }

        process.WaitForExit();
        
        var stdoutTask = process.StandardOutput.ReadToEndAsync();
        var stderrTask = process.StandardError.ReadToEndAsync();

        Task.WhenAll(stdoutTask, stderrTask);

        output.StandardOutput.Write(stdoutTask.Result);
        output.StandardError.Write(stderrTask.Result);

        output.Flush();
    }

    private static string[] CommandArguments(string[] args, Output output)
    {
        var cutIndex = RedirectionCutIndex(output);
        return cutIndex != -1 ? args[..cutIndex] : args;
    }

    private static int RedirectionCutIndex(Output output)
    {
        var cutIndex = -1;

        if (output.OutRedirectionIndex != -1)
        {
            cutIndex = output.OutRedirectionIndex;
        }

        if (output.ErrRedirectionIndex != -1)
        {
            cutIndex = cutIndex == -1
                ? output.ErrRedirectionIndex
                : Math.Min(cutIndex, output.ErrRedirectionIndex);
        }

        return cutIndex;
    }
}
