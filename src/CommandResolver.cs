namespace CodeCrafters.Shell;

public class CommandResolver
{
    public static ICommand? Resolve(string? cmd)
    {
        var builtInCommand = ResolveBuiltIn(cmd);

        if (builtInCommand is not null)
        {
            return builtInCommand;
        }

        var executablePath = FindExecutablePath(cmd);
        if (executablePath is not null)
        {
            return new ExecutableCommand(executablePath);
        }

        return null;
    }

    private static ICommand? ResolveBuiltIn(string? cmd)
    {
        return cmd switch
        {
            Commands.EXIT => new ExitCommand(),
            Commands.ECHO => new EchoCommand(),
            Commands.TYPE => new TypeCommand(),
            Commands.PWD => new PwdCommand(),
            Commands.CD => new CdCommand(),
            _ => null
        };
    }

    private static string? FindExecutablePath(string? cmd)
    {
        if (string.IsNullOrWhiteSpace(cmd))
        {
            return null;
        }

        foreach (var path in Commands.GetExecutablePaths())
        {
            var fullPath = Path.Combine(path, cmd);
            
            if (FileUtilities.IsExecutable(fullPath))
            {
                return fullPath;
            }
        }

        return null;
    }
}