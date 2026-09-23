namespace CodeCrafters.Shell;

public class CommandResolver
{
    public static ICommand? Resolve(string? cmd)
    {
        return cmd switch
        {
            Commands.EXIT => new ExitCommand(),
            Commands.ECHO => new EchoCommand(),
            Commands.TYPE => new TypeCommand(),
            _ => null
        };
    }
}