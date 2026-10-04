namespace CodeCrafters.Shell;

public class Commands
{
    public const string EXIT = "exit";
    public const string ECHO = "echo";
    public const string TYPE = "type";
    public const string PWD = "pwd";
    public const string CD = "cd";

    public static IReadOnlyCollection<string> BuiltInCommands => [EXIT, ECHO, TYPE, PWD, CD];

    public static IReadOnlyCollection<string> ExecutablePaths
    {
        get => field;
        set
        {
            var pathString = Environment.GetEnvironmentVariable("PATH");
            field = pathString?.Split(Path.PathSeparator, StringSplitOptions.TrimEntries) ?? [];
        }
    }
}