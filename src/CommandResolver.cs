namespace CodeCrafters.Shell;

public class CommandResolver
{
    public static ICommand? Resolve(string? cmd)
    {
        if (IsExecutableExists(cmd))
        {
            return new ExecutableCommand(cmd!);    
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

    private static bool IsExecutableExists(string? cmd)
    {
        if (string.IsNullOrWhiteSpace(cmd))
        {
            return false;
        }

        var executablePaths = Commands.GetExecutablePaths();
        return executablePaths.Any(path => File.Exists(Path.Combine(path, cmd)) || File.Exists(Path.Combine(path, cmd + ".exe")));
    }
}