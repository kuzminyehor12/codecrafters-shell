namespace CodeCrafters.Shell;

public class CommandResolver
{
    public static ICommand? Resolve(string? cmd)
    {
        if (IsExecutableExists(cmd, out var executablePath))
        {
            return new ExecutableCommand(executablePath);    
        }

        return ResolveBuiltIn(cmd);
    }

    private static ICommand? ResolveBuiltIn(string? cmd)
    {
        return cmd switch
        {
            Commands.EXIT => new ExitCommand(),
            Commands.ECHO => new EchoCommand(),
            Commands.TYPE => new TypeCommand(),
            _ => null
        };
    }

    private static bool IsExecutableExists(string? cmd, out string executablePath)
    {
        executablePath = string.Empty;
        
        if (string.IsNullOrWhiteSpace(cmd))
        {
            return false;
        }

        var executablePaths = Commands.GetExecutablePaths();
        var executableDirectoryPath = executablePaths.FirstOrDefault(path => File.Exists(Path.Combine(path, cmd)));

        if (executableDirectoryPath is not null)
        {
            executablePath = Path.Combine(executableDirectoryPath, cmd);
            return true;
        }

        executableDirectoryPath = executablePaths.FirstOrDefault(path => File.Exists(Path.Combine(path, cmd + ".exe")));

        if (executableDirectoryPath is not null)
        {
            executablePath = Path.Combine(executableDirectoryPath, cmd + ".exe");
            return true;
        }

        return false;
    }
}