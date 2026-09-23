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

        if (IsExecutableExists(cmd))
        {
            return new ExecutableCommand(cmd!);    
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